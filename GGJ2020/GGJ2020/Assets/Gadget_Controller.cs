using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadget_Controller : MonoBehaviour
{
    public KeyCode shoot_;

    GameObject bullet_Template_;

    GameObject[] bullet_Pool_;
    int pool_Size_;

    void Start()
    {
        shoot_ = KeyCode.Space;
        bullet_Pool_ = new GameObject[pool_Size_];
        for(int i = 0; i < pool_Size_; i++)
        {
            bullet_Pool_[i] = bullet_Template_;
        }
    }

    void Update()
    {
        if (Input.GetKey(shoot_))
        {

        }
    }
}
