using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyProt : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject SeekerDetect;
    private Rigidbody2D rb;
    private Animation anim;
    private Transform currentPoint;
    public Transform castPoint;
    public float speed;
    public GameObject player;
    private Vector2 endPos;
    private Vector2 startPos;
    public Transform Enemy;

    public Transform playerTransform;
    public string currentState;
    public float agroRange;
    //public float chaseDistance;
    public bool isPlayerSeek;
    public bool isFacingLeft = false;
    private Vector3 Distance = new Vector3(3,0,0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        currentPoint = pointB.transform;
        currentState = "walk";
        //anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<StunGun>().hit == true)
        {
            if (player.GetComponent<StunGun>().stunEnemy)
            {
                rb.velocity = new Vector2(0, 0);
                //Animation
            }
        }
        else
        {
            if (CanSeekPlayer(agroRange))
            {
                chasing();
            }
            else
            {
                walkAround();
            }
            if (isFacingLeft)
            {
                endPos = Enemy.position + (Vector3.right * agroRange);
                //Debug.Log("turn right");
                //Debug.Log(endPos);
            }
            else
            {
                endPos = Enemy.position + (Vector3.left * agroRange);
                //Debug.Log("turn left");
                //Debug.Log(endPos);
            }
        }
        
    }

    /*public void chase()
    {
        if (isChasing)
        {
            

            if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
            {
                isChasing = false;
            }
            else if (Vector2.Distance(transform.position, playerTransform.position) > 0.5f & currentPoint == pointB.transform)
            {
                isChasing = false;
            }
            else if (Vector2.Distance(transform.position, playerTransform.position) < 0.5f & currentPoint == pointA.transform)
            {
                isChasing = false;
            }
        }


        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
            
        }
    }*/

    bool CanSeekPlayer(float distance)
    {
        bool val = false;
        float castDisk = distance;

        //if(isFacingLeft)
        //{
            //castDisk = -distance;
        //}
        //endPos = Enemy.position + Vector3.left * castDisk;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            //Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            //Debug.DrawLine(castPoint.position, endPos,Color.blue);
        }
        return val;

    }

    public void chasing()
    {
        /*if(!isPlayerSeek)
        {
            currentState = "walk";
        }
        else
        {*/
            if (transform.position.x > playerTransform.position.x)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else if (transform.position.x < playerTransform.position.x)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        //}
    }

    public void walkAround()
    {
        /*if (isPlayerSeek)
        {
            currentState = "chase";
        }
        else
        {*/
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f & currentPoint == pointB.transform)
            {
                isFacingLeft = false;
                flip();
                currentPoint = pointA.transform;
            }
            else if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f & currentPoint == pointA.transform)
            {
                isFacingLeft = true;
                flip();
                currentPoint = pointB.transform;
            }
        //}
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject == player)
        {
            isPlayerSeek = true;
        }    
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerSeek = false;
        }
    }*/
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        //Debug.Log("flip1");
        localScale.x *= -1;
        //Debug.Log("flip2");
        transform.localScale = localScale;
        //Debug.Log("flip3");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.otherCollider, collision.collider);
        }
    }
}
