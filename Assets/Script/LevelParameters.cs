using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelParameters 
{
    [SerializeField]
    private int h_size;

    [SerializeField]
    private int w_size;

    [SerializeField]
    private int increaseNomber;

    public int H_size
    {
        get
        {
            return h_size;
        }

    }
    public int W_size
    {
        get { return w_size; }
    }
    public int IncreaseNomber
    {
        get { return increaseNomber; }
    }
    public LevelParameters(int currentLevel)
    {
        int fieldIncreaseStep = currentLevel / 3;
        if (fieldIncreaseStep % 2 == 0 && fieldIncreaseStep != 0)
        {
            h_size = 18 + 6 * fieldIncreaseStep;
            w_size = 6 + 3 * (fieldIncreaseStep - 1);
        }
        else if (fieldIncreaseStep % 2 != 0 && fieldIncreaseStep != 0)
        {
            h_size = 18 + 6 * (fieldIncreaseStep - 1);
            w_size = 6 + 3 * fieldIncreaseStep;
        }
        else
        {
            h_size = 18 + 6 * fieldIncreaseStep;
            w_size = 6 + 3 * fieldIncreaseStep ;
        }
    
        increaseNomber = 1 + fieldIncreaseStep;
    }
}
