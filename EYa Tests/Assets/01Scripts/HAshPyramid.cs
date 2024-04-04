using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class HAshPyramid : MonoBehaviour
{

    private string outputList;
    public int listSize = 2;
    int row, height;

    void Start()
    {
        outputList = "";
    }


    // I have -   # -
    // I want each layer to add a #, a space and an enter
    // Then   -   # # - if its 1
    // I want it to do the layers seperately, counting up. for each in 5, display the 1, the two, the three and the four.. instead of doing the same thing always.

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            addToList(listSize);
            Debug.Log(outputList);
        }
    }

    private void addToList(int size)    // Adds 1 layer -- NEEDS TO DO Add 1, add 2, add 3... now it does NR amount the NR
    {
        for (int i = 1;i <= size;i++) {  // FOR EACH NR TOWARDS MAX SIZE, 

            for (int j  = 0 ; j < i; j++) {  // WHILE J IS SMALLER OR THE SAME AS I, ADD # ! -- But also spaces, how?
           
                
            //for(int spaces = 0; spaces < height - row; spaces++)
            //{
            //    outputList += "~";
            //    Debug.Log(spaces);
            //}
            outputList += " # ";
                
            }
        outputList += "\n";


        }
    }

}

