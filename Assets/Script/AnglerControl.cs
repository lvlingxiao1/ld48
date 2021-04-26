using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerControl : MonoBehaviour, Respawnable
{
    Transform player;

    public float searchDistance = 25;
    public float speed = 0.05f;
    int attackCounter;
    int attackCD = 150;
    int attackCharge = 170;
    int attackDuration = 210;
    float attackSpeed = 10f;
    float targetRotationY;
    float angle;
    public int flashDuration = 66;
    int flashCounter;
    Vector3 flashSpeed;
    AudioSource attackSound;
    public Vector3 respawnPos = new Vector3(0, 0, 0);
    bool attacked;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        attackSound = GetComponent<AudioSource>();
        respawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (flashCounter > 0){
            flashCounter --;
            transform.position += flashSpeed;
            return;
        }
        Vector3 diff = player.position - transform.position;
        if (Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.y, 2) < searchDistance)
        {
            attackCounter++;
            if (attackCounter >= attackCD && attackCounter < attackCharge)
            {
                angle = Mathf.Atan2(diff.y, diff.x);
                if (Mathf.Abs(angle) > Mathf.PI / 2)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, 180 - angle * Mathf.Rad2Deg);
                }
                else
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, angle * Mathf.Rad2Deg);
                }

            }
            else if (attackCounter >= attackCharge && attackCounter < attackDuration)
            {
                if (!attacked)
                {
                    attacked = true;
                    attackSound.Play();
                }
                transform.position = new Vector3(transform.position.x + Mathf.Cos(angle) * speed * attackSpeed, transform.position.y + Mathf.Sin(angle) * speed * attackSpeed, transform.position.z);
            }
            else if (attackCounter >= attackDuration)
            {
                attackCounter = 0;
                attacked = false;
            }
            if (attackCounter < attackCD)
            {
                angle = Mathf.Atan2(diff.y, diff.x);
                if (Mathf.Abs(angle) > Mathf.PI / 2)
                {
                    targetRotationY = 180;
                    float rotationY = Mathf.Lerp(transform.eulerAngles.y, targetRotationY, 0.1f);
                    transform.eulerAngles = new Vector3(0, rotationY, 180 - angle * Mathf.Rad2Deg);
                }
                else
                {
                    targetRotationY = 0;
                    float rotationY = Mathf.Lerp(transform.eulerAngles.y, targetRotationY, 0.1f);
                    transform.eulerAngles = new Vector3(0, rotationY, angle * Mathf.Rad2Deg);
                }
                transform.position = new Vector3(transform.position.x + Mathf.Cos(angle) * speed, transform.position.y + Mathf.Sin(angle) * speed, transform.position.z);
            }

        }
        else
        {
            attackCounter = attackCD;
            attacked = false;
        }
    }

    public void StartFlash(Vector3 source){
        flashCounter = flashDuration;
        Vector3 diff = transform.position - source;
        angle = Mathf.Atan2(diff.y, diff.x);
        flashSpeed = 3 * speed * (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0));
        if (Mathf.Abs(angle) > Mathf.PI / 2)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, 180 - angle * Mathf.Rad2Deg);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, angle * Mathf.Rad2Deg);
        }
    }

    public void respawn()
    {
        attackCounter = 0;
        flashCounter = 0;
        attacked = false;
        transform.position = respawnPos;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
