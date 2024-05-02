using UnityEngine;

public class ResetElevatorButton : MonoBehaviour, IInterractable, IActive
{
    [SerializeField] Type _type;
    [SerializeField] Elevator _elevator;
    [SerializeField] ResetElevatorButton _oppositeResetButton;
    [SerializeField] MoveElevatorButton[] _moveButtons;

    bool _isActive;
    void Update()
    {
        if (_isActive)
        {
            Debug.Log("Reset is active");
            switch (_type)
            {
                case Type.Bottom:
                    _elevator.MoveBackward();
                    break;
                case Type.Top:
                    _elevator.MoveForward();
                    break;
            }
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Intertact(Player actor)
    {
        foreach (MoveElevatorButton button in _moveButtons)
            button.IsMovingElevator = false;
        _oppositeResetButton.Deactivate();
        Activate();
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    public enum Type
    {
        Bottom,
        Top,
    }
}
