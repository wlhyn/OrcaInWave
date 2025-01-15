using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour{
    private GameController gameController;
    public float SPEED;
    private Rigidbody2D rb;

    void Start(){
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        float inputx = Input.GetAxisRaw("Horizontal");
        float inputy = Input.GetAxisRaw("Vertical");

        Vector2 inputDirection = new Vector2(inputx, inputy).normalized;
        Vector2 movement = inputDirection * SPEED;

        if (gameController != null && gameController.gameOver){
            rb.velocity = Vector2.zero;
        }else{
            rb.velocity = movement;
        }
    }
}

