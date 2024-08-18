using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballWeapon : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    [SerializeField] float timeToAttack;
    [SerializeField] EnemiesManager enemyManager;

    float timer = 4f;
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        timer = timeToAttack;

        GameObject newFireball = Instantiate(fireball);
        Fireball fb = newFireball.GetComponent<Fireball>();
        Vector3 playerPosition = GetComponentInParent<Transform>().position;
        fb.transform.position = playerPosition;

        List<GameObject> enemies = enemyManager.enemies;
        Vector3 closestEnemy = new Vector3(1000, 1000, 0);
        for(int i = 0; i < enemies.Count; i++)
        {
            GameObject currEnemy = enemies[i];
            if((currEnemy.transform.position - playerPosition).sqrMagnitude < closestEnemy.sqrMagnitude)
            {
                closestEnemy = currEnemy.transform.position;
            }
        }
        fb.movementVector = (closestEnemy - playerPosition).normalized;
        fb.setRotation();
    }
}
