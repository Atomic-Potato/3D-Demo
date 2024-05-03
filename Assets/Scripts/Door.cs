using UnityEngine;

public class Door : MonoBehaviour, IInterractable
{
    [SerializeField, Min(0)] float _speed = 3f;
    
    bool _isClose;
    int _closingDirection;
    bool _isOpen;
    int _openningDirection;

    float _rotationVelocity;
    void Update()
    {
        if (_isClose)
        {
            if (!Mathf.Approximately(transform.rotation.z, 0))
            {
                transform.rotation = Quaternion.Euler(
                    transform.rotation.x,
                    Mathf.SmoothDamp(
                        transform.rotation.y, 
                        0f, ref _rotationVelocity, Time.deltaTime * _speed * _closingDirection),
                    transform.rotation.z);
            }
            else
            {
                _isClose = false;
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            }
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Intertact(Player actor)
    {
        if (transform.rotation.y != 0)
        {
            _isClose = true;
            _closingDirection = transform.rotation.y > 0 ? -1 : 1;
        }
        else
            _isOpen = true;
    }
}
