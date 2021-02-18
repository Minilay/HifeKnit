using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateSpeed;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            print("Hit");
            GameManager.S.knifeDecrease();
        }
        if(!GameManager.S.dying)
        {
            collision.gameObject.transform.SetParent(transform);
            collision.transform.tag = "Knife";
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            collision.gameObject.GetComponent<Knife>().enabled = false;
        }
        GameManager.S.Spawn();

    }
    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
