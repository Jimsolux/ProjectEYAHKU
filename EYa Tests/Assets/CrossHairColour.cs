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

    private void Awake()
    {
        instance = this;
        crossHair = GetComponent<Image>();
        crossHair.color = crossHairColourInactive;
    }
    private void Start()
    {
        
    }

    public void SetCrossHairColour(bool active)
    {
        if(active)
        {
            crossHair.color = crossHairColourActive;
            crossHairActive = true;
        }
        if(!active)
        {
            crossHair.color = crossHairColourInactive;
            crossHairActive = false;
        }
    }
}
