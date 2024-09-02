using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerInput input;
    private GameManager GM;
    private InputAction shoot;

    [SerializeField]
    private List<GameObject> _playerNodes;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private GameObject _bullet;

    private GameObject barrel;

    private int currentNode;

    private float moveDirection;

    private Coroutine bulletTime;

    private bool bulletFired;

    [SerializeField]
    private float _timer;

    private float origTimer;

    [SerializeField]
    private AudioSource _shootSound;

    // Start is called before the first frame update
    void Start()
    {
        input = gameObject.GetComponent<PlayerInput>();
        input.currentActionMap.Enable();
        shoot = input.currentActionMap.FindAction("Fire");

        shoot.started += shoot_started;
        shoot.canceled += shoot_canceled;

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentNode = 0;
        gameObject.transform.position = _playerNodes[currentNode].transform.position;

        barrel = GameObject.Find("BarrelTransform");

        origTimer = _timer;

    }

    private void shoot_started(InputAction.CallbackContext obj)
    {
        if (GM.IsPlaying)
        {
            bulletFired = true;
            if (bulletTime == null)
            {
                bulletTime = StartCoroutine(bulletDelay());
            }

        }
    }

    private void shoot_canceled(InputAction.CallbackContext context)
    {
        bulletFired = false;
    }

    void OnMovement(InputValue iValue)
    {
        if (GM.IsPlaying)
        {
            moveDirection = iValue.Get<float>();
            if (currentNode == 0 && moveDirection > 0)
            {
                currentNode += ((int)moveDirection);
                gameObject.transform.position = _playerNodes[currentNode].transform.position;
                return;
            }
            if (currentNode == 4 && moveDirection < 0)
            {
                currentNode += ((int)moveDirection);
                gameObject.transform.position = _playerNodes[currentNode].transform.position;
                return;
            }
            if (currentNode > 0 && currentNode < 4)
            {
                currentNode += ((int)moveDirection);
                gameObject.transform.position = _playerNodes[currentNode].transform.position;
                return;
            }
        }
    }


    private void FireBullet()
    {
        Instantiate(_explosion, barrel.transform);
        _shootSound.Play();
        Instantiate(_bullet, barrel.transform);
        _timer = origTimer;
    }

    private IEnumerator bulletDelay()
    {
        FireBullet();

        while (bulletFired == true || _timer > 0)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0 && bulletFired == true)
            {
                FireBullet();
            }
            yield return null;

        }
        _timer = origTimer;

        bulletTime = null;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            GM.LoseLife();
            Destroy(collision.gameObject);
        }
    }


}
