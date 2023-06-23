using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingMechanism : MonoBehaviour
{
    public bool isHiding;
    private bool hideAllow;

    // Update is called once per frame
    void Update()
    {
        if (hideAllow && Input.GetKeyDown(KeyCode.F))
        {
            isHiding = true;
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            this.GetComponent<BoxCollider2D>().isTrigger = false;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            this.GetComponent<SpriteRenderer>().enabled = true;
            isHiding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hidepoint"))
        {   
            hideAllow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hidepoint"))
        {
            hideAllow = false;
        }
    }
}
