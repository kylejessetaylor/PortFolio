using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTextBox : MonoBehaviour {

    [HideInInspector]
    public PlayerController player;
    private bool playerDisabled;

    public GameObject textBox;
    private Animator anim;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        anim = textBox.GetComponent<Animator>();

        for (int i = 0; i < textBox.transform.childCount; i++)
        {
            textBox.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (playerDisabled)
        {
            if (Input.anyKeyDown)
            {
                EnableMovement();
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("enabled");
            //Disables Movement
            player.moveState = PlayerController.MoveState.Disabled;
            playerDisabled = true;

            for (int i = 0; i < textBox.transform.childCount; i++)
            {
                textBox.transform.GetChild(i).gameObject.SetActive(true);
            }

            anim.SetBool("TextShow", true);

        }
    }


    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Debug.Log("disabled");
    //        //Enables Movement
    //        EnableMovement();
    //    }
    //}

    void EnableMovement()
    {
        player.moveState = PlayerController.MoveState.Idle;
        playerDisabled = false;

        anim.SetBool("TextShow", false);

        for (int i = 0; i < textBox.transform.childCount; i++)
        {
            textBox.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
