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

    //Sound
    [HideInInspector]
    public AudioSource playerSound;
    [Tooltip("Time it takes to complete a whole walk cycle.")]
    public float animStep;
    private float moveSoundCycle;
    [HideInInspector]
    public float timeSinceLastSound = 0.05f;
    public AudioClip walkSound;
    public AudioClip stuckSound;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

        //Sound
        playerSound = GetComponent<AudioSource>();
        NewWalkSound(false);

	}
	
	// Update is called once per frame
	void Update () {

        KeyControls();
        ApplyMovement();
    }

    private void KeyControls()
    {
        if (moveState == MoveState.Disabled)
        {
            //Animator
            anim.SetInteger("MovementState", 0);

            return;
        }
        else if (!Input.anyKey)
        {
            //Stays Still
            moveState = MoveState.Idle;
            //Animator
            anim.SetInteger("MovementState", 0);
        }
        //Up
        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Moves up
            moveState = MoveState.Up;
            //Animator
            anim.SetInteger("MovementState", 1);
        }
        //Down
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Moves down
            moveState = MoveState.Down;
            //Animator
            anim.SetInteger("MovementState", 2);
        }
        //Left
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Moves left
            moveState = MoveState.Left;
            //Animator
            anim.SetInteger("MovementState", 3);
        }
        //Right
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Moves right
            moveState = MoveState.Right;
            //Animator
            anim.SetInteger("MovementState", 4);
        }

        //If any key up & not interacting
        else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
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
            //Sound
            PlayMoveSound();
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
            //Sound
            PlayMoveSound();
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
            //Sound
            PlayMoveSound();
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
            //Sound
            PlayMoveSound();
        }
    }

    public void SlowAnimation()
    {
        anim.speed = slowAnim;

        //Sound
        NewWalkSound(true);
    }

    public void ResetAnimSpeed()
    {
        anim.speed = 1f;

        //Sound
        NewWalkSound(false);
    }

    private void PlayMoveSound()
    {
        timeSinceLastSound += Time.deltaTime;

        if (timeSinceLastSound >= moveSoundCycle && moveState != MoveState.Disabled)
        {
            timeSinceLastSound -= moveSoundCycle;

            playerSound.Play();
        }
    }

    private void NewWalkSound(bool slowRepetition)
    {
        //Normal Movement
        if (!slowRepetition)
        {
            AudioClip oldClip = playerSound.clip;
            playerSound.clip = walkSound;

            //Checks if sound changed from old
            if (oldClip != playerSound.clip && moveState != MoveState.Disabled)
            {
                playerSound.Play();

                timeSinceLastSound = 0;
            }

            moveSoundCycle = animStep / 2;
        }
        //StuckonWall
        else
        {
            AudioClip oldClip = playerSound.clip;
            playerSound.clip = stuckSound;

            //Checks if sound changed from old
            if (oldClip != playerSound.clip && moveState != MoveState.Disabled)
            {
                playerSound.Play();

                timeSinceLastSound = 0;

                //Restarts current animation
                AnimatorStateInfo currentAnim = anim.GetCurrentAnimatorStateInfo(0);
                int currentAnimInt = currentAnim.fullPathHash;
                anim.StopPlayback();
                anim.Play(currentAnimInt, -1, 0f);
            }

            moveSoundCycle = (animStep / 2) / slowAnim;
        }
    }
}
