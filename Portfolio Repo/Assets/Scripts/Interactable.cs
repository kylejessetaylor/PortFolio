using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    protected Animator anim;
    protected GameObject playerObject;
    protected PlayerController player;

    protected bool interactable;

    protected bool menuVisible;
    protected GameObject menu;
    protected GameObject blackMenu;
    protected GameObject menuIcons;
    protected Animator menuAnim;
    public GameObject thisMenu;


    protected void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerController>();

        menu = GameObject.FindGameObjectWithTag("Menus");
        blackMenu = menu.transform.GetChild(0).gameObject;
        menuIcons = menu.transform.GetChild(1).gameObject;
        menuAnim = menuIcons.GetComponent<Animator>();

        blackMenu.SetActive(false);
    }


    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject == playerObject)
        {
            anim.SetBool("Interactable", true);
            interactable = true;
        }
    }

    void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.gameObject == playerObject)
        {
            anim.SetBool("Interactable", false);
            interactable = false;
        }
    }

    protected void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && interactable)
        {
            if (!menuVisible)
            {
                player.moveState = PlayerController.MoveState.Disabled;

                ActivateMenu();
            }
            else
            {
                player.moveState = PlayerController.MoveState.Idle;

                HideMenu();
            }
        }

        ChildUpdate();
    }

    protected void ChildUpdate()
    {

    }

    protected void ActivateMenu()
    {
        blackMenu.SetActive(true);

        menuVisible = true;
        menuAnim.SetBool("MenuVisible", true);

        //Child Script Activate
        ActivateThisMenu();
    }

    protected void HideMenu()
    {
        blackMenu.SetActive(false);

        menuVisible = false;
        menuAnim.SetBool("MenuVisible", false);

        //Child Script Activate
        HideThisMenu();
    }

    protected void ActivateThisMenu()
    {

    }

    protected void HideThisMenu()
    {

    }

}
