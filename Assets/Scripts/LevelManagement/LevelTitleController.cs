using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTitleController : MonoBehaviour
{
    private TextMeshProUGUI titleText;

    private void Start()
    {
        titleText = GameObject.FindGameObjectWithTag("Level Title").GetComponent<TextMeshProUGUI>();
        titleText.text = SceneManager.GetActiveScene().name;
    }
}
