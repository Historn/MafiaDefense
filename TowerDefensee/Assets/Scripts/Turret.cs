using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Atributos")]
    public float range = 1f;

    [Header("Balas")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Rayos")]
    public bool useLightning = false;

    public float damageOverTime = 30;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    [Header("Ondas Ralentizantes")]
    public bool useSlow = false;
    public float slowAmount = .5f;

    [Header("Assets a Asignar")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 5f;


    public Transform firePoint;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget() //Busca un enemigo y detecta el más cercano
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        // Deja de buscar enemigos si no hay enemigo
        if (target == null)
        {
            if (useLightning)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    
                }
                    
            }

            return;
        }       
            

        FijarEnemigo();

        if (useLightning)
        {
            Rayos();
        }
        else if (useSlow)
        {
            SlowingMusic();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void FijarEnemigo()
    {
        //Seguiminento de enemigos
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;    //Cambia el factor de rotación a Eulers
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);             //Rotar en el eje Y. Dejamos el eje X - Z en 0 para que no se mueva.
    }

    void Rayos()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();       
        }
            

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized * .3f;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    
    }

    void SlowingMusic()
    {
        targetEnemy.Slow(slowAmount);
        Debug.Log("Slowea");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
