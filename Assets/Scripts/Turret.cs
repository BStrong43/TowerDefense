using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    float shotCooldown = 1f; //Time in seconds between shots
    private float cooldown;

    public GameObject bullet;
    public Transform bulletSpawn;
    private Transform target;
    
    void Start()
    {
        ResetCooldown();
    }

    void Update()
    {
        if(target == null)
        {
            GetClosestEnemy();
        }
        else
        {
            FaceEnemy();
            CheckShoot();
        }
        
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    private void ResetCooldown()
    {
        cooldown = shotCooldown;
    }

    private void GetClosestEnemy()
    {
        List<Enemy> enemies = GameObject.FindObjectOfType<EnemyManager>().GetEnemies();
        
        if(enemies.Count == 0)
        {
            target = null;
            return;
        }

        Enemy closest = enemies[0];
        float dist = Vector3.Distance(gameObject.transform.position, closest.transform.position);

        foreach (Enemy i in enemies)
        {
            if (closest == i)//Skips first iteration, done above loop
                continue;

            float nextDist = Vector3.Distance(gameObject.transform.position, i.transform.position);

            if (dist > nextDist)
            {
                closest = i;
                dist = nextDist;
            }
        }

        target = closest.transform;
    }

    private void FaceEnemy()
    {
        //This made it tilt
        //transform.LookAt(target, Vector3.up);

        //Turret will look at target without "tilting"
        Vector3 lookDir = target.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    private void CheckShoot()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            Shoot();
            ResetCooldown();
        }
    }

    private void Shoot()
    {
        GameObject proj = GameObject.Instantiate(bullet, bulletSpawn.position, transform.rotation);
        
        //Give projectile correct rotation
        //proj.transform.rotation = transform.rotation;
    }
}
