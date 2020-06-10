using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{
    private float timeAttack;
    public float startTimeAttack;
    public Transform attackPos;
    public LayerMask meleeEnemies;
    public LayerMask rangeEnemies;
    public float attackRange;
    public int damage;

    void Update()
    {
        if(timeAttack <= 0 || GetComponent<PlayerController>().dashing)
        {
            if (Input.GetKey(KeyCode.Space) || GetComponent<PlayerController>().dashing)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, meleeEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<MeleeEnemy>().TakeDamage(damage);
                }
                
                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, rangeEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<RangeEnemy>().TakeDamage(damage);
                }               
            }
            timeAttack = startTimeAttack;
        }
        else
        {
            timeAttack -= Time.deltaTime;
        }
    }
}
