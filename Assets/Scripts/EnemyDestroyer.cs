using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour{
    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Enemy")){
            Destroy(other.gameObject);
        }
    }
}

