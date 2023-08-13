using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region Settings
    [Header("Settings")]
    [SerializeField] float deadZone = 0.2f;
    #endregion

    #region Internal Variables
    private bool isJumpButtonPressed = false;
    private Vector3 direction = Vector3.zero;
    private Vector3 rawInput = Vector3.zero;
    private bool IsDashButtonPressed = false;
    private bool inputEnabled = true;
    private bool attackButtonPressed = false;
    private bool isRunButtonPressed = false;
    private bool isEquipButtonPressed = false;
    private bool isShieldButtonPressed = false;
    private bool isRollButtonPressed;
    #endregion

    #region Properties
    public bool IsJumpButtonPressed{get => isJumpButtonPressed;set => isJumpButtonPressed = value;}
    public Vector3 Direction{get => direction;set => direction = value;}
    public bool InputEnabled{get => inputEnabled;set => inputEnabled = value;}
    public Vector3 RawInput{get => rawInput;set => rawInput = value;}
    public bool AttackButtonPressed{get => attackButtonPressed;set => attackButtonPressed = value;}
    public bool IsRunButtonPressed{get => isRunButtonPressed;set => isRunButtonPressed = value;}
    public bool IsEquipButtonPressed{get => isEquipButtonPressed;set => isEquipButtonPressed = value;}
    public bool IsShieldButtonPressed{get => isShieldButtonPressed;set => isShieldButtonPressed = value;}
    public bool IsRollButtonPressed{get => isRollButtonPressed;set => isRollButtonPressed = value;}
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            isJumpButtonPressed = Input.GetKey(KeyCode.Space);
            IsDashButtonPressed = Input.GetKey(KeyCode.F);
            AttackButtonPressed = Input.GetMouseButtonDown(0);
            isRunButtonPressed = Input.GetKey(KeyCode.LeftShift);
            isEquipButtonPressed = Input.GetKeyDown(KeyCode.E);
            isShieldButtonPressed = Input.GetMouseButton(1);
            isRollButtonPressed = Input.GetKey(KeyCode.R);
            RawInput = InputWithRadialDeadZone(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            //RawInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            direction = InputWithRadialDeadZone(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            isJumpButtonPressed = false;
            IsDashButtonPressed = false;
            AttackButtonPressed = false;
            isRunButtonPressed = false;
            RawInput = Vector3.zero;
            direction = Vector3.zero;
        }
    }

    public Vector3 InputWithRadialDeadZone(float horizontalInput, float verticalInput)
    {
        Vector3 joystickInput = new Vector3(horizontalInput, 0, verticalInput);

        if (joystickInput.magnitude < deadZone) joystickInput = Vector3.zero;

        return Vector3.ClampMagnitude(joystickInput, 1);
    }

    public Vector3 InputWithScaledRadialDeadZone(float horizontalInput, float verticalInput)
    {
        Vector3 joystickInput = new Vector3(horizontalInput, 0, verticalInput);
        if (joystickInput.magnitude < deadZone)
            joystickInput = Vector3.zero;
        else
            joystickInput = joystickInput.normalized * ((joystickInput.magnitude - deadZone) / (1 - deadZone));

        return Vector3.ClampMagnitude(joystickInput, 1);
    }
}