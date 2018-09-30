using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour {

    public bool colliding = false;
    private PlayerController player;
    private int myNumber;

    void Start()
    {
        Transform playerObject = transform.parent.transform;

        player = playerObject.GetComponent<PlayerController>();
        for (int i = 0; i < playerObject.childCount; i++)
        {
            if(playerObject.GetChild(i).name == gameObject.name)
            {
                myNumber = i;
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Interactable")
        {
            colliding = true;
            player.movementCheckers[myNumber] = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Interactable")
        {
            colliding = false;
            player.movementCheckers[myNumber] = false;

            player.ResetAnimSpeed();
        }
    }
}
