using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetFindScript : MonoBehaviour
{
    [SerializeField] EnemyBasicScript enemyBasic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemyBasic.findPlayer = true;
        }
    }
}
