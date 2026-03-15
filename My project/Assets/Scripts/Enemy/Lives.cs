using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Lives : MonoBehaviour
{
    public GameObject[] lives;
    public int currentLife = 0;
    public Animator anim;
    public float knockback;
    public float deathTime = 0.5f;
    public GameObject deathParticle;
    private bool dead = false;
    public GameUI gameUI;
    private void Start()
    {
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>() ;
    }
    public void Hit(Vector3 dir)
    {
        lives[currentLife].GetComponent<SpriteRenderer>().color = Color.darkGray;
        anim.SetTrigger("Hit");
        transform.position += dir * knockback;
        currentLife++;
    }
    private void Update()
    {
        if(currentLife ==  lives.Length && !dead)
        {
            dead = true;
            StartCoroutine("Death");
        }
    }
    private IEnumerator Death()
    {
        gameUI.score += 5;
        anim.SetTrigger("Death");
        this.GetComponent<EnemyScript>().enabled = false;
        yield return new WaitForSeconds(deathTime);
        this.gameObject.SetActive(false);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
      
    }
}
