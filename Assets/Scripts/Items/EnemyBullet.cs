using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public GameObject effect;

    private Player playerScript;
    private Vector2 targetPosition;

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }


    void Update() {
        if (Vector2.Distance(transform.position, targetPosition) > .1f) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                playerScript.TakeDamage(damage);
                Destroy(gameObject);

            }
    }  

    private void OnDestroy() {
        Instantiate(effect, transform.position, Quaternion.identity);
    }      
}
