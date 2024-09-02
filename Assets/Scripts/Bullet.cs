using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int _bulletSpeed;

    [SerializeField]
    private GameObject _explosion;

    private Rigidbody2D rb2d;

    

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = null;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * _bulletSpeed;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        switch (collision.gameObject.tag)
        {
            case "Border":
                Destroy(this.gameObject);
                return;
            case "Enemy":
                GameObject explosion = Instantiate(_explosion, other.transform);
                explosion.transform.parent = null;
                Destroy(this.gameObject);
                return;
            default: 
                return;
        }

    }
}
