using System;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator hurtPanel;

    private Rigidbody2D rb;
    private Animator anim;
    private Animator cameraAnim;

    private Vector2 moveAmount;
    private SceneTransitions sceneTransitions;

    void Start() {
        anim = GetComponent<Animator>();
        cameraAnim = Camera.main.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransitions = FindAnyObjectByType<SceneTransitions>();

    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        anim.SetBool("isRunning", moveInput != Vector2.zero);

    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount) {
        health -= damageAmount;
        cameraAnim.SetTrigger("shake");
        hurtPanel.SetTrigger("hurt");
        UpdateHealthUI(health);

        if (health <= 0) {
            Destroy(gameObject);
            sceneTransitions.LoadScene("Lose");

        }
    }

    public void EquipWeapon(Weapon weapon) {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weapon, transform.position, transform.rotation, transform);
    }

    void UpdateHealthUI(int currentHealth) {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }

    public void GainHealth(int healAmount) {
        health = Math.Min(5, health + healAmount);
        UpdateHealthUI(health);
    }
}
