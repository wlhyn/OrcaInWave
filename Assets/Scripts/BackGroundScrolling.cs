using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour{
    private MeshRenderer render;
    public float initialSpeed; //{ get; private set; }
    private float acceleration;
    private float offset;
    //public bool gameOver { get; private set; }
    private float maximumSpeed;

    private GameController gameController; 

    private float startTime;

    void Start(){
        render = GetComponent<MeshRenderer>(); 
        offset = 0;

        initialSpeed = 0.25f;

        acceleration = 0.005f;

        gameController = FindObjectOfType<GameController>();

        startTime = Time.time;

        maximumSpeed = 0.72f;
    }

    void Update(){
        if (gameController != null && gameController.gameOver){
            initialSpeed = 0f;
        }else{
            float timeElapsed = Time.time - startTime;
            float currSpeed = initialSpeed + acceleration * timeElapsed;

            if (currSpeed > maximumSpeed) {
                currSpeed = maximumSpeed;
            }

           // Debug.Log("currSpeed = " + currSpeed);
            offset += Time.deltaTime * currSpeed;
            render.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
    
    public void ResetSpeed(float newInitialSpeed){
        initialSpeed = newInitialSpeed;
        startTime = Time.time;
    }
}


