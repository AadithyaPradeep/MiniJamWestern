using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    public GameObject Player;
    public Transform aimTransform;
    public Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private bool inRange;
    private float plDist;
    private float Range;
    public float speed;
    private void Start()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        inRange = false;
    }
    private void FixedUpdate()
    {
        aim();
        plDist = Vector3.Distance(transform.position, Player.transform.position);
        if (plDist < Range)
        {
            inRange = true;
        }
        if(!inRange)
        {
            transform.position += (Player.transform.position - aimTransform.position).normalized*speed*Time.deltaTime;
            if((Player.transform.position.x - transform.position.x) < 0)
            {
                sp.flipX = true;
                animator.SetBool("Running", true);

            }
            if ((Player.transform.position.x - transform.position.x) > 0)
            {
                sp.flipX = false;
                animator.SetBool("Running", true);
            }
        }
        if(inRange)
        {

        }

    }
    private void aim()
    {
        Vector3 aimDirection = (Player.transform.position - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
        }
        aimTransform.localScale = aimLocalScale;
    }
}
