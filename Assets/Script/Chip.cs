using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{


    public delegate void CollectDelegate();
    public static event CollectDelegate Score;
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Player")
        {
            Score();
            GetComponent<Renderer>().material.color = Controller.Instance.LevelRepository.LevelList[Controller.Instance.Index].levelColor; ;

            Collider col = GetComponent<Collider>();
            col.GetComponent<Collider>();
            col.enabled = false;
        }
        else if (collider.tag == "Enamy")
        {
            Destroy(gameObject);
        }
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enamy"))
        {
            Destroy(gameObject);
        }
    }
}
