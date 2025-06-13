using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealthImages;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _activeHealthSprite;
    [SerializeField] private Sprite _passiveHealthSprite;

    [Header("Settings")]
    [SerializeField] private float _animationDuration;
    
    private RectTransform[] _healthTransforms;

    private void Awake()
    {
        _healthTransforms = new RectTransform[_playerHealthImages.Length];
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            _healthTransforms[i] = _playerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        // For Testing Purposes
        if(Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamageForAll();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamage();
        }
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _activeHealthSprite)
            {
                AnimateHealthUI(_healthTransforms[i], _playerHealthImages[i]);
                break;
            }
        }    
    }

    public void AnimateDamageForAll()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
                AnimateHealthUI(_healthTransforms[i], _playerHealthImages[i]);
        }  
    }
    public void AnimateHealthUI(RectTransform activeTransform, Image activeImage)
    {
        activeTransform.DOScale(0f, _animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _passiveHealthSprite;
            activeTransform.DOScale(1f, _animationDuration).SetEase(Ease.OutBack);
        });
    }
}
