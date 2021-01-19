using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Joystick joystick;
    public int speed;
    private Vector3 vector = new Vector3(0.1f, 0.1f, 0.16f);


    void Start()
    {
        
        joystick = FindObjectOfType<Joystick>();
        rigidbody = GetComponent<Rigidbody>();
        
        GetComponent<Renderer>().material.color = Controller.Instance.LevelRepository.LevelList[Controller.Instance.Index].levelColor;
        VictoryController.Win += EndLvl;

        //transform.SetParent(Controller.Instance.Field.transform);
        //transform.localScale = vector;
    }

   
    void FixedUpdate()
    {
        Move();

    }

    public void Move()
    {
        rigidbody.velocity = new Vector3(-joystick.Vertical * speed, rigidbody.velocity.y, joystick.Horizontal * speed);
    }
    public void EndLvl()
    {
        if (gameObject)
        {
            VictoryController.Win -= EndLvl;
            Destroy(gameObject);
        }
    }

}