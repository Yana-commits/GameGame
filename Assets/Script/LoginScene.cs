using Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    [SerializeField]
    private Button play;

    [SerializeField]
    private Button leaderBoard;

    [SerializeField]
    private Button offAdv;

    [SerializeField]
    private Button exit;

    [SerializeField]
    private Button inputBt;

    [SerializeField]
    private InputField  name;
    [SerializeField]
    private Button joystic;
    [SerializeField]
    private Button buttons;
    [SerializeField]
    private Button giro;

    void Start()
    {
       play.onClick.AddListener(() => ToPlay());
       leaderBoard.onClick.AddListener(() => ShowLeaderBoard());
        inputBt.onClick.AddListener(() => Input());
        exit.onClick.AddListener(() => Exit());
        joystic.onClick.AddListener(() => ForJoystic());
      buttons.onClick.AddListener(() => ForButton());
        giro.onClick.AddListener(() => ForGiro());
    }

    
    void Update()
    {
        
    }

    private void Input()
    {
        UserDataController.Instance().info.Name = name.text.ToString();
    }
    private void ToPlay()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
    private void ShowLeaderBoard()
    { 
    
    }
    private void Exit()
    {
        Application.Quit();
    }
    public void ForJoystic()
    {
        UserDataController.Instance().info.faktor = 0;
    }
    public void ForButton()
    {
        UserDataController.Instance().info.faktor = 1;
    }
    public void ForGiro()
    {
        UserDataController.Instance().info.faktor = 2;
    }
}
