using UnityEngine;

public class StopElevatorButton : MonoBehaviour, IInterractable, IActive
{
    [SerializeField] MoveElevatorButton[] _moveButtons;

    bool _isActive;
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
        foreach (MoveElevatorButton button in _moveButtons)
        {
            if (button.IsMovingElevator)
                button.IsMovingElevator = false;
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }
}
