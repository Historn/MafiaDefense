using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamino2 : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float health = 100;

    public int moneyGain = 50;

    public GameObject deathEffect;

    private bool isDead = false;

    [Header("Sonidos Que Emite El Enemigo")]
    public AudioSource sfx;
    public AudioClip sonidoDinero;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage2(float amount)
    {
        health -= amount;

        if (health <= 0 && !isDead)
        {
            sfx.PlayOneShot(sonidoDinero);
            Die();
        }
    }

    public void Slow2(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;
        PlayerStats.Money += moneyGain;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WavesSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }

}
