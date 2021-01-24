using Game.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    private static Hud instance;

    public static Hud Instance
    {
        get
        {
            return instance;
        }

    }

    [SerializeField]
    private TMP_Text scoreValue;
    [SerializeField]
    private TMP_Text lvlValue;
    [SerializeField]
    private CanvasGroup winWindow;
    [SerializeField]
    private Button next;
    [SerializeField]
    private Button again;
    [SerializeField]
    private Button mainMenu;
    [SerializeField]
    private GameObject bottom;
    [SerializeField]
    private TMP_Text noName;
    [SerializeField]
    private Joystick joystick;
    private int selection;

    private void Awake()
    {
        instance = this;
        
      
    }

    void Start()
    {
        mainMenu.onClick.AddListener(() => Exit());
        noName.text = UserDataController.Instance().info.Name;
        selection = Controller.Instance.faktor;
        switch (selection)
        {
            case 0:
                bottom.SetActive(false);
                joystick.gameObject.SetActive(true);
                break;
            case 1:
                joystick.gameObject.SetActive(false);
                bottom.SetActive(true);
                break;
            case 2:
                joystick.gameObject.SetActive(false);
                bottom.SetActive(false);
                break;
        }
    }

    public void UpdateScoreValue(int value)
    {
        scoreValue.text = value.ToString();
        StartCoroutine(AnimationScore(value));
    }
    public void UpdateLvlValue(int value)
    {
        lvlValue.text = (value+1).ToString();
    }

    IEnumerator AnimationScore(int score)
    {
        
        int defoltScore = 0;
        while (defoltScore <= score)
        {
          //  Debug.Log(score);
            scoreValue.text = Mathf.Clamp(defoltScore, 0, score).ToString();
            while (scoreValue.fontSize <= 150)
            {
                scoreValue.fontSize += 10;
                yield return new WaitForFixedUpdate();
            }

            defoltScore += 1;
            yield return new WaitForFixedUpdate();

        }
        while (scoreValue.fontSize >= 100)
        {
            scoreValue.fontSize -= 10;
            yield return new WaitForFixedUpdate();
        }
    }

    public void ShowWinWindow()
    {
        next.gameObject.SetActive(true);
        //bottom.SetActive(false);
        StartCoroutine(Show(winWindow));

        winWindow.blocksRaycasts = true;

        winWindow.interactable = true;
    }
    public void ShowLoseWindow()
    {
        again.gameObject.SetActive(true);
        //bottom.SetActive(false);
        StartCoroutine(Show(winWindow));

        winWindow.blocksRaycasts = true;

        winWindow.interactable = true;
    }

    IEnumerator Show(CanvasGroup window)
    {
        while (window.alpha < 1f)
        {
            window.alpha += 0.01f;
            yield return new WaitForFixedUpdate();
        }
    }
    public void HideWinWindow()
    {
        StartCoroutine(Hide(winWindow));

        winWindow.blocksRaycasts = false;

        next.gameObject.SetActive(false);
    }
    public void HideLoseWindow()
    {
        StartCoroutine(Hide(winWindow));

        winWindow.blocksRaycasts = false;

        again.gameObject.SetActive(false);
    }

    IEnumerator Hide(CanvasGroup window)
    {
        while (window.alpha > 0f)
        {
            window.alpha -= 0.01f;
            yield return new WaitForFixedUpdate();
        }
    }
    private void Exit()
    {
        SceneManager.LoadScene("LoginScene", LoadSceneMode.Single);
    }
}
