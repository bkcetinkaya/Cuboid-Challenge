using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieController : MonoBehaviour
{

    public event Action OnPlayerCollided;
    private Player _cubeController;
    private Rigidbody _playerRigidbody;
    private AudioManager audioManager;

    private void Start()
    {
        _cubeController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerRigidbody = _cubeController.GetComponent<Rigidbody>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            StartCoroutine(DieAndThenIvokeRestartScene());
        }
    }

    public IEnumerator DieAndThenIvokeRestartScene()
    {
        _playerRigidbody.isKinematic = false;
        audioManager.Play("FailSound");
        PlayerHealthController.Instance.SetPlayerHealth(-1);
        yield return new WaitForSeconds(1);
        
        OnPlayerCollided?.Invoke();
    }

}
