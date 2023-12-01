using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject explosion;
    public int damage;

    void Start()
    {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
