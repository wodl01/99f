using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicScript : MonoBehaviour
{
    [Header("Hp")]
    [SerializeField] float curHealth;
    [SerializeField] float maxHealth;

    [Header("Shape")]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Target")]
    public GameObject player;
    public bool findPlayer;
    private void OnEnable()
    {
        curHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            BulletScript bulletScript = collision.GetComponent<BulletScript>();

            if (!bulletScript.isPlayerAttack) return;

            curHealth -= bulletScript.bulletDmg;


            if (curHealth <= 0)
                gameObject.SetActive(false);
        }
    }
}
