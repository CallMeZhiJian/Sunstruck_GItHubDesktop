using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGun : MonoBehaviour
{
    [SerializeField] private int ammo;
    [SerializeField] private float stunDuration;

    public bool stunEnemy;

    private float stunTimer;
    private bool checkOnHand;
    private Collider2D enemyCollider;
    private Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        stunTimer = stunDuration;
    }

    // Update is called once per frame
    void Update()
    {
        checkOnHand = GetComponent<InteractionSystem>().pickUpStunGun;
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
            if(checkOnHand && ammo > 0)
            {
                //if(Input.GetKeyDown(KeyCode.F))
                //{
                    stunEnemy = true;
                    if (stunEnemy)
                        Physics2D.IgnoreCollision(enemyCollider, playerCollider, true);
                    ammo--;
                //}
                //else
                //    transform.position = this.GetComponent<CheckpointRespawn>().respawnPoint;
            }
            else
                transform.position = this.GetComponent<CheckpointRespawn>().respawnPoint;
        }
    }
}
