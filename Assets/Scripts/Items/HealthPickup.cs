using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount;
    public GameObject effect;

    private bool isConsumed = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !isConsumed) {
            isConsumed = true;
            Instantiate(effect, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Player>().GainHealth(healthAmount);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, audio.clip.length);
        }
    }
}
