using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButtonController : MonoBehaviour
{
    [SerializeField]
    private Transform bridge;

    [SerializeField]
    private Transform dieColliderLocatedOnBridge;

    

    private bool isPlayerDied;
    bool isBridgeActive;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<Rigidbody>().isKinematic)
            {
                isPlayerDied = true;
            }
        }

    
            if (other.CompareTag("WinCollider"))
            {
                if (isPlayerDied == false)
                {
                AudioManager.Instance.Play("ButtonPressSound");
                ToggleBridge();
                }


            }
        

    }

    private void ToggleBridge()
    {
         isBridgeActive= bridge.gameObject.activeInHierarchy;       
         bridge.gameObject.SetActive(!isBridgeActive);
         isBridgeActive = !isBridgeActive;
      
         dieColliderLocatedOnBridge.gameObject.SetActive(!isBridgeActive);
               
    }
}
