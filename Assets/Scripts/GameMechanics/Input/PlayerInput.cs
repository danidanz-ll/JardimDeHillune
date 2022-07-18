using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 GetMovementInput()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontalInput, verticalInput);
    }
    public bool IsDashingButtonDown()
    {
        return Input.GetButtonDown("Dash");
    }
    public bool IsAttackButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.KeypadEnter);
        bool isMobileButtonDown = false;
        return isKeyboardButtonDown || isMobileButtonDown;
    }
}
