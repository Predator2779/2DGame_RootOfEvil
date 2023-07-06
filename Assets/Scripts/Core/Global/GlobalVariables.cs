using UnityEngine;

namespace GlobalVariables
{
    public class GlobalConstants
    {
        #region String Constants

        public const string HorizontalAxis = "Horizontal";
        public const string VerticalAxis = "Vertical";
        public const string JumpButton = "Jump";

        public const string LeftMouseButton = "Fire1";
        public const string MiddleMouseButton = "Fire3";
        public const string RightMouseButton = "Fire2";

        public const string MouseX = "Mouse X";
        public const string MouseY = "Mouse Y";

        public const string MouseScrollWheel = "Mouse ScrollWheel";

        #endregion

        #region Int Constants

        public const int MinHitPoints = 0;

        #endregion

        #region Float Constants

        public const float CoefMovementSpeed = 15f;
        public const float CoefMouseSensitivityX = 2f;
        public const float CoefMouseSensitivityY = 0.3f;
        public const float MinRangeDamage = 0.65f;
        public const float MaxRangeDamage = 2.01f; 
        public const float TimeChangeColor = 0.1f;

        #endregion

        #region Position

        public static Vector2 ItemPositionLeft = new Vector2(-4f, -1.2f);
        public static Vector2 ItemPositionCenter = new Vector2(0, -1.2f);
        public static Vector2 ItemPositionRight = new Vector2(4f, -1.2f); 
        
        public static Vector2 PlayerTriggerPosFront = new Vector2(0, -3f);
        public static Vector2 PlayerTriggerPosBack = new Vector2(0, 3f);
        public static Vector2 PlayerTriggerPosLeft = new Vector2(-3f, 0f);
        public static Vector2 PlayerTriggerPosRight = new Vector2(3f, 0);

        #endregion

        #region Colors

        public static Color HealColor = new Color(0.459f, 1.000f, 1.000f, 1.000f);
        public static Color DamageColor = new Color(1.000f, 0.459f, 0.459f, 1.000f);

        #endregion

        #region Scenes

        public const string AfterDeathScene = "Tavern_Inside";

        #endregion
    }
}