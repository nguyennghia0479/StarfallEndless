using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100;

    private void Update()
    {
        transform.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
    }
}
