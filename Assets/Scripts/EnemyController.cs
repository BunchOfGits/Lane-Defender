using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int _health;

    [SerializeField]
    private int _speed;

    [SerializeField]
    private AudioSource _enemyDamageSound;

    private Rigidbody2D rb2d;
    private Animator anim;

    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
        rb2d.velocity = Vector2.left * _speed;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            _health--;
            StartCoroutine(StunEnemy());
            anim.SetTrigger("Hit");
            if(_health > 0)
            {
                _enemyDamageSound.Play();
            }
            if (_health == 0)
            {
                GM.IncreaseScore();
                Destroy(this.gameObject);
            }
        }
        if (collision.transform.tag == "Border")
        {
            GM.LoseLife();
            Destroy(this.gameObject);
        }
    }

    private IEnumerator StunEnemy()
    {
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        rb2d.velocity = Vector2.left * _speed;
    }
}
