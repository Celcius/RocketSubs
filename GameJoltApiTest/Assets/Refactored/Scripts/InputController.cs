using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private BoolVar leftInput;

    [SerializeField]
    private BoolVar rightInput;

    private bool pressingRight = false;
    private bool pressingLeft = false;

#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftInput.Value = true;     
        } 
        else if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftInput.Value = false;     
        }
            
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightInput.Value = true;     
        } 
        else if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightInput.Value = false;     
        }
    }
#endif

    public void StartRotateRight()
    {
        pressingRight = true;
        rightInput.Value = true;
        leftInput.Value = false;
        Debug.Log("StartRotateRight");
    }

    public void StartRotateLeft()
    {
        pressingLeft = true;
        leftInput.Value = true;
        rightInput.Value = false;
        Debug.Log("StartRotateLeft");
    }

    public void StopRotateRight()
    {
        pressingRight = false;
        rightInput.Value = false;
        leftInput.Value = pressingLeft;
        Debug.Log("StopRotateRight");
    }

    public void StopRotateLeft()
    {
        pressingLeft = false;
        leftInput.Value = false;
        rightInput.Value = pressingRight;
        Debug.Log("StopRotateLeft");
    }
}