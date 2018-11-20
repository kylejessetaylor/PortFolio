using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Interactable : MonoBehaviour {

    //"E" Icon
    protected Animator anim;
    protected GameObject playerObject;
    protected PlayerController player;
    protected bool interactable;

    //'Canvas' object that also holds black screen
    protected bool menuVisible;
    protected GameObject menu;
    protected GameObject blackMenu;
    protected GameObject menuIcons;
    protected Animator menuAnim;
    protected Collider2D leftButton;
    protected Collider2D rightButton;

    //Individual Menus (Children scripts)
    public GameObject thisMenu;
    protected Animator thisMenuAnim;
    [Tooltip("How many different tabs there are available to cycle through.")]
    public int maxPages = 6;
    protected int pageNumber = 1;
    protected List<Collider2D> hyperlinkPages = new List<Collider2D>();
    [Tooltip("Count size should equal 'Max Pages'. Fix code if not true.")]
    public List<string> urls = new List<string>();


    protected void Awake()
    {
        //"E" Icon & player
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerController>();

        //'Canvas object
        menu = GameObject.FindGameObjectWithTag("Menus");
        blackMenu = menu.transform.GetChild(0).gameObject;
        menuIcons = menu.transform.GetChild(1).gameObject;
        menuAnim = menuIcons.GetComponent<Animator>();
        blackMenu.SetActive(false);

        //Individual Menus
        thisMenuAnim = thisMenu.GetComponent<Animator>();
        //Buttons
        leftButton = menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Collider2D>();
        rightButton = menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Collider2D>();
        //Hyperlinks
        for (int i = 0; i < thisMenu.transform.childCount; i++)
        {
            Collider2D link = thisMenu.transform.GetChild(i).gameObject.GetComponent<Collider2D>();
            hyperlinkPages.Add(link);
        }
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
        Interacting();

        //ChildUpdate();

        MenuInteractions();
    }

    private void Interacting()
    {
        if (interactable)
        {
            if (Input.GetKeyUp(KeyCode.E) && !menuVisible)
            {
                player.moveState = PlayerController.MoveState.Disabled;

                ActivateMenu();
            } 
            else if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.E))
            {
                player.moveState = PlayerController.MoveState.Idle;

                HideMenu();
            }
        }
    }

    protected void ChildUpdate()
    {

    }

    protected void ActivateMenu()
    {
        blackMenu.SetActive(true);

        menuVisible = true;
        menuAnim.SetBool("MenuVisible", true);
        //Hides all icons
        Transform menuTransform = menuIcons.transform;
        for (int i = 1; i < menuTransform.childCount; i++)
        {
            menuTransform.GetChild(i).gameObject.SetActive(false);
        }
        //Enables this one
        thisMenu.SetActive(true);

        //Starts off menu animator to correct slot
        thisMenuAnim.SetBool("PlayerDisabled", true);
        thisMenuAnim.SetInteger("PageNumber", 1);
        //Enables first hyperlink
        for (int i = 0; i < hyperlinkPages.Count; i++)
        {
            hyperlinkPages[i].gameObject.SetActive(false);
        }
        hyperlinkPages[pageNumber - 1].gameObject.SetActive(true);

        //Child Script Activate
        ActivateThisMenu();
    }

    protected void HideMenu()
    {
        blackMenu.SetActive(false);

        menuVisible = false;
        menuAnim.SetBool("MenuVisible", false);

        //Resets menu animator to starting slot
        thisMenuAnim.SetBool("PlayerDisabled", false);
        thisMenuAnim.SetInteger("PageNumber", 0);
        //Disables all hyperlinks
        for (int i = 0; i < hyperlinkPages.Count; i++)
        {
            hyperlinkPages[i].gameObject.SetActive(false);
        }

        //Child Script Activate
        HideThisMenu();
    }

    protected void ActivateThisMenu()
    {
    }

    protected void HideThisMenu()
    {
    }

//------------------------------------------------------------------------

    protected void MenuInteractions()
    {
        if (menuVisible)
        {
            //Turns on buttons
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);

            ClickControls();

            KeyBindControls();
        }
        else
        {
            //Turns off button images
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
        }
    }

    protected void ClickControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D target = ClickedOnScreen();
            if (target == null)
            {
                return;
            }

            //Right Button
            else if (target == rightButton)
            {
                PageNumber(true);
            }
            //Left Button
            else if (target == leftButton)
            {
                PageNumber(false);
            }

            //Hyperlinks
            else if (target.tag == "Hyperlink")
            {
                GoToHyperlink();
            }
        }
    }

    protected void KeyBindControls()
    {
        //Right Button
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            PageNumber(true);
        }
        //Left Button
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            PageNumber(false);
        }
    }

    protected Collider2D ClickedOnScreen()
    {

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (!hit)
        {
            return null;
        }
        return hit.collider;
    }

    protected void PageNumber(bool increase)
    {
        if (!thisMenuAnim.GetCurrentAnimatorStateInfo(0).IsName("BookFlip") && !thisMenuAnim.GetCurrentAnimatorStateInfo(0).IsName("BookFlip_Inverse"))
        {
            //Right
            if (increase)
            {
                pageNumber += 1;

                if (pageNumber > maxPages)
                {
                    pageNumber -= maxPages;
                }
                thisMenuAnim.speed = 1f;
                thisMenuAnim.SetInteger("PageNumber", pageNumber);
            }
            //Left
            else
            {
                pageNumber -= 1;

                if (pageNumber < 1)
                {
                    pageNumber += maxPages;
                }
                thisMenuAnim.speed = 1f;
                thisMenuAnim.SetInteger("PageNumber", pageNumber);
            }

            //Changes Active Hyperlink
        }
    }

    protected void GoToHyperlink()
    {
#if !UNITY_EDITOR
		openWindow(urls[pageNumber - 1]);
#endif
    }

    [DllImport("__Internal")]
    protected static extern void openWindow(string url);

}
