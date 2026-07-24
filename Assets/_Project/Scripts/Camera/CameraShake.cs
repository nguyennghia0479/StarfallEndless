using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Range(0f, .1f)]
    [SerializeField] private float shakeMagnitude =.05f;
    [Range(0f, .2f)]
    [SerializeField] private float shakeDuration = .1f;

    private Vector3 defaultPosition;
    private Coroutine cameraShakeRoutine;

    private void Awake()
    {
        defaultPosition = transform.position;
    }

    private void OnEnable()
    {
        GameEvents.OnEntityDamaged += PlayCameraShake;
    }

    private void OnDisable()
    {
        GameEvents.OnEntityDamaged -= PlayCameraShake;
    }

    public void PlayCameraShake(GameObject gameObject)
    {
        if (!gameObject.CompareTag(GameIdentifiers.GameTags.TAG_PLAYER))
            return;

        if (cameraShakeRoutine != null)
        {
            StopCoroutine(cameraShakeRoutine);
            cameraShakeRoutine = null;
        }

        cameraShakeRoutine = StartCoroutine(CameraShakeRoutine());
    }

    private IEnumerator CameraShakeRoutine()
    {
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            transform.position = defaultPosition + (Vector3)shakeOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = defaultPosition;
    }
}
