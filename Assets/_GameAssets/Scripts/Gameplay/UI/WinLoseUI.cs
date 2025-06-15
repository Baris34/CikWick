using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameObject _winPopup;
    [SerializeField] private GameObject _losePopup;
    [SerializeField] private GameObject _blackBackground;
    
    [Header("Settings")]
    [SerializeField] private float _animationDuration = 0.3f;
    
    private Image _blackBackgroundImage;
    private RectTransform _winPopupTransform;
    private RectTransform _losePopupTransform;

    private void Awake()
    {
        _blackBackgroundImage = _blackBackground.GetComponent<Image>();
        _winPopupTransform = _winPopup.GetComponent<RectTransform>();
        _losePopupTransform = _losePopup.GetComponent<RectTransform>();
    }

    public void OnGameWin()
    {
        _winPopup.SetActive(true);
        _blackBackground.SetActive(true);
        
        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _winPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    public void OnGameLose()
    {
        _losePopup.SetActive(true);
        _blackBackground.SetActive(true);
        
        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _losePopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }
}
