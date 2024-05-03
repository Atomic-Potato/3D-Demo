using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] string _sceneToLoad;

    void OnTriggerEnter(Collider col)
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
