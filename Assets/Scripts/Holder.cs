using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField, Min(0)] float _maximumThrowForce = 10;
    [SerializeField, Min(0)] float _throwForceBuildUpMultiplier = 1;
    [SerializeField] Transform _holdingParent;
    [Space]
    [SerializeField] Rigidbody _bodyToKnockBack;
    [SerializeField, Min(0.1f)] float _knockBackFactorFromThrowForce = .5f; 

    Throwable _throwable;

    float _forceBuiltUp;
    void Update()
    {
        if (_throwable == null)
            return;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_forceBuiltUp < _maximumThrowForce)
            {
                _forceBuiltUp += Time.deltaTime * _throwForceBuildUpMultiplier;
                if (_forceBuiltUp > _maximumThrowForce)
                    _forceBuiltUp = _maximumThrowForce;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Throw(_forceBuiltUp);
            KnockBody(_forceBuiltUp * _knockBackFactorFromThrowForce);
            _forceBuiltUp = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
            Drop();
    }

    public void Hold(Throwable throwable)
    {
        if (_throwable != null)
            Drop();

        _throwable = throwable;
        _throwable.transform.parent = _holdingParent;
        _throwable.transform.position = _holdingParent.transform.position;
        _throwable.Rigidbody.isKinematic = true;
    }

    void Throw(float force)
    {
        _throwable.transform.parent = null;
        _throwable.Rigidbody.isKinematic = false;
        _throwable.Rigidbody.AddForce(_holdingParent.forward * force, ForceMode.Impulse);
        _throwable = null;
    }

    void Drop()
    {
        _throwable.transform.parent = null;
        _throwable.Rigidbody.isKinematic = false;
        _throwable = null;
    }

    void KnockBody(float force)
    {
        if (_bodyToKnockBack != null)
        {
            _bodyToKnockBack.AddForce(-_holdingParent.forward * force, ForceMode.Impulse);
        }
    }
}
