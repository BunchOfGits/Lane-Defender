using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _lanes;

    [SerializeField]
    private List<GameObject> _enemies;

    private int randLane;

    private int randEnemy;

    // Start is called before the first frame update
    void Start()
    {
        _lanes = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        randLane = Random.Range(0, _lanes.Count) - 1;
        randEnemy = Random.Range(0, _enemies.Count) - 1;

        Instantiate(_enemies[randEnemy], (_lanes[randLane]).transform);

    }
}
