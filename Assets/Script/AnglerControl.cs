using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerControl : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        attackCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
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
                transform.position = new Vector3(transform.position.x + Mathf.Cos(angle) * speed * attackSpeed, transform.position.y + Mathf.Sin(angle) * speed * attackSpeed, transform.position.z);
            }
            else if (attackCounter >= attackDuration)
            {
                attackCounter = 0;
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
        }
    }
}
