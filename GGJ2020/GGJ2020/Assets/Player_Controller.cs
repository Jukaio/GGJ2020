using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using LEVEL = Player_Data.LEVEL;
using ERROR_STATE = Player_Data.ERROR_STATE;
using AREA_STATE = Player_Data.AREA_STATE;
using AIR_STATE = Player_Data.AIR_STATE;
using GROUND_STATE = Player_Data.GROUND_STATE;

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rigid_Body_;
    Vector2 current_Velocity_ = new Vector2();
    Vector2 last_Velocity_ = new Vector2();
    Animator animator_;
    SpriteRenderer renderer_;

    float drop_Cooldown_ = 0.0f;
    public bool double_Jump_ = false;
    public bool prev_Jump = false;

    public Collider2D collider_;

    public KeyCode move_Left_;
    public KeyCode move_Right_;
    public KeyCode jump_;
    public KeyCode drop_;

    public float speed_;
    public float jump_Height_;

    public LEVEL current_Level_;
    public ERROR_STATE current_Error_State_;
    public AREA_STATE current_Area_State_;
    public AIR_STATE current_Air_State_;
    public GROUND_STATE current_Ground_State_;

    public void Set_Positional_State(AREA_STATE state)
    {
        current_Area_State_ = state;
    }

    void Start()
    {
        move_Left_ = KeyCode.A;
        move_Right_ = KeyCode.D;
        jump_ = KeyCode.W;
        drop_ = KeyCode.S;

        rigid_Body_ = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        collider_ = (Collider2D)GetComponent(typeof(Collider2D));
        animator_ = (Animator)GetComponent(typeof(Animator));
        renderer_ = (SpriteRenderer)GetComponent(typeof(SpriteRenderer));
    }

    void FixedUpdate()
    {
        current_Velocity_ = rigid_Body_.velocity;

        if (current_Velocity_.y <= -12.6f)
            current_Velocity_.y = -12.6f;

        //rigid_Body_.velocity = current_Velocity_;

        Decide();
        Act();

        last_Velocity_ = current_Velocity_;

        if (drop_Cooldown_ > 0)
            drop_Cooldown_ -= Time.fixedDeltaTime;
        else if (drop_Cooldown_ < 0)
            drop_Cooldown_ = 0;

        prev_Jump = Input.GetKey(jump_);
    }

    void Decide()
    {
        switch (current_Area_State_)
        {
            case AREA_STATE.AIR:

                if (Input.GetKey(jump_) && double_Jump_ == false && prev_Jump == false)
                {
                    current_Air_State_ = AIR_STATE.DOUBLE_JUMP;
                    return;
                }

                if (last_Velocity_.y > 0)
                {
                    if (Input.GetKey(move_Left_) ^ Input.GetKey(move_Right_)) // Move left or right but not jump
                    {
                        if (Input.GetKey(move_Left_))
                        {
                            current_Air_State_ = AIR_STATE.MOVE_LEFT_AIR;
                        }
                        else if (Input.GetKey(move_Right_))
                        {
                            current_Air_State_ = AIR_STATE.MOVE_RIGHT_AIR;
                        }
                        return;
                    }
                    else
                    {
                        current_Air_State_ = AIR_STATE.RISE;
                    }
                }
                else
                {
                    if (Input.GetKey(move_Left_) ^ Input.GetKey(move_Right_)) // Move left or right but not jump
                    {
                        if (Input.GetKey(move_Left_))
                        {
                            current_Air_State_ = AIR_STATE.FALL_LEFT;
                        }
                        else
                        {
                            current_Air_State_ = AIR_STATE.FALL_RIGHT;
                        }
                        return;
                    }
                    else
                    {
                        current_Air_State_ = AIR_STATE.FALL;
                    }
                }

                return;

            case AREA_STATE.GROUND:

                if (Input.GetKey(jump_) && prev_Jump == false)
                {
                    if (Input.GetKey(move_Left_) ^ Input.GetKey(move_Right_))
                    {
                        if (Input.GetKey(move_Left_))
                        {
                            current_Ground_State_ = GROUND_STATE.JUMP_LEFT_ENTER;
                        }
                        else if (Input.GetKey(move_Right_))
                        {
                            current_Ground_State_ = GROUND_STATE.JUMP_RIGHT_ENTER;
                        }
                    }
                    else
                    {
                        current_Ground_State_ = GROUND_STATE.JUMP_ENTER;
                    }
                    return;
                }
                else if (Input.GetKey(drop_) && drop_Cooldown_ == 0 && current_Level_ == LEVEL.PASSABLE)
                {
                    if (Input.GetKey(move_Left_) ^ Input.GetKey(move_Right_))
                    {
                        if (Input.GetKey(move_Left_))
                        {
                            current_Ground_State_ = GROUND_STATE.DROP_ENTER_LEFT;
                        }
                        else if (Input.GetKey(move_Right_))
                        {
                            current_Ground_State_ = GROUND_STATE.DROP_ENTER_RIGHT;
                        }
                    }
                    else
                    {
                        current_Ground_State_ = GROUND_STATE.DROP_ENTER;
                    }
                    return;
                }
                else
                {
                    if (Input.GetKey(move_Left_) ^ Input.GetKey(move_Right_))
                    {
                        if (Input.GetKey(move_Left_))
                        {
                            current_Ground_State_ = GROUND_STATE.MOVE_LEFT_GROUND;
                        }
                        else if (Input.GetKey(move_Right_))
                        {
                            current_Ground_State_ = GROUND_STATE.MOVE_RIGHT_GROUND;
                        }

                        return;
                    }
                }

                current_Ground_State_ = GROUND_STATE.IDLE_GROUND;

                return;
        }

        current_Error_State_ = ERROR_STATE.ERROR;
    }

    void Act()
    {
        switch(current_Area_State_)
        {
            case AREA_STATE.AIR:

                switch (current_Air_State_)
                {
                    case AIR_STATE.BUG:

                        break;

                    case AIR_STATE.RISE:
                        animator_.Play("idle");
                        Rise();
                        break;

                    case AIR_STATE.MOVE_LEFT_AIR:
                        renderer_.flipX = false;
                        animator_.Play("idle");
                        Rise();
                        Move_Left_Air();
                        break;

                    case AIR_STATE.MOVE_RIGHT_AIR:
                        renderer_.flipX = true;
                        animator_.Play("idle");
                        Rise();
                        Move_Right_Air();
                        break;

                    case AIR_STATE.FALL:
                        animator_.Play("idle");
                        Fall();
                        break;

                    case AIR_STATE.FALL_LEFT:
                        renderer_.flipX = false;
                        animator_.Play("idle");
                        Move_Left_Air();
                        break;

                    case AIR_STATE.FALL_RIGHT:
                        renderer_.flipX = true;
                        animator_.Play("idle");
                        Move_Right_Air();
                        break;

                    case AIR_STATE.DOUBLE_JUMP:
                        Double_Jump_Enter();
                        break;
                }
                break;

            case AREA_STATE.GROUND:

                switch (current_Ground_State_)
                {
                    case GROUND_STATE.BUG:

                        break;

                    case GROUND_STATE.IDLE_GROUND:
                        animator_.Play("idle");
                        Idle_Ground();
                        break;

                    case GROUND_STATE.MOVE_LEFT_GROUND:
                        animator_.Play("Run");
                        renderer_.flipX = false;
                        Move_Left();
                        break;

                    case GROUND_STATE.MOVE_RIGHT_GROUND:
                        animator_.Play("Run");
                        renderer_.flipX = true;

                        Move_Right();
                        break;

                    case GROUND_STATE.JUMP_ENTER:
                        animator_.Play("idle");
                        Jump_Enter();
                        break;

                    case GROUND_STATE.JUMP_LEFT_ENTER:
                        animator_.Play("idle");
                        Move_Left();
                        Jump_Enter();
                        break;

                    case GROUND_STATE.JUMP_RIGHT_ENTER:
                        animator_.Play("idle");
                        Move_Right();
                        Jump_Enter();
                        break;

                    case GROUND_STATE.DROP_ENTER:
                        Drop_Enter();
                        break;

                    case GROUND_STATE.DROP_ENTER_LEFT:
                        Move_Left();
                        Drop_Enter();
                        break;

                    case GROUND_STATE.DROP_ENTER_RIGHT:
                        Move_Right();
                        Drop_Enter();
                        break;

                    default:

                        break;
                }
                break;
        }
    }

    void Idle_Ground()
    {
        
        current_Velocity_.x = 0.0f;
        rigid_Body_.velocity = current_Velocity_;
    }

    void Rise()
    {
        current_Velocity_.x = 0.0f;
        rigid_Body_.velocity = current_Velocity_;
    }

    void Fall()
    {
        current_Velocity_.x = 0.0f;
        rigid_Body_.velocity = current_Velocity_;
    }

    void Move_Left()
    {
        current_Velocity_.x = -1.0f * speed_;
        rigid_Body_.velocity = current_Velocity_;
    }

    void Move_Right()
    {
        current_Velocity_.x = 1.0f * speed_;
        rigid_Body_.velocity = current_Velocity_;
    }

    void Move_Left_Air()
    {
        current_Velocity_.x = -1.0f * speed_;
        rigid_Body_.velocity = current_Velocity_;
    }

    void Move_Right_Air()
    {
        current_Velocity_.x = 1.0f * speed_;
        rigid_Body_.velocity = current_Velocity_;
    }

    void Jump_Enter()
    {
        current_Area_State_ = AREA_STATE.AIR;
        rigid_Body_.AddForce(Vector2.up * 100.0f * jump_Height_);
    }

    void Double_Jump_Enter()
    {

        current_Velocity_.y = 0;
        rigid_Body_.velocity = current_Velocity_;


        rigid_Body_.AddForce(Vector2.up * (100.0f * jump_Height_ * 0.75f));
        double_Jump_ = true;
    }

    void Drop_Enter()
    {
        current_Area_State_ = AREA_STATE.AIR;
        drop_Cooldown_ = 0.75f;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag != "BLOCK")
            current_Area_State_ = AREA_STATE.GROUND;

        if (collision.collider.tag == "ZERO")
            current_Level_ = LEVEL.ZERO;
        else if(collision.collider.tag == "PASSABLE")
            current_Level_ = LEVEL.PASSABLE;
        else
            current_Level_ = LEVEL.ELSE;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        double_Jump_ = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        current_Area_State_ = AREA_STATE.AIR;
    }
}
