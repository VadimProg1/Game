using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeAttack;
    public float startTimeAttack;
    public Transform attackPos;

    public LayerMask meleeEnemies;

    public int clhkdsfajhk;
    //public LayerMask rangeEnemies;

    public float attackRange;
    public int damage;

    void Update()
    {
        if(timeAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, meleeEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<RangeEnemy>().TakeDamage(damage);
                }
                /*
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, rangeEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<RangeEnemy>().TakeDamage(damage);
                }
                */
            }
            timeAttack = startTimeAttack;
        }
        else
        {
            timeAttack -= Time.deltaTime;
        }

    }
}
