using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collider : MonoBehaviour
{
    Collider2D collider_;
    Player_Controller controller_;

    void Awake()
    {
        collider_ = (Collider2D)GetComponent(typeof(Collider2D));
    }

    public void Set_Parent_Controller(Player_Controller controller)
    {
        controller_ = controller;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //controller_.Set_Positional_State(Player_Controller.AREA_STATE.GROUND);
    }
}
