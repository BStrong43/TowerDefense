using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private GameController gc;
    private float health = 20;
    private float maxHealth;

    private void Start()
    {
        maxHealth = health;
        gc = FindObjectOfType<GameController>();
    }

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public void TickHealth()
    {
        health -= 1;
        float p = ((health / maxHealth) * 100);
        gc.DoHealth((int)p);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(health <= 0)
        {
            gc.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            TickHealth();
        }
    }
}
