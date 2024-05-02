using UnityEngine;
using PathCreation;

public class Elevator : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;

    void Start() {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            rotation.x = transform.rotation.x;
            rotation.z = transform.rotation.z;
            transform.rotation = rotation;
        }
    }

    Vector3 _previousPoint;
    bool _isreachedEnd = false;
    public void MoveForward()
    {
        if (pathCreator != null && !_isreachedEnd)
        {
            distanceTravelled += speed * Time.deltaTime;
            Vector3 newPoint = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            // Debug.Log("Moving forward " + _previousPoint + " " + "");
            if (_previousPoint == newPoint)
            {
                _isreachedEnd = true;
                return;
            }
            transform.position = newPoint;
            Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            rotation.x = transform.rotation.x;
            rotation.z = transform.rotation.z;
            transform.rotation = rotation;
            _previousPoint = newPoint;
        }
    }

    public void MoveBackward()
    {
        if (pathCreator != null)
        {
            if (distanceTravelled < 0)
            {
                distanceTravelled = 0f;
                return;
            }
            Debug.Log("Moving backward");
            distanceTravelled -= speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            rotation.x = transform.rotation.x;
            rotation.z = transform.rotation.z;
            transform.rotation = rotation;
            _isreachedEnd = false;
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}