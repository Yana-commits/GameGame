using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonsInput : MonoBehaviour, IAxisInput
{
    [SerializeField] CustomBt leftButton;
    [SerializeField] CustomBt rightButton;
    [SerializeField] CustomBt upButton;
    [SerializeField] CustomBt downButton;

    private Dictionary<CustomBt, Vector2> buttonsValues;

    public Vector2 Direction => GetDirection();

    private void Awake()
    {
        buttonsValues = new Dictionary<CustomBt, Vector2>
        {
            [leftButton] = Vector2.left,
            [rightButton] = Vector2.right,
            [upButton] = Vector2.up,
            [downButton] = Vector2.down,
        };
    }

    private Vector2  GetDirection()
    {
        Vector2 retVal = Vector2.zero;

        if (buttonsValues != null)
        {
            retVal = buttonsValues
                .Where(pair => pair.Key.pressed)
                .Aggregate(Vector2.zero, (acc, pair) => acc + pair.Value);
        }
        return retVal;
    }

   
}
