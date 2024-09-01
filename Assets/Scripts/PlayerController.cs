using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerInput input;
    private GameManager GM;

    [SerializeField]
    private List<GameObject> _playerNodes;

    private int currentNode;

    private float moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        input = gameObject.GetComponent<PlayerInput>();
        input.currentActionMap.Enable();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentNode = 0;
        gameObject.transform.position = _playerNodes[currentNode].transform.position;

    }

    void OnMovement(InputValue iValue)
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

    void OnFire()
    {

    }

    /// <summary>
    /// "Nah, I'd win"
    /// </summary>
    void OnDomain()
    {

    }

    void Pause()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
