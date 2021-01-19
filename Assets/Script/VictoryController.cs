using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    public GameObject[] allChips;

    public List<GameObject> allChip2 = new List<GameObject>();

    private int amount = 0;

    public delegate void WinDelegate();
    public static event WinDelegate Win;
    void Start()
    {
        Chip.Score += ChipAmount;
        Invoke("Collect", 1f);
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
            Win?.Invoke();
            Controller.Instance.NewLevel();
            DeliteList(ref allChip2);
            Invoke("Collect", 3f);
        }
        else
        {
            //Debug.Log("Continiue");
        }
    }
    private void Collect()
    {
        //allChips = GameObject.FindGameObjectsWithTag("Chip");

        allChip2 = new List<GameObject>(GameObject.FindGameObjectsWithTag("Chip"));
    }
    private void DeliteList(ref List<GameObject>allChip2)
    {
        allChip2 = new List<GameObject>();
        amount = 0;
    }

}
