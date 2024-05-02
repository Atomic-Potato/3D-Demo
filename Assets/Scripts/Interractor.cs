using UnityEngine;

public class Interractor : MonoBehaviour
{
    [SerializeField, Min(0)] float _detectionDistance = 3f;
    [SerializeField] LayerMask _interactableLayer;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * _detectionDistance);
    } 

    void Update()
    {
        IInterractable interactable = null;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, _detectionDistance, _interactableLayer);
        if (hits.Length > 0)
        {
            interactable = hits[0].collider.gameObject.GetComponent<IInterractable>();
        }

        if (interactable != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                interactable.Intertact(Player.Instance);
                Debug.Log("Interacted " + interactable.GetGameObject().name);
            }
        }
    }
}
