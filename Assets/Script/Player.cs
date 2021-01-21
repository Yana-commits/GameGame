using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Joystick joystick;
    public int speed;
  
    void Start()
    {
        
        joystick = FindObjectOfType<Joystick>();
        rigidbody = GetComponent<Rigidbody>();
        
        GetComponent<Renderer>().material.color = Controller.Instance.LevelRepository.LevelList[Controller.Instance.Index].levelColor;
        VictoryController.Win += EndLvl;
        Controller.OnGameOver += EndLvl;
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
            Controller.OnGameOver -= EndLvl;
            Destroy(gameObject);
        }
    }

    

}