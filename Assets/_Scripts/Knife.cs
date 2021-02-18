using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Knife : MonoBehaviour
{
    private Rigidbody2D rigid;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Knife") && transform.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            rigid = GetComponent<Rigidbody2D>();
            print("GameOver");

            rigid.constraints = RigidbodyConstraints2D.None;
            rigid.velocity = Vector3.zero;
            rigid.AddTorque(Random.Range(800, 1200));
            int power = (Random.value > 0.5f) ? 50 : -50;
            rigid.AddForce(Vector3.right * power);
            rigid.gravityScale = 1.5f;
            StartCoroutine(GameManager.S.reload());
        }
    }
}
