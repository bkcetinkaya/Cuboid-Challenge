using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorizontalTouchButton : MonoBehaviour
{

    PlayerDieController playerDieController;


    private void Start()
    {
        playerDieController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDieController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinCollider"))
        {
            StartCoroutine(playerDieController.DieAndThenIvokeRestartScene());
        }
    }
}
