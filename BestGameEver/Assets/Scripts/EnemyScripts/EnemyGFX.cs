using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    private bool facingRight = false;

    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            if (!facingRight)
            {
                Flip();
            }
        }
        else if(aiPath.desiredVelocity.x <= 0.01f)
        {
            if (facingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
       // healthBarCheck = facingRight;
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;      
        transform.localScale = Scaler;
        /*
        if ((healthBarCheck && !facingRight) || (!healthBarCheck && facingRight))
        {
            Scaler = bar.localScale;
            Scaler.x *= -1;
            bar.localScale = Scaler;
        }
        */
    }
}
