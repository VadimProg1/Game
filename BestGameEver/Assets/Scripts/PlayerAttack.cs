using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeAttack;
    private float dashTimeAttack;
    public float startTimeAttack;
    public float startDashTimeAttack;
    public Transform attackPos;
    public LayerMask meleeEnemies;
    public LayerMask rangeEnemies;
    public LayerMask turretEnemies;
    public LayerMask bossEnemy;
    public float attackRange;
    public int damage;
    private bool hitCheck;

    public Animator animator;
    private float animationTime = 0.005f;
    private float tempAnimationTime = 0.005f;
    bool checkAnimation = false;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            tempAnimationTime = animationTime;
            checkAnimation = true;
        }

        if (timeAttack <= 0 || GetComponent<PlayerController>().dashing)
        {           
            if ((Input.GetKey(KeyCode.Space) && GetComponent<PlayerController>().dashing == false) || (GetComponent<PlayerController>().dashHit == false && GetComponent<PlayerController>().dashing))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, meleeEnemies);
                hitCheck = Physics2D.OverlapCircle(attackPos.position, attackRange, meleeEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<MeleeEnemy>().TakeDamage(damage);
                }
                if (hitCheck)
                {
                    if (GetComponent<PlayerController>().dashing)
                    {
                        GetComponent<PlayerController>().dashHit = true;
                    }
                    else
                    {
                        checkAnimation = true;                      
                    }
                }

                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, rangeEnemies);
                hitCheck = Physics2D.OverlapCircle(attackPos.position, attackRange, rangeEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<RangeEnemy>().TakeDamage(damage);
                }
                if (hitCheck)
                {
                    if (GetComponent<PlayerController>().dashing)
                    {
                        GetComponent<PlayerController>().dashHit = true;
                    }
                    else
                    {
                        checkAnimation = true;
                    }
                }

                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, turretEnemies);
                hitCheck = Physics2D.OverlapCircle(attackPos.position, attackRange, turretEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<TurretEnemy>().TakeDamage(damage);
                }
                if (hitCheck)
                {
                    if (GetComponent<PlayerController>().dashing)
                    {
                        GetComponent<PlayerController>().dashHit = true;
                    }
                    else
                    {
                        checkAnimation = true;
                    }
                }

                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, bossEnemy);
                hitCheck = Physics2D.OverlapCircle(attackPos.position, attackRange, bossEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<BossScript>().TakeDamage(damage);
                }
                if (hitCheck)
                {
                    if (GetComponent<PlayerController>().dashing)
                    {
                        GetComponent<PlayerController>().dashHit = true;
                    }
                    else
                    {
                        checkAnimation = true;
                    }
                }
            }
            timeAttack = startDashTimeAttack;
        }
        else
        {
            timeAttack -= Time.deltaTime;
        }

        if (checkAnimation && tempAnimationTime >= 0)
        {
            animator.SetBool("IsAttacking", true);
            tempAnimationTime -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            checkAnimation = false;
            tempAnimationTime = animationTime;
        }
    }

}
