using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairColour : MonoBehaviour
{
    public Color crossHairColourInactive;
    public Color crossHairColourActive;
    public bool crossHairActive;
    public Image crossHair;
    public static CrossHairColour instance;

    private void Start()
    {
        instance = this;
        crossHair = GetComponent<Image>();
        crossHair.color = crossHairColourInactive;
    }

    public void SetCrossHairColour(bool active)
    {
        if(active)
        {
            crossHair.color = crossHairColourActive;
            crossHairActive = true;
        }
        else
        {
            crossHair.color = crossHairColourInactive;
            crossHairActive = false;
        }
    }
}
