using UnityEngine;

public class ElevatorDoor : MonoBehaviour, IInterractable
{
    [SerializeField] Transform _sittingTransform;
    [SerializeField] Transform _exitTransform;
    [SerializeField] MoveElevatorButton[] _moveButtons;
    [SerializeField] ResetElevatorButton[] _resetButtons;
    bool _isSittingComfortably;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Intertact(Player actor)
    {
        if (!_isSittingComfortably)
        {
            _isSittingComfortably = true;
            actor.IsMovementActive = false;
            actor.transform.position = _sittingTransform.position;
            actor.transform.parent = _sittingTransform.parent;
            actor.Rigidbody.isKinematic = true;
            foreach (MoveElevatorButton button in _moveButtons)
                button.Activate();
            foreach (ResetElevatorButton button in _resetButtons)
                    button.Deactivate();
        }
        else
        {
            _isSittingComfortably = false;
            actor.IsMovementActive = true;
            actor.transform.position = _exitTransform.position;
            actor.transform.parent = null;
            actor.Rigidbody.isKinematic = false;
            foreach (MoveElevatorButton button in _moveButtons)
                button.Deactivate();
        }
    }
}
