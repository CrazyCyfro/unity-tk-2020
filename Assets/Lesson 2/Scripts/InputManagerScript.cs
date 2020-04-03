using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerScript : MonoBehaviour
{
    public float sens;
    
    public float GetLookInputsHorizontal()
    {
        return Input.GetAxisRaw("Mouse X") * sens;
    }

    public float GetLookInputsVertical()
    {
        return Input.GetAxisRaw("Mouse Y") * sens;
    }

    public float GetMovementInputHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetMovementInputVertical()
    {
        return Input.GetAxis("Vertical");
    }


}
