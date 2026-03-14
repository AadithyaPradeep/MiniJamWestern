using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 target;
    private Vector3 direction;
    public bool enemyBullet;
    private Rigidbody2D rb;
    public float force;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        direction = target  - transform.position;
        Vector3 rotation = transform.position - target;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized* force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
    private void Awake()
    {
        StartCoroutine("timer");
    }
    // Update is called once per frame  
    void Update()
    {
        
    }
    private IEnumerator timer()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {if (!enemyBullet)
        {
            
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(this.gameObject);
                collision.gameObject.GetComponent<Lives>().Hit(direction.normalized);
            }
        }
    }

}
