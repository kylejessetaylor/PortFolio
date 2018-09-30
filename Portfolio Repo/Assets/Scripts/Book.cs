using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Interactable {

    protected new void ChildUpdate()
    {

    }

    protected new void ActivateThisMenu()
    {
        blackMenu.SetActive(true);

        Transform menuTransform = menuIcons.transform;
        //Hides all icons
        for (int i = 0; i < menuTransform.childCount; i++)
        {
            menuTransform.GetChild(i).gameObject.SetActive(false);
        }

        thisMenu.SetActive(true);
    }

    protected new void HideThisMenu()
    {


    }
}
