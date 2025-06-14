using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCollectUI : MonoBehaviour
{
    public static EggCollectUI Instance { get; private set; }
    
    [Header("References")]
    [SerializeField] private TMP_Text _eggCountText;
    [SerializeField] private Color _eggTextColor;
    
    [Header("Settings")]
    [SerializeField] private float _eggScaleDuration;
    [SerializeField] private float _eggTextColorDuration;
    
    private RectTransform _eggCountTransform;
    public RectTransform EggCountTransform => _eggCountTransform;
    private void Awake()
    {
        Instance = this;
        _eggCountTransform = _eggCountText.gameObject.GetComponent<RectTransform>();
    }

    public void UpdateEggCountText(int eggCount,int maxEggCount)
    {
        _eggCountTransform.DOScale(1.15f, _eggScaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            _eggCountText.text = eggCount.ToString() + "/" + maxEggCount.ToString();
            
            if (eggCount == maxEggCount) return;
            
            _eggCountTransform.DOScale(Vector3.one, _eggScaleDuration).SetEase(Ease.OutBack);
        });
    }

    public void UpdateEggCompleted()
    {
        _eggCountText.DOColor(_eggTextColor, _eggTextColorDuration);
        _eggCountTransform.DOScale(1.25f, _eggScaleDuration).SetEase(Ease.InBack);
    }
}
