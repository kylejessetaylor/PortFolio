using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConsole : Interactable {

    protected override void ChildStart()
    {

    }

    protected override void ChildUpdate()
    {

    }

    protected override void ActivateThisMenu()
    {
        if (gameManager.enabled)
        {
            gameManager.seenTV = true;
        }
        
    }

    protected override void HideThisMenu()
    {


    }

    protected override void ButtonsOn(bool on)
    {
        if (on)
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
        else
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
        }
    }
}
