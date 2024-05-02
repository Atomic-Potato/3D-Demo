using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] float _sensitivity = 2.0f;

    [SerializeField] float _minYAngle = -90.0f;
    [SerializeField] float _maxYAngle = 90.0f;
    float _rotationX = 0.0f;
    float _rotationY = 0.0f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _rotationX = transform.rotation.eulerAngles.x;
        _rotationY = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        _rotationY += Input.GetAxis("Mouse X") * _sensitivity;
        _rotationX -= Input.GetAxis("Mouse Y") * _sensitivity;
        _rotationX = Mathf.Clamp(_rotationX, _minYAngle, _maxYAngle);
        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
    }
}
