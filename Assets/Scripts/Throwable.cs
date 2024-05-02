using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Throwable : MonoBehaviour, IInterractable
{
    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }

    public void Intertact(Player actor)
    {
        actor.Holder.Hold(this);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
