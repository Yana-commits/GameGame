using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Data;

public interface IAxisInput
{
    Vector2 Direction { get; }
}

public class Player : MonoBehaviour
{
    public IAxisInput currentInput;

    private Rigidbody rigidbody;
    public int speed;
    public int normalSpeed;
    private int selection;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        selection = Controller.Instance.faktor;

        GetComponent<Renderer>().material.color = Controller.Instance.LevelRepository.LevelList[Controller.Instance.Index].levelColor;
        VictoryController.Win += EndLvl;
        Controller.OnGameOver += EndLvl;
        //transform.SetParent(Controller.Instance.Field.transform);
        //transform.localScale = vector;
    }

    void FixedUpdate()
    {
        Vector2 dir = currentInput.Direction;
        rigidbody.velocity = new Vector3(-dir.y, 0 , dir.x) * normalSpeed;
    }

    public void EndLvl()
    {
        if (gameObject)
        {
            VictoryController.Win -= EndLvl;
            Controller.OnGameOver -= EndLvl;
            Destroy(gameObject);
        }
    }

    //public void BBB()
    //{

    //    Vector3 direction = Vector3.zero;
    //    if (!allBt.leftButton.pressed)
    //    {
    //        if (!allBt.rightButton.pressed)
    //        {
    //            return;
    //        }
    //        direction.z = -1;
    //        Debug.Log("MMM");
    //    }
    //    direction.z = 1;
    //    Debug.Log("RRR");

    //    if (!allBt.downButton.pressed)
    //    {
    //        if (!allBt.upButton.pressed)
    //        {
    //            return;
    //        }
    //        direction.x = 1;
    //        Debug.Log("BBB");
    //    }
    //    direction.x = -1;
    //    rigidbody.velocity = direction * speed;
    //}

}