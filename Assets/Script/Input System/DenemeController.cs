using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DenemeController : MonoBehaviour
{
    private Vector2 holdVector;

    public void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
           
            Debug.Log("started "+ holdVector);
        }

        if (ctx.performed)
        {
            Debug.Log("performed "+ holdVector);
        }

        if (ctx.canceled)
        {
            Debug.Log("canceled " + holdVector);
        }

    }

    public void TouchPosition(InputAction.CallbackContext ctx)
    {
        holdVector = ctx.ReadValue<Vector2>();
    }

}
