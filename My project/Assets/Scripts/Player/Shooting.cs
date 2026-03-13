using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public Vector3 mousePosition;
    private Rigidbody2D rb;
    public float force;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePosition = PlayerAimWeapon.GetMouseWorldPosition();
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized* force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Update is called once per frame  
    void Update()
    {
        
    }
    private IEnumerator timer()
    {
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }

}
