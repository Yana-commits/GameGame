using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcelrationInput : MonoBehaviour, IAxisInput
{
    public Vector2 Direction => GetDirection();
    private Vector2 GetDirection()
    {
        Vector2 retVal = Vector2.zero;

        retVal = new Vector3(Input.acceleration.x, -Input.acceleration.y , 0f);

        if (retVal.sqrMagnitude > 1)
        {
            retVal.Normalize();
        }
        return retVal;
    }
}
