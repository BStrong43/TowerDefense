using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float lifetime = 10f;

    private Rigidbody rb;

    private int damage = 1;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void doLifeTimer()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag != "Player")
            Destroy(this.gameObject);
    }
}
