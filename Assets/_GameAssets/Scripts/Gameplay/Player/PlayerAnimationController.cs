using System;
using _GameAssets.Scripts.Gameplay.Helpers;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private PlayerController _playerController;
    private StateController _stateController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();
    }

    private void Start()
    {
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }
    
    private void Update()
    {
        SetPlayerAnimation();
    }
    private void PlayerController_OnPlayerJumped()
    {
        _animator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke("ResetJumping",0.5f);
    }

    private void ResetJumping()
    {
        _animator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }
    public void SetPlayerAnimation()
    {
        PlayerState playerState = _stateController.GetCurrentPlayerState();

        switch (playerState)
        {
            case PlayerState.Idle:
                _animator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                _animator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                break;
            case PlayerState.Move:
                _animator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                _animator.SetBool(Consts.PlayerAnimations.IS_SLIDING,false);
                break;
            case PlayerState.SlideIdle:
                _animator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                _animator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                break;
            case PlayerState.Slide:
                _animator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                _animator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                break;
            
            
        }
    }
}
