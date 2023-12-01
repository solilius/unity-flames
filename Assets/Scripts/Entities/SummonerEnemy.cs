using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerEnemy : Enemy
{
    public float attackSpeed;
    public float stopDistance;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float timeBetweenSummons;
    public Enemy spawn;

    private float summonTime;
    private Vector2 targetPosition;
    private Animator anim;
    private float attackTime;

    public override void Start() {
        base.Start();
        float randonX = Random.Range(minX, maxX);
        float randonY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randonX, randonY);
        anim = GetComponent<Animator>();
    }


    void Update() {
        if (player != null) {
            if (Vector2.Distance(transform.position, player.position) < stopDistance) {
                if (Time.time > attackTime) {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }

            if (Vector2.Distance(transform.position, targetPosition) > .5f) {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else {
                anim.SetBool("isRunning", false);
                if (Time.time >= summonTime) {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }
        }
    }

    public void Summon() {
        if (player != null) {
            Instantiate(spawn, transform.position, transform.rotation);
        }
    }

    IEnumerator Attack() {
        player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targerPosition = player.position;

        float percent = 0;
        while (percent <= 1) {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targerPosition, formula);
            yield return null;
        }
    }
}
