using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private BoolVar leftInput;

    [SerializeField]
    private BoolVar rightInput;

    [SerializeField]
    private BoolVar inputForward;

    [SerializeField]
    private BoolVar isInputBlocked;

    private bool pressingRight = false;
    private bool pressingLeft = false;

    private void Awake() 
    {
        isInputBlocked.OnChange += InputBlocked;    
        leftInput.Value = false;
        rightInput.Value = false;
        inputForward.Value = false;
    }

    private void OnDestroy()
    {
        isInputBlocked.OnChange -= InputBlocked;    
    }
    
    private void InputBlocked(bool oldVal, bool newVal)
    {
        if(newVal)
        {
            pressingLeft = false;
            pressingRight = false;
            StopRotateLeft();
            StopRotateRight();
            StopForward();
        }
    }

#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        if(isInputBlocked.Value)
        {
            return;
        }

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

            
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputForward.Value = true;     
        } 
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            inputForward.Value = false;     
        }
    }
#endif

    public void StartRotateRight()
    {
        if(isInputBlocked.Value)
        {
            return;
        }

        pressingRight = true;
        rightInput.Value = true;
        leftInput.Value = false;
    }

    public void StartRotateLeft()
    {
        if(isInputBlocked.Value)
        {
            return;
        }

        pressingLeft = true;
        leftInput.Value = true;
        rightInput.Value = false;
    }

    public void StopRotateRight()
    {
        pressingRight = false;
        rightInput.Value = false;
        leftInput.Value = pressingLeft;
    }

    public void StopRotateLeft()
    {
        pressingLeft = false;
        leftInput.Value = false;
        rightInput.Value = pressingRight;
    }

    public void StartForward()
    {
        if(isInputBlocked.Value)
        {
            return;
        }

        inputForward.Value = true;
    }

    public void StopForward()
    {
        inputForward.Value = false;
    }
}