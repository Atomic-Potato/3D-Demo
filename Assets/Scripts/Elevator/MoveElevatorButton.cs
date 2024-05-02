using PathCreation.Examples;
using UnityEngine;

public class MoveElevatorButton : MonoBehaviour, IInterractable, IActive
{
    [SerializeField] Elevator _elevatorPathFollower;
    [SerializeField] Type _type;
    [SerializeField] MoveElevatorButton _oppositeTypeButton;

    [HideInInspector] public bool IsMovingElevator;
    bool _isActive;
    void Update()
    {
        if (!IsMovingElevator || !_isActive)
            return;

        switch (_type)
        {
            case Type.Up:
                _elevatorPathFollower.MoveForward();
                break;
            case Type.Down:
                _elevatorPathFollower.MoveBackward();
                break;
        }
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Intertact(Player actor)
    {
        Debug.Log(_isActive);
        if (!_isActive)
            return;
        IsMovingElevator = true;
        _oppositeTypeButton.IsMovingElevator = false;
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public enum Type
    {
        Up,
        Down,
    }
}
