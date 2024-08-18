using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Fireball : MonoBehaviour
{
    [HideInInspector]
    public Vector3 movementVector = new Vector3();
    Rigidbody2D myRigidBody;
    [SerializeField] float speed = 3f;
    [SerializeField] Vector2 fbAttackSize = new Vector2(2f, 2f);
    [SerializeField] int fbDamage = 4;
    [SerializeField] GameObject explosion;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        myRigidBody.velocity = movementVector * speed;
        Attack();
    }

    private void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, fbAttackSize, 0f);
        ApplyDamage(colliders);
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            Enemy e = colliders[i].GetComponent<Enemy>();
            if (e != null)
            {
                colliders[i].GetComponent<Enemy>().TakeDamage(fbDamage);
                Destroy(gameObject);
                explode();
            }
            else if (colliders[i].GetComponent<Environment>() != null)
            {
                Destroy(gameObject);
                explode();
            }
        }
    }

    private void explode()
    {
        GameObject myExplosion = Instantiate(explosion);
        FireballExplosion explo = myExplosion.GetComponent<FireballExplosion>();
        explo.transform.position = transform.position;
    }

    internal void setRotation()
    {
        // Calculate the angle in degrees between the movement vector and the right vector
        float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;

        // Rotate the fireball to face the direction of its movement
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
}
