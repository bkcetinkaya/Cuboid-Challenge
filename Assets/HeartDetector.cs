using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDetector : MonoBehaviour
{

    HeartsUIManager heartsUIManager;

    private void Start()
    {
        heartsUIManager = GameObject.FindGameObjectWithTag("HeartsUIManager").GetComponent<HeartsUIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinCollider"))
        {
            PlayerHealthController.Instance.SetPlayerHealth(1);
            heartsUIManager.UpdateUI();
            Destroy(gameObject);
        }
    }
}
