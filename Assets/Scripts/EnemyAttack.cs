using UnityEngine;

public class EnemyAttack : MonoBehaviour{

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Enemy")){
            GameController gameController = FindObjectOfType<GameController>();
            if (gameController != null){
                gameController.SetGameOver(true);
            }
        }
    }
}





