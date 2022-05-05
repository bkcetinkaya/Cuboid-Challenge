using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementButtonController : MonoBehaviour
{
    private Player _player;

    private WinController _winController;

    [SerializeField]
    private Transform moveButtonsContent;

    public Button forwardButton;
    public Button backButton;
    public Button rightButton;
    public Button leftButton;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _winController = GameObject.FindGameObjectWithTag("WinController").GetComponent<WinController>();

        forwardButton.onClick.AddListener(_player.MoveForward);
        backButton.onClick.AddListener(_player.MoveBackward);
        rightButton.onClick.AddListener(_player.MoveRight);
        leftButton.onClick.AddListener(_player.MoveLeft);
        moveButtonsContent.gameObject.SetActive(true);
        _winController.OnPlayerCollided += DisableMovementButtons;
    }

    private void DisableMovementButtons()
    {
        moveButtonsContent.gameObject.SetActive(false);
    }
}
