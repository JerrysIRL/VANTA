using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private Button _button;
    [SerializeField] private int _buildIndex;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SwitchLevel);
    }

    [ContextMenu("Setup")]
    public void Setup(int count)
    {
        levelTxt.text = count.ToString();
        _buildIndex = count;
        print("setup");
    }
    public void SwitchLevel()
    {
        print("KEVIN!!!");
        SceneManager.LoadScene(_buildIndex);
    }
}
