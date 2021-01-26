﻿using Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private static Controller instance;

    public static Controller Instance
    {
        get
        {
            if (instance == null)
            {
                var controller =
                           Instantiate(Resources.Load("Prefabs/Controller")) as GameObject;

                instance = controller.GetComponent<Controller>();
            }
            return instance;
        }
    }
    [SerializeField]
    private LevelRepository levelRepository;
    public LevelRepository LevelRepository
    {
        get
        {
            return levelRepository;
        }
    }

    private int index = 0;
    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

   
    [SerializeField]
    private Enemy enemy;
    public Enemy Enemy
    {
        get
        {
            return enemy;
        }

        set
        {
            enemy = value;
        }
    }

    [SerializeField]
    private LevelParameters level;
    public LevelParameters Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    private Field field;
    public Field Field
    {
        get
        {
            return field;
        }

        set
        {
            field = value;
        }
    }

    [SerializeField] Joystick joystick;
    [SerializeField] ButtonsInput buttonsInput;
    [SerializeField] AcelrationInput acelration;

    [SerializeField]
    private Player player;
    public Player Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    private int currentLevel;
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
        }
    }

    private int nomberIncreaseStep;
    public int NomberIncreaseStep
    {
        get
        {
            return nomberIncreaseStep;
        }

        set
        {
            nomberIncreaseStep = value;
        }
    }

    [SerializeField]
    private Score score;
    public Score Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int faktor = 0;

    
    public delegate void IninitializeComplete();
    public static event IninitializeComplete OnInitializeComplete;
    public delegate void GameOverEvent();
    public static event GameOverEvent OnGameOver;
    public delegate void WinDelegate();
    public static event WinDelegate Win;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this) Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {

        CurrentLevel = UserDataController.Instance().info.currentLvl;
        Index = UserDataController.Instance().info.index;
        nomberIncreaseStep = UserDataController.Instance().info.increaseNomber;
        Score.CurrentScore = UserDataController.Instance().info.score;
        faktor = UserDataController.Instance().info.faktor;


        InitializeLevel();
    }
 
    public void InitializeLevel()
    {
        level = new LevelParameters(currentLevel, nomberIncreaseStep);
        Hud.Instance.UpdateLvlValue(currentLevel);
        field = Field.Create(Level, LevelRepository, Enemy, Player);

        var playerInstance = FindObjectOfType<Player>();
        IAxisInput current = null;
        switch (faktor)
        {
            case 0:
                current = joystick;
                break;
            case 1:
                current = buttonsInput;
                break;
            case 2:
                current = acelration;
                break;
        }
        playerInstance.currentInput = current;

        OnInitializeComplete?.Invoke();
    }
    public void NewLevel()
    {
        LevelControl();
        //Score.AddLevelBonus();
        Debug.Log($"{currentLevel}");       
        InitializeLevel();
    }
    public void TryAgain()
    {
        InitializeLevel();
    }
    
    public void ClearField()
    {
        Destroy(field.gameObject);
    }

    public void GameWinner()
    {
        Score.AddLevelBonus();
        Hud.Instance.ShowWinWindow();
        Win?.Invoke();
        ClearField();
    }
    public void GameOver()
    {
        Hud.Instance.ShowLoseWindow();
        OnGameOver?.Invoke();
    }

    public void LevelControl()
    {
        Index++;
        if (Index >= 9)
        {
            Index = 0;
        }
        UserDataController.Instance().info.index = Index;
        currentLevel++;
        if (currentLevel >= 9)
        {
            currentLevel = 0;
        }
        UserDataController.Instance().info.currentLvl = currentLevel;
        nomberIncreaseStep++;
        UserDataController.Instance().info.increaseNomber = nomberIncreaseStep;
        UserDataController.Instance().LocalSave();
    }
   
}
