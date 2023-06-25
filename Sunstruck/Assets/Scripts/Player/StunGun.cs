using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGun : MonoBehaviour
{
    [SerializeField] private int ammo;
    [SerializeField] private float stunDuration;
    [SerializeField] private float useDuration;

    public bool stunEnemy;
    private bool hit;

    private float stunTimer;
    private float useTimer;
    private Collider2D enemyCollider;
    private Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        stunTimer = stunDuration;
        useTimer = useDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(hit)
        {
            useTimer -= 1 * Time.deltaTime;
        }

        if(stunEnemy)
        {
            stunTimer -= 1 * Time.deltaTime;
        }

        if(stunTimer <= 0)
        {
            stunEnemy = false;
            stunTimer = stunDuration;
            Physics2D.IgnoreCollision(enemyCollider, playerCollider, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyCollider = collision.otherCollider;
        playerCollider = collision.collider;
        if (collision.collider.CompareTag("Enemy") && !GetComponent<HidingMechanism>().isHiding)
        {
            hit = true;
            if(GetComponent<InteractionSystem>().pickUpStunGun && ammo > 0)
            {
                if (Input.GetKeyDown(KeyCode.F) && useTimer > 0)
                {
                    stunEnemy = true;
                    if (stunEnemy)
                        Physics2D.IgnoreCollision(enemyCollider, playerCollider, true);
                    ammo--;
                    useTimer = useDuration;
                }
                else if(useTimer <= 0)
                {
                    transform.position = this.GetComponent<CheckpointRespawn>().respawnPoint;
                    useTimer = useDuration;
                }                 
            }
            else
                transform.position = this.GetComponent<CheckpointRespawn>().respawnPoint;
        }
    }
}
