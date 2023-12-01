using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;
    public GameObject deathEffect;
    public GameObject bloodStain;

    public int dropChance;
    public GameObject[] drops;

    [HideInInspector]
    public Transform player;

    public virtual void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damageAmount) {
        health -= damageAmount;
        if (health <= 0) {
            DropItem();
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            GameObject blood = Instantiate(bloodStain, transform.position, Quaternion.identity);
            Destroy(blood, 2f);
            Destroy(gameObject);
        }
    }

    private void DropItem() {
        int randomNumber = Random.Range(0, 101);
        if (randomNumber < dropChance) {
            GameObject drop = drops[Random.Range(0, drops.Length)];
            Instantiate(drop, transform.position, transform.rotation);
        }
    }
}
 