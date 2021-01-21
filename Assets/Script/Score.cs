using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Score
{
    [SerializeField]
    private int currentScore;
    public int CurrentScore
    {
        get
        {
            return currentScore;
        }

        set
        {
            currentScore = value;
            Hud.Instance.UpdateScoreValue(currentScore);
        }
    }

    [SerializeField]
    private int levelScoreBonus;

    [SerializeField] 
    private int pluseScoreBonus;

    public void AddLevelBonus()
    {
        CurrentScore += levelScoreBonus;
    }

    public void AddTurnBonus()
    {
        CurrentScore += pluseScoreBonus;
    }
 
}
