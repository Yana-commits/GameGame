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
        Controller.Win += EndLvl;
        Controller.OnGameOver += EndLvl;
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
            Controller.Win -= EndLvl;
            Controller.OnGameOver -= EndLvl;
            Destroy(gameObject);
        }
    }
}