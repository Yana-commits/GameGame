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
        InitializeLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeLevel()
    {
        level = new LevelParameters(currentLevel);

        field = Field.Create(Level, LevelRepository, Enemy, Player);
    }
    public void NewLevel()
    {
        Index++;
        if (Index >= 9)
        {
            Index = 0;
        }
        currentLevel++;
        Debug.Log($"{currentLevel}");
        Destroy(field.gameObject);
        InitializeLevel();
    }
}