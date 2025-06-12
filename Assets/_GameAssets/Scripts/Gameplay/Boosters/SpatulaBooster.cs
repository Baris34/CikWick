using _GameAssets.Scripts.Gameplay.Helpers;
using UnityEngine;

public class SpatulaBooster : MonoBehaviour,IBoostable
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    
    [Header("Settings")]
    [SerializeField] private float _jumpForce;

    private bool _isActivated;
    public void Boost(PlayerController playerController)
    {
        if (_isActivated) return;
        PlayBoostAnimation();
        Rigidbody rb = playerController.GetPlayerRigidbody();
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        
        rb.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
        _isActivated = true;
        Invoke(nameof(ResetActivation), 0.5f);
    }
    private void PlayBoostAnimation()
    {
        if (_animator != null)
        {
            _animator.SetTrigger(Consts.OtherAnimations.IS_SPATULA);
        }
    }

    private void ResetActivation()
    {
        _isActivated = false;
    }
}
