using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _lanes;

    // Start is called before the first frame update
    void Start()
    {
        _lanes = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
