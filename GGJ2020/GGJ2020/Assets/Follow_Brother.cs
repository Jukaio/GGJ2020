using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Brother : MonoBehaviour
{
    public GameObject brother_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = brother_.transform.position;
    }
}
