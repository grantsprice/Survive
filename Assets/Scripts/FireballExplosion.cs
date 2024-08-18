using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosion : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] Vector2 fbExplosionAttackSize = new Vector2(0.2f, 0.2f);
    [SerializeField] int fbExplosionDamage = 4;
    private float timer = 0.4f;
    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        Attack();
    }
    private void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, fbExplosionAttackSize, 0f);
        ApplyDamage(colliders);
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            Enemy e = colliders[i].GetComponent<Enemy>();
            if (e != null)
            {
                colliders[i].GetComponent<Enemy>().TakeDamage(fbExplosionDamage);
            }
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Destroy(gameObject);
        }
        else
        {
            Attack();
        }
    }
}
