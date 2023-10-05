using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoplayer;
    [SerializeField] private int _nextScene;

    [SerializeField] private bool _anykeydown;
    [SerializeField] private Image _image;
    [SerializeField] private float _fillamount;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private bool _add3SecondsAtTheStart;

    private void Start()
    {
        StartCoroutine(PlayScene());
    }

    private void FixedUpdate()
    {
        if (Input.anyKey)
        {
            _anykeydown = true;
        }
        else
        {
            _anykeydown = false;
        }

        if (_anykeydown)
        {
            _image.fillAmount += _fillamount;
            if(_image.fillAmount > 0)
            {
                _gameObject.SetActive(true);
            }
            if (_image.fillAmount == 1)
            {
                SceneManager.LoadScene(_nextScene);
            }
        }
        else if(!_anykeydown && _image.fillAmount > 0)
        {
            _image.fillAmount -= _fillamount;
            if(_image.fillAmount == 0)
            {
                _gameObject.SetActive(false);
            }
        }
       
    }

    private IEnumerator PlayScene()
    {
        if (_add3SecondsAtTheStart)
        {
            yield return new WaitForSeconds(3);
        }

        _videoplayer.Play();
 
        yield return new WaitForSeconds(Mathf.RoundToInt((float)_videoplayer.length));
            
       SceneManager.LoadScene(_nextScene);
    }

    private void ToggleSkip(bool showImage)
    {
        if (showImage == false && _image.enabled == true) 
        {
            _image.enabled = true;
        }
        if(showImage == false && _image == true)
        {
            _image.enabled = false;
        }
    }

}