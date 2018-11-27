using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnMove : MonoBehaviour {

    public PlayerController pc;
    public GameObject controls;
    	
	// Update is called once per frame
	void Update () {
		if (pc.moveState != PlayerController.MoveState.Idle)
        {
            controls.SetActive(false);
            GetComponent<HideOnMove>().enabled = false;
        }
	}
}
