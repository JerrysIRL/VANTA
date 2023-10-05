using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SpawnLevelButtons : MonoBehaviour
{
    [SerializeField] private LevelSelectButton _buttonPrefab;
    [SerializeField] private List<LevelSelectButton> _buttons = new List<LevelSelectButton>();

    [ContextMenu("SpawnButtons")]
    public void SpawnAllLevelButtons()
    {
        int count = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < _buttons.Count; i++)
        {
            DestroyImmediate(_buttons[i].gameObject);
        }

        _buttons.Clear();

        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            LevelSelectButton temp = Instantiate(_buttonPrefab,this.transform);
            _buttons.Add(temp);
            temp.name = "Level " + i + " button";
            temp.Setup(i);
        }
    }
}

