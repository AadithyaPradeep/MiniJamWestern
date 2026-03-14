using System;
using System.Collections;
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
    public float Range;
    public float speed;
    public Animator aimAnimator;
    public GameObject bullet;
    public GameObject gunSmoke;
    public Transform bulletSpawn;
    private bool canShoot = true;
    public float fireDelay;
    public float aimDelay;
    private Vector3 aimPos;
    
    private void Start()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        inRange = false;
    }
    private void FixedUpdate()
    {
        
        plDist = Vector3.Distance(transform.position, Player.transform.position);
        if (plDist < Range )
        {
            inRange = true;
        }
        if(!inRange)
        {
            aim();
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
        if(inRange && canShoot)
        {
            canShoot = false;
            animator.SetBool("Running", false);
            StartCoroutine("Shoot");
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
    private IEnumerator Shoot()
    {
        aim();
        aimPos = Player.transform.position;
        yield return new WaitForSeconds(aimDelay);
        
            aimAnimator.SetTrigger("Shoot");
            if (bullet != null)
            {
                GameObject newbullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                newbullet.GetComponent<Bullet>().target = aimPos;
                newbullet.GetComponent<Bullet>().enemyBullet = true;
                GameObject newgunSmoke = Instantiate(gunSmoke, bulletSpawn.position, Quaternion.identity);
                newgunSmoke.GetComponent<ParticleSystem>().Play();
                
                
            }
            StartCoroutine("timer");
            inRange = false;
        
    }
    private IEnumerator timer()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }
    

}
