using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;

    public event Action OnPlayerDied;
    public event Action OnPlayerHasNoHp;

    private AudioManager audioManager;

    private bool isDead = false;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border"))
        {
            //To prevent this block from running twice
            if (!isDead)
            {
                isDead = true;

                StartCoroutine(DieAndThenIvokeRestartScene());
            }
        }
    }

    public IEnumerator DieAndThenIvokeRestartScene()
    {
        _playerRigidbody.isKinematic = false;
        audioManager.Play("FailSound");
        PlayerHealthController.Instance.SetPlayerHealth(-1);
        
        yield return new WaitForSeconds(1);

        ChekIfPlayerHasZeroHp();
        OnPlayerDied?.Invoke();

    }

    private void ChekIfPlayerHasZeroHp()
    {
        int healthValue = PlayerHealthController.Instance.GetPlayerHealth();

        if(healthValue == 0)
        {
            OnPlayerHasNoHp?.Invoke();
        }
    }
}
