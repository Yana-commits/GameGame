using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(Controller.Instance.Field.transform);
    }

}
