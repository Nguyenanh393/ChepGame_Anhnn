using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class Kunai : MonoBehaviour
{
    public GameObject hitVFX;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDesPawn), 4f);
    }

    public void OnDesPawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().OnHit(30f);
            Instantiate(hitVFX, transform.position, Quaternion.identity);
            OnDesPawn();
        }
    }
}
