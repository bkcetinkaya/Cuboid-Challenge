
using UnityEngine;

[ExecuteInEditMode]
public class SimpleCameraController : MonoBehaviour
 {
    [SerializeField]
    private Transform _player;


    [SerializeField]
    private Vector3 offset;

    private void Update()
    {
        transform.position = _player.position + offset;
        transform.LookAt(_player);
    }
}

