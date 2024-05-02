using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinge : MonoBehaviour
{
    [SerializeField, Range(0f, 180f)] float _angleLimit = 90f;

    void Update()
    {
        Debug.Log(transform.rotation.y);
        if (transform.rotation.y > 0f && transform.rotation.y > _angleLimit/180f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, _angleLimit, transform.rotation.z);
            Debug.Log("limited angle");
        }
        else if (transform.rotation.y <= 0f && transform.rotation.y < -_angleLimit / 180f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, _angleLimit, transform.rotation.z);
        }
    }
}
