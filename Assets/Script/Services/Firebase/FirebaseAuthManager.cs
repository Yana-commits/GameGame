using System;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Services.Firebase
{
    public class LoginCredential
    {
        public string email;
        public string password;
    }

    public class CreateCredential : LoginCredential
    {
        public string name;
    }

    public class FirebaseAuthManager : IDisposable
    {

        public bool IsInitialized { get; private set; } = false;
        public bool LoginInProcess { get; private set; }
        public bool IsLoggined => auth.CurrentUser != null;

        public FirebaseUser User { get; private set; }
        public event Action<FirebaseUser> OnUserChanged;

        protected FirebaseAuth auth = null;

        private bool disposed = false;

        public FirebaseAuthManager() { }
        ~FirebaseAuthManager()
            => Dispose(false);

#region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                // Free any other managed objects here.
                auth?.Dispose();
                auth = null;
            }
            // Free any unmanaged objects here.

            disposed = true;
        }

#endregion

#region Initialize

        public void Initialize()
        {
            auth = FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(auth, EventArgs.Empty);

#if UNITY_EDITOR
            if (User == null)
            {
                var t = Utils.LOGGED;
                switch (t)
                {
                    case Utils.LogType.EM:
                        _ = SigninWithEmailAsync(new LoginCredential
                        {
                            email = PlayerPrefs.GetString("Email"),
                            password = PlayerPrefs.GetString("Pwd")
                        });
                        break;
                    default:
                        break;
                }
            }
#endif

            IsInitialized = true;
        }

#endregion

#region Firebase callback

        void AuthStateChanged(object sender, EventArgs e)
        {
            FirebaseAuth senderAuth = sender as FirebaseAuth;

            if (senderAuth == auth && senderAuth.CurrentUser != User)
            {
                bool signedIn = User != senderAuth.CurrentUser && senderAuth.CurrentUser != null;
                if (!signedIn && User != null)
                    Debug.Log("Signed out " + User.UserId);

                User = senderAuth.CurrentUser;
                OnUserChanged?.Invoke(User);

                if (signedIn)
                {
                    Debug.Log("AuthStateChanged Signed in " + senderAuth.CurrentUser.UserId);
                    DisplayDetailedUserInfo(senderAuth.CurrentUser, 1);
                }
                else
                {

                }
            }
        }

#endregion

#region SignIn

        public async Task CreateUserWithEmailAsync(CreateCredential data)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                    Debug.Log("Not signed in, unable to update user profile");
                throw new Exception("internet connection not avilable");
            }

            Task authTask = null;

            try
            {
                LoginInProcess = true;
                await (authTask = auth.CreateUserWithEmailAndPasswordAsync(data.email, data.password));
                await (authTask = UpdateUserProfileAsync(data.name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LogTaskCompletion(authTask);
                LoginInProcess = false;
            }

            if (Utils.REMEMBER_ME)
            {
                PlayerPrefs.SetString("Email", data.email);
                PlayerPrefs.SetString("Pwd", data.password);
                Utils.LOGGED = Utils.LogType.EM;
            }
        }

        public async Task SigninWithEmailAsync(LoginCredential data)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("Not signed in, unable to update user profile");
                throw new Exception("internet connection not avilable");
            }

            Credential credential = EmailAuthProvider.GetCredential(data.email, data.password);
            Task authTask = auth.SignInWithCredentialAsync(credential);

            try
            {
                LoginInProcess = true;
                await authTask;
            }
            catch (FirebaseException ex)
            {
                Debug.LogException(ex);
                throw ex;
            }
            finally
            {
                LogTaskCompletion(authTask);
                LoginInProcess = false;
            }

            if (Utils.REMEMBER_ME)
            {
                PlayerPrefs.SetString("Email", data.email);
                PlayerPrefs.SetString("Pwd", data.password);
                Utils.LOGGED = Utils.LogType.EM;
            }
        }

        public async Task SignInAnonymously()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("Not signed in, unable to update user profile");
                throw new Exception("internet connection not avilable");
            }

            Task authTask = auth.SignInAnonymouslyAsync();

            try
            {
                LoginInProcess = true;
                await authTask;
            }
            catch (FirebaseException ex)
            {
                Debug.LogException(ex);
                throw ex;
            }
            finally
            {
                LogTaskCompletion(authTask);
                LoginInProcess = false;
            }
        }

        public void SignOut()
        {
            auth.SignOut();
            LoginInProcess = false;
            User = null;
            OnUserChanged?.Invoke(User);
            Utils.LOGGED = Utils.LogType.NONE;
        }

        public Task SendPasswordResetEmail(string email)
        {
            return auth.SendPasswordResetEmailAsync(email)
                .ContinueWith(authTask =>
                {
                    LogTaskCompletion(authTask, "Send Password Reset Email");
                    return authTask;
                })
                .Unwrap();
            /**/
        }

#endregion

#region User porifle

        public async Task UpdateUserProfileAsync(string displayName = null)
        {
            if (User == null)
            {
                Debug.Log("Not signed in, unable to update user profile");
                throw new Exception("User is sign out");
            }
            await User.UpdateUserProfileAsync(new UserProfile
            {
                DisplayName = displayName,
                PhotoUrl = auth.CurrentUser.PhotoUrl,
            });

            DisplayDetailedUserInfo(User, 1);
        }

#endregion

#region Display

        // Display a more detailed view of a FirebaseUser.
        protected void DisplayDetailedUserInfo(FirebaseUser user, int indentLevel)
        {
            string indent = new string(' ', indentLevel * 2);
            string text = DisplayUserInfo(user, indentLevel);
            text += $"{indent}Anonymous: {user.IsAnonymous}\n" +
                    $"{indent}Email Verified: {user.IsEmailVerified}\n" +
                    $"{indent}Phone Number: {user.PhoneNumber}\n";

            var providerDataList = new List<IUserInfo>(user.ProviderData);
            var numberOfProviders = providerDataList.Count;
            if (numberOfProviders > 0)
            {
                for (int i = 0; i < numberOfProviders; ++i)
                {
                    text += $"{indent}Provider Data: {i}\n";
                    text += DisplayUserInfo(providerDataList[i], indentLevel + 2);
                }
            }

            Debug.Log(text);
        }

        // Display user information.
        protected string DisplayUserInfo(IUserInfo userInfo, int indentLevel)
        {
            string indent = new string(' ', indentLevel * 2);
            var userProperties = new Dictionary<string, string> {
                {"Display Name", userInfo.DisplayName},
                {"Email", userInfo.Email},
                {"Photo URL", userInfo.PhotoUrl?.ToString()},
                {"Provider ID", userInfo.ProviderId},
                {"User ID", userInfo.UserId}
                };

            string text = "";
            foreach (var property in userProperties)
                if (!string.IsNullOrEmpty(property.Value))
                    text += $"{indent}{property.Key}: {property.Value}\n";
            return text;
        }

        internal static bool LogTaskCompletion<T>(T task, [CallerMemberName] string memberName = "") where T : Task
        {
            switch (task?.Status)
            {
                case TaskStatus.Canceled:
                    Debug.Log($"LogTask: {memberName} canceled.");
                    break;
                case TaskStatus.Faulted:
                    foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                    {
                        if (exception is FirebaseException firebaseEx)
                            Debug.Log($"LogTask: AuthError.{(AuthError)firebaseEx.ErrorCode}: {exception}");
                        //else if (exception is DatabaseException databaseException)
                        //    Debug.Log($"LogTask: DatabaseException.{databaseException.Message}: {exception}");
                    }
                    break;
                case TaskStatus.RanToCompletion:
                    Debug.Log($"LogTask: {memberName} completed");
                    return true;
                default:
                    break;
            }

            return false;
        }
#endregion
    }

    static internal class Utils
    {
        internal static bool REMEMBER_ME
        {
            get => PlayerPrefs.GetInt(nameof(REMEMBER_ME), 0) == 1;
            set => PlayerPrefs.SetInt(nameof(REMEMBER_ME), value ? 1 : 0);
        }

        internal static LogType LOGGED
        {
            get => (LogType)PlayerPrefs.GetInt(nameof(LOGGED), 0);
            set => PlayerPrefs.SetInt(nameof(LOGGED), (int)value);
        }

        internal enum LogType
        {
            NONE = 0,
            EM = 1,
            GG = 2,
            FB = 3,
            TW = 4,
            AM = 5,
            PH = 6,

        }
    }
}