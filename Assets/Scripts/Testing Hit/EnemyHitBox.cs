using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{

    public int health = 200;
    public GameObject deathParticle;
    public Transform particleHolder;

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            //can add death particle
            deathParticle = (GameObject)(Instantiate(deathParticle, particleHolder.transform.position, Quaternion.identity));
            Destroy(deathParticle, 2.8f);
            Destroy(gameObject);
        }
    }
}
