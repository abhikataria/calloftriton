using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [Tooltip("in s")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab of Player")][SerializeField] GameObject deathFx;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFx.SetActive(true);
        Invoke("ReloadScene",levelLoadDelay);

    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
