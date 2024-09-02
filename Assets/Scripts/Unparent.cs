using UnityEngine;

public class Unparent : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.parent = null;
    }

}
