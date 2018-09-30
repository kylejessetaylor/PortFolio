using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public enum MoveState {Idle, Up, Down, Left, Right, Disabled}
    public MoveState moveState = MoveState.Idle;

    //public List<Collider2D> movementCheckers = new List<Collider2D>();
    public List<bool> movementCheckers = new List<bool>();

    private Animator anim;
    public float slowAnim = 0.25f;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        ApplyMovement();
    }

    private void OnGUI()
    {
        if (moveState == MoveState.Disabled)
        {
            //Animator
            anim.SetInteger("MovementState", 0);

            return;
        }
        if (!Input.anyKey)
        {
            //Stays Still
            moveState = MoveState.Idle;
            //Animator
            anim.SetInteger("MovementState", 0);
        }
        //Up
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Moves up
            moveState = MoveState.Up;
            //Animator
            anim.SetInteger("MovementState", 1);
        }
        //Down
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Moves down
            moveState = MoveState.Down;
            //Animator
            anim.SetInteger("MovementState", 2);
        }
        //Left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Moves left
            moveState = MoveState.Left;
            //Animator
            anim.SetInteger("MovementState", 3);
        }
        //Right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Moves right
            moveState = MoveState.Right;
            //Animator
            anim.SetInteger("MovementState", 4);
        }

        //If any key up & not interacting
        if (!Input.anyKeyDown)
        {
            //Up
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                //Moves up
                moveState = MoveState.Up;
                //Animator
                anim.SetInteger("MovementState", 1);
            }
            //Down
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                //Moves down
                moveState = MoveState.Down;
                //Animator
                anim.SetInteger("MovementState", 2);
            }
            //Left
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //Moves left
                moveState = MoveState.Left;
                //Animator
                anim.SetInteger("MovementState", 3);
            }
            //Right
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //Moves right
                moveState = MoveState.Right;
                //Animator
                anim.SetInteger("MovementState", 4);
            }
        }
    }

    private void ApplyMovement()
    {
        if (moveState == MoveState.Idle)
        {
            return;
        }
        //Up
        else if (moveState == MoveState.Up)
        {
            //Check if can move
            if (movementCheckers[0] == false)
            {
                ResetAnimSpeed();
                //Moves up
                transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            }
            else
            {
                SlowAnimation();
            }
        }
        //Down
        else if (moveState == MoveState.Down)
        {
            //Check if can move
            if (movementCheckers[1] == false)
            {
                ResetAnimSpeed();

                //Moves down
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
            }
            else
            {
                SlowAnimation();
            }
        }
        //Left
        else if (moveState == MoveState.Left)
        {
            //Check if can move
            if (movementCheckers[2] == false)
            {
                ResetAnimSpeed();      
                //Moves left
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                SlowAnimation();
            }
        }
        //Right
        else if (moveState == MoveState.Right)
        {
            //Check if can move
            if (movementCheckers[3] == false)
            {
                ResetAnimSpeed();
                //Moves right
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                SlowAnimation();
            }
        }
    }

    public void SlowAnimation()
    {
        anim.speed = slowAnim;
    }

    public void ResetAnimSpeed()
    {
        anim.speed = 1f;
    }
}
