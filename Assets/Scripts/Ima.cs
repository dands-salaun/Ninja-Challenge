using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ima : MonoBehaviour
{
    private Rigidbody2D myRg;
    private GameObject player;
    Vector2 playerDirection;
    float timeStamp;
    bool flyCat;

    public PowerUpController puController;    
    void Start()
    {
        myRg = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        puController = GameObject.FindGameObjectWithTag("ControlePU").GetComponent<PowerUpController>();
    }

    
    void Update()
    {
        if (puController.ima)
        {
            playerDirection = -(transform.position - player.transform.position).normalized;
            myRg.velocity = new Vector2(playerDirection.x, playerDirection.y) * 300f;
        }
    }


}
