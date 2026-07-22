using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputSet inputs;

    private void Awake()
    {
        inputs = new PlayerInputSet();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    public  Vector3 GetMoveDirection()
    {
        Vector2 moveDir = inputs.Player.Move.ReadValue<Vector2>();

        return moveDir.normalized;
    }
}
