using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public GameObject phone;
    private Animator anim;

    public bool seenBook;
    public bool seenTV;
    public bool seenPhone;

	// Use this for initialization
	void Start () {
        anim = phone.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckForPhoneAnim();
	}

    private void CheckForPhoneAnim()
    {
        if (seenPhone)
        {
            anim.SetBool("Ring", false);
            GetComponent<Manager>().enabled = false;
        }
        else if (seenBook && seenTV)
        {
            anim.SetBool("Ring", true);
        }
    }
}
