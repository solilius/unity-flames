using UnityEngine.UI;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;
    public Enemy[] enemies;
    public float spawnOffset;
    public int damage;
    public GameObject bloodEffect;
    public GameObject deathEffect;
    public Slider healthBar;

    private int halfHealth;
    private Animator anim;
    private SceneTransitions sceneTransitions;

    void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindAnyObjectByType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        sceneTransitions = FindAnyObjectByType<SceneTransitions>();
    }

    public void TakeDamage(int damageAmount) {
        health -= damageAmount;
        healthBar.value = health;
        print("health: " + health);
        print(healthBar.value);
        
        if (health <= 0) {
            KillBoss();
            sceneTransitions.LoadScene("Win");
        }

        if (health <= halfHealth) {
            anim.SetTrigger("enraged");
        }

        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, spawnOffset), transform.rotation, transform);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void KillBoss() {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            GameObject blood = Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(blood, 5f);
            
            AudioSource deathSound = GetComponent<AudioSource>();
            deathSound.Play();            
            transform.position = new Vector2(-1000, -1000);
            Destroy(gameObject, deathSound.clip.length);
            healthBar.gameObject.SetActive(false);
    }
}
