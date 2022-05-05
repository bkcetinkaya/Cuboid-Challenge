using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorizontalTouchButton : MonoBehaviour
{
    
    DieController dieController;


    private void Start()
    {
        dieController = GameObject.FindGameObjectWithTag("Border").GetComponent<DieController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinCollider"))
        {
            StartCoroutine(dieController.DieAndThenIvokeRestartScene());
        }
    }
}
