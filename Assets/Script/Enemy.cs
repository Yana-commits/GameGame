using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    void Start()
    {
        AlignOnGrid();
        Prent();
    }

    private void AlignOnGrid()
    {
        Vector3 alignedPosition = transform.position;
        alignedPosition.x = Mathf.Round(transform.position.x);
        alignedPosition.z = Mathf.Round(transform.position.z);
        transform.position = alignedPosition;
    }
    private void Prent()
    {
        transform.SetParent(Controller.Instance.Field.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enamy"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Game over");
            Destroy(other.gameObject);
        }
    }
}
