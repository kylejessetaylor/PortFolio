using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : Interactable {

    protected GameObject oldExitButton;

    protected override void ChildStart()
    {
        oldExitButton = exitButton.gameObject;
        exitButton = hyperlinkPages[0].gameObject.gameObject.transform.Find("Button_Exit").GetComponent<Collider2D>();
    }

    protected override void ChildUpdate()
    {

    }

    protected override void ActivateThisMenu()
    {
        if (gameManager.enabled)
        {
            gameManager.seenPhone = true;
        }
    }

    protected override void HideThisMenu()
    {

    }

    protected override void ButtonsOn(bool on)
    {
        if (on)
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(true);
            oldExitButton.SetActive(false);
        }
        else
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
        }
    }
}
