using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{
   private Rigidbody2D rb;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
       

         if(collision.gameObject.CompareTag("Ball")){
            collision.gameObject.SetActive(false);
            // Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Gift")){
            Destroy(collision.gameObject);
        }
    }
}
