using System;
using _GameAssets.Scripts.Gameplay.Helpers;
using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private CatStateController _catStateController;

    private void Awake()
    {
        _catStateController = GetComponent<CatStateController>();
    }

    private void Update()
    {
        SetCatAnimations();
    }

    private void SetCatAnimations()
    {
        var catState = _catStateController.GetCurrentCatState();

        switch (catState)
        {
            case CatState.Idle:
                _animator.SetBool(Consts.CatAnimations.IS_IDLING, true);
                _animator.SetBool(Consts.CatAnimations.IS_WALKING, false);
                _animator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;
            case CatState.Walking:
                _animator.SetBool(Consts.CatAnimations.IS_IDLING, false);
                _animator.SetBool(Consts.CatAnimations.IS_WALKING, true);
                _animator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;
            case CatState.Running:
                _animator.SetBool(Consts.CatAnimations.IS_RUNNING, true);
                break;
            case CatState.Attacking:
                _animator.SetBool(Consts.CatAnimations.IS_ATTACKING, true);
                break;
        }
    }
}
