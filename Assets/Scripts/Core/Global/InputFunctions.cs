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

        static public float GetHorizontalAxis()
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

        static public bool GetLMB_Down()
        {
            return Input.GetMouseButtonDown(0);
        }

        static public bool GetLMB_Up()
        {
            return Input.GetMouseButtonUp(0);
        }  
        
        static public bool GetRMB_Up()
        {
            return Input.GetMouseButtonUp(1);
        }

        static public bool GetRMB_Down()
        {
            return Input.GetMouseButtonDown(1);
        }

        static public bool GetMMB_Down()
        {
            return Input.GetMouseButtonDown(2);
        }

        static public bool GetEscapeButton_Up()
        {
            return Input.GetKeyUp(KeyCode.Escape);
        }

        static public bool GetKeyE_Up()
        {
            return Input.GetKeyUp(KeyCode.E);
        }  
        
        static public bool GetKeyF_Up()
        {
            return Input.GetKeyUp(KeyCode.F);
        }
    }
}