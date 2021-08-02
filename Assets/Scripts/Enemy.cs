using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health = 10;

    float speed;

    private NavMeshAgent agent;

    void Start()
    {
        //Init nav mesh agent and set target
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        SetTarget();
    }

    void Update()
    {
        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        float speedMult = health / 10.0f;

        //Lower health means slower enemies
        agent.speed = speed * speedMult;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            //Damage enemy
            health -= other.GetComponent<Bullet>().Damage;
            CheckHealth();
        }
        else if(other.gameObject.tag == "Tower")
        {
            //Remove enemy from list
            GameObject.FindObjectOfType<EnemyManager>().RemoveEnemy(this);

            //Delete this
            Destroy(this.gameObject);
        }
    }

    private void CheckHealth()
    {
        if(health <= 0)
        {
            //Tell turretspawn and UI that enemy died
            GameObject.FindObjectOfType<TurretSpawn>().IncrementKills();
            GameObject.FindObjectOfType<UIController>().DoScore(50);

            //Remove this enemy from enemy list
            GameObject.FindObjectOfType<EnemyManager>().RemoveEnemy(this);

            //Destroy enemy object
            Destroy(this.gameObject);
        }
    }

    private void SetTarget()
    {
        Transform tower = GameObject.Find("Tower").GetComponent<Transform>();
        agent.SetDestination(tower.position);
    }
    
}
