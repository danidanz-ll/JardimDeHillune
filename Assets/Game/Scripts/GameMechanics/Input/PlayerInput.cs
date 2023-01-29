using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 GetMovementInput()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontalInput, verticalInput).normalized;
    }
    public bool IsDashingButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetButtonDown("Dash");
        bool isMobileButtonDown = false;
        bool isJoystickdButtonDown = Input.GetButtonDown("Dash Joystick");
        return isKeyboardButtonDown || isMobileButtonDown || isJoystickdButtonDown;
    }
    public bool IsSelectorTowerButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetButtonDown("SelectorTower");
        bool isMobileButtonDown = false;
        //bool isJoystickdButtonDown = Input.GetButtonDown("Attack Joystick");
        //return isKeyboardButtonDown || isMobileButtonDown || isJoystickdButtonDown;
        return Input.GetButtonDown("SelectorTower");
    }
    public bool IsAttackButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetButtonDown("Attack");
        bool isMobileButtonDown = false;
        bool isJoystickdButtonDown = Input.GetButtonDown("Attack Joystick");
        return isKeyboardButtonDown || isMobileButtonDown || isJoystickdButtonDown;
    }
    public bool IsInvokeButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetButtonDown("Invoke");
        bool isMobileButtonDown = false;
        bool isJoystickdButtonDown = Input.GetButtonDown("Invoke Joystick");
        return isKeyboardButtonDown || isMobileButtonDown || isJoystickdButtonDown;
    }
}
