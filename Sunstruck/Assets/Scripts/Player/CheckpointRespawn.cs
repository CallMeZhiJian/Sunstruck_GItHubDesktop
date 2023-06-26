using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointRespawn : MonoBehaviour
{
    [SerializeField] private GameObject deadSpace;
    [SerializeField] private GameObject checkpoint;

    public Vector3 respawnPoint;
    private bool isCheckPoint;

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        deadSpace.transform.position = new Vector2(transform.position.x, deadSpace.transform.position.y);
        if(isCheckPoint && Input.GetKeyDown(KeyCode.F))
        {
            respawnPoint = transform.position;
            checkpoint.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Void"))
        {
            transform.position = respawnPoint;
        }

        if(collision.CompareTag("Checkpoint"))
        {
            isCheckPoint = true;
        }
        
        if(collision.CompareTag("NextScene") && GetComponent<InteractionSystem>().pickUpStunGun)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        else
        {
            print("Pick up the fucking stun gun!!!!!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Checkpoint"))
        {
            isCheckPoint = false;
        }      
    } 
}
