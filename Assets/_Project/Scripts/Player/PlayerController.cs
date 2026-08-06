using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputSet inputs;
    private Vector2 previousTouchPos;
    private Vector2 currentMoveDelta;
    private bool isDragging = false;

    private void Awake()
    {
        inputs = new PlayerInputSet();
    }

    private void OnEnable()
    {
        EnableInputs();
    }

    private void OnDisable()
    {
        DisableInputs();
    }

    private void Update()
    {
        Vector2 currentPos = inputs.Player.Pointer.ReadValue<Vector2>();
        if (inputs.Player.Press.WasPressedThisFrame())
        {
            isDragging = true;
            previousTouchPos = currentPos;
            currentMoveDelta = Vector2.zero;
        }
        else if (inputs.Player.Press.IsPressed() && isDragging)
        {
            currentMoveDelta = currentPos - previousTouchPos;
            previousTouchPos = currentPos;

        }
        else if (inputs.Player.Press.WasReleasedThisFrame())
        {
            isDragging = false;
            currentMoveDelta = Vector2.zero;
        }
    }

    public Vector3 GetMoveDirection()
    {
        Vector2 moveDir = inputs.Player.Move.ReadValue<Vector2>();
        if (moveDir == Vector2.zero)
            moveDir = isDragging ? currentMoveDelta : Vector2.zero;

        return moveDir.normalized;
    }

    public void EnableInputs() => inputs.Enable();
    public void DisableInputs() => inputs.Disable();
}
