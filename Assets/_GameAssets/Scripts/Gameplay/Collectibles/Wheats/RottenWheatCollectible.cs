using System;
using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour,ICollectible
{
    [SerializeField] WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerStateUI _playerStateUI;
    
    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.PlayerBoosterSlowTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        _playerStateUI.PlaySetBoosterUI(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.RottenBoosterWheatImage,
            _wheatDesignSO.ActiveSprite, _wheatDesignSO.PassiveSprite,
            _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.PassiveWheatSprite, _wheatDesignSO.ResetDuration);
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetDuration);
        Destroy(gameObject);

    }
}
