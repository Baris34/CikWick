using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    
    [SerializeField] private RectTransform _playerBoosterSpeedTransform;
    [SerializeField] private RectTransform _playerBoosterJumpTransform;    
    [SerializeField] private RectTransform _playerBoosterSlowTransform;
    
    [Header("Images")]
    [SerializeField] private Image _goldBoosterWheatImage;
    [SerializeField] private Image _holyBoosterWheatImage;
    [SerializeField] private Image _rottenBoosterWheatImage;
    
    private Image _playerWalkingImage;
    private Image _playerSlidingImage;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;

    [Header("Settings")]
    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    public RectTransform PlayerBoosterSpeedTransform => _playerBoosterSpeedTransform;
    public RectTransform PlayerBoosterJumpTransform => _playerBoosterJumpTransform;
    public RectTransform PlayerBoosterSlowTransform => _playerBoosterSlowTransform;
    public Image GoldBoosterWheatImage => _goldBoosterWheatImage;
    public Image HolyBoosterWheatImage => _holyBoosterWheatImage;
    public Image RottenBoosterWheatImage => _rottenBoosterWheatImage;
    
    private void Start()
    {
        _playerController.OnPlayerStateChanged +=PlayerController_OnplayerStateChanged;
        SetPlayerStateUI(_playerWalkingActiveSprite,_playerSlidingPassiveSprite,_playerWalkingTransform,_playerSlidingTransform);
    }

    private void Awake()
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }
    private void PlayerController_OnplayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move: 
                SetPlayerStateUI(_playerWalkingActiveSprite,_playerSlidingPassiveSprite,_playerWalkingTransform,_playerSlidingTransform);
                break;
            case PlayerState.SlideIdle:
            case PlayerState.Slide:
                SetPlayerStateUI(_playerWalkingPassiveSprite,_playerSlidingActiveSprite,_playerSlidingTransform,_playerWalkingTransform);
                break;
        }
    }
    
    public void SetPlayerStateUI(Sprite playerWalkingSprite,Sprite playerSlidingSprite,
        RectTransform activeTransform,RectTransform passiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    IEnumerator SetBoosterUI(RectTransform activeTransform, Image boosterImage, Image wheatImage,
        Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f,_moveDuration).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);
        
        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f,_moveDuration).SetEase(_moveEase);
    }

    public void PlaySetBoosterUI(RectTransform activeTransform, Image boosterImage, Image wheatImage,
        Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUI(activeTransform, boosterImage, wheatImage, activeSprite, passiveSprite, 
            activeWheatSprite, passiveWheatSprite, duration));
    }
}
