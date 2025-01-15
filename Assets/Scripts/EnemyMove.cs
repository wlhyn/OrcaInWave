using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour{
    public float initialSpeed;
    private float acceleration;
    private float currentSpeed;

    private GameController gameController; 

    void Start(){
        //initialSpeed = 2.5f;
        acceleration = 0.25f;
        currentSpeed = initialSpeed;
        gameController = FindObjectOfType<GameController>();
    }

    void Update(){
        if (gameController != null && gameController.gameOver){
            currentSpeed = 0f;
        }else{
            currentSpeed += acceleration * Time.deltaTime;
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
            //Debug.Log("currentSpeed of Enemy = " + currentSpeed);
        }
    }
}

