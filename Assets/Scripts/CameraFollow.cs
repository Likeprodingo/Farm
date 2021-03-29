using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private float _smoothSpeed;

    private Transform _targetTransform;

    private void OnEnable()
    {
        //_targetTransform = FindObjectOfType<Character>().transform;
    }

    private void LateUpdate()
    {
        if (_targetTransform != null)
        {
            Vector3 newDirection = new Vector3(_targetTransform.position.x - _cameraOffset.x, _cameraOffset.y, _targetTransform.position.z - _cameraOffset.z);
            transform.position = Vector3.Lerp(transform.position, newDirection, _smoothSpeed);
        }
    }
}
