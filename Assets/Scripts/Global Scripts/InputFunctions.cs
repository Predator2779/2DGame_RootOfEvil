using UnityEngine;
using GlobalVariables;

namespace InputData
{
    public class InputFunctions
    {
        static public float GetVerticalAxis()
        {
            return Input.GetAxis(GlobalConstants.VerticalAxis);
        }

        static public float GetHorizontallAxis()
        {
            return Input.GetAxis(GlobalConstants.HorizontalAxis);
        }

        static public Vector2 GetMousePosition()
        {
            return Input.mousePosition;
        }

        static public float GetMousePositionX()
        {
            return Input.GetAxis(GlobalConstants.MouseX) * GlobalConstants.CoefMouseSensitivityX;
        }

        static public float GetMousePositionY()
        {
            return Input.GetAxis(GlobalConstants.MouseY) * GlobalConstants.CoefMouseSensitivityY;
        }
        
        static public bool GetLMB()
        {
            return Input.GetMouseButton(0);
        } 
        
        static public bool GetRMB_Down()
        {
            return Input.GetMouseButtonDown(1);
        }  
        
        static public bool GetMMB_Down()
        {
            return Input.GetMouseButtonDown(2);
        }

        static public bool GetKeyE()
        {
            return Input.GetKey(KeyCode.E);
        }
    }
}