using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float health = 100;

    public int moneyGain = 50;

    public GameObject deathEffect;

    [Header("Heal Bar")]
    public Image healBar;

    private bool isDead = false;

    [Header("Sonidos Que Emite El Enemigo")]
    public GameObject muerteSonido;
    public GameObject dineroSonido;
    //public AudioSource sfx;


    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow (float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die ()
    {
        isDead = true;
        PlayerStats.Money += moneyGain;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        DeathSounds(muerteSonido, transform.position, 1f);
        DeathSounds(dineroSonido, transform.position, 1f);

        WavesSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    void DeathSounds(GameObject prefab, Vector3 posición, float duración = 5f)
    {
        Destroy(Instantiate(muerteSonido, transform.position, Quaternion.identity), 1f);
        Destroy(Instantiate(dineroSonido, transform.position, Quaternion.identity), 1f);
    }

}
