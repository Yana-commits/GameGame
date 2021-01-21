using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    [SerializeField]
    private Button nextLvlBt;
    [SerializeField]
    private Button restartBt;
    [SerializeField]
    private Button resetBt;
    void Start()
    {
        nextLvlBt.onClick.AddListener(() => Hud.Instance.HideWinWindow());
        nextLvlBt.onClick.AddListener(() => Controller.Instance.NewLevel());
        restartBt.onClick.AddListener(() => Hud.Instance.HideLoseWindow());
        restartBt.onClick.AddListener(() => Controller.Instance.TryAgain());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
