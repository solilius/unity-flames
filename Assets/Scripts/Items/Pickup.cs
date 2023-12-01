using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weapon;
    public GameObject effect;

    private bool isConsumed = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !isConsumed) {
            isConsumed = true;
            Instantiate(effect, transform.position, Quaternion.identity);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, audio.clip.length);
            collision.gameObject.GetComponent<Player>().EquipWeapon(weapon);
        }
    }
}
