using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardSmall : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text lvlText;

    public void Initialize( string name, int score,int level)
    {
        nameText.text = name;
        scoreText.text = score.ToString();
        lvlText.text = level.ToString();
    }
}
