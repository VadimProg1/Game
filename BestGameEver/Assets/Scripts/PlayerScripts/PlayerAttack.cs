using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timeAttack;
    private float dashTimeAttack;
    public float startTimeAttack;
    public float startDashTimeAttack;
    public Transform attackPos;
    public LayerMask meleeEnemies;
    public LayerMask rangeEnemies;
    public LayerMask turretEnemies;
    public LayerMask bossEnemy;
    public LayerMask MiniMeleeEnemies;
    public float attackRange;
    public int damage;
    private bool hitCheck;

    public Animator animator;
    private float animationTime = 0.01f;
    private float tempAnimationTime = 0.1f;
    bool checkAnimation = false;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsAttacking", true);          
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }

        if (checkAnimation)
        {
            tempAnimationTime -= Time.deltaTime;
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
                    timeAttack = startDashTimeAttack;
                    if (GetComponent<PlayerController>().dashing)
                    {
                        GetComponent<PlayerController>().dashHit = true;
                    }
                    else
                    {
                        animator.SetBool("IsAttacking", true);
                        //checkAnimation = true;                      
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
                    timeAttack = startDashTimeAttack;
                    if (GetComponent<PlayerController>().dashing)
                    {
                        GetComponent<PlayerController>().dashHit = true;
                    }
                    else
                    {
                        animator.SetBool("IsAttacking", true);
                        //checkAnimation = true;
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
                    timeAttack = startDashTimeAttack;
                    if (GetComponent<PlayerController>().dashing)
                    {
                        GetComponent<PlayerController>().dashHit = true;
                    }
                    else
                    {
                        animator.SetBool("IsAttacking", true);
                        //checkAnimation = true;
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
                    timeAttack = startDashTimeAttack;
                    if (GetComponent<PlayerController>().dashing)
                    {
                        GetComponent<PlayerController>().dashHit = true;
                    }
                    else
                    {
                        animator.SetBool("IsAttacking", true);
                        //checkAnimation = true;
                    }
                }

                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, MiniMeleeEnemies);
                hitCheck = Physics2D.OverlapCircle(attackPos.position, attackRange, bossEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<MiniMeleeEnemy>().TakeDamage(damage);
                }
                if (hitCheck)
                {
                    timeAttack = startDashTimeAttack;
                    if (GetComponent<PlayerController>().dashing)
                    {
                        GetComponent<PlayerController>().dashHit = true;
                    }
                    else
                    {
                        animator.SetBool("IsAttacking", true);
                        //checkAnimation = true;
                    }
                }
            }           
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            timeAttack -= Time.deltaTime;
        }

       /* if (checkAnimation && tempAnimationTime >= 0)
        {
            animator.SetBool("IsAttacking", true);           
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            checkAnimation = false;
            tempAnimationTime = animationTime;
        }
        */
    }
    
}
