using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class VictoryController : MonoBehaviour
{

    public List<GameObject> allChip2 = new List<GameObject>();

    private int amount = 0;

    
    void Awake()
    {
        Chip.Score += ChipAmount;
        Controller.OnInitializeComplete += Collect;
        Controller.OnGameOver += GameOverHandler;
        Controller.OnInitializeComplete -= Collect;
        Controller.OnGameOver -= GameOverHandler;
    }

    private void ChipAmount()
    {
        amount++;
       
        CheckChips();
    }
    private void CheckChips()
    {
        if (amount == allChip2.Count)
        {
            Debug.Log("Win!");
            Controller.Instance.GameWinner();
            DeliteList();
        }
        else
        {
            //Debug.Log("Continiue");
        }
    }
    private void Collect()
    {
        //allChip2 = new List<GameObject>(GameObject.FindObjectsOfType<Chip>().Select(c=> c.gameObject));
        StartCoroutine(CollectDelay());
    }

    IEnumerator CollectDelay()
    {
        yield return new WaitForSeconds(1f);
        allChip2 = new List<GameObject>(GameObject.FindObjectsOfType<Chip>().Select(c => c.gameObject));
        
    }
    private void DeliteList()
    {
        if (allChip2.Count > 0)
            allChip2.Clear();
        amount = 0;
    }

    private void GameOverHandler()
    {
        Controller.Instance.ClearField();
        DeliteList();
    }
}
