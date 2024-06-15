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

    private void Awake()
    {
        crossHair = GetComponent<Image>();
        crossHair.color = crossHairColourInactive;
    }
    private void Start()
    {
        crossHairColourInactive.a = 1;
        crossHairColourActive.a = 1;
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
