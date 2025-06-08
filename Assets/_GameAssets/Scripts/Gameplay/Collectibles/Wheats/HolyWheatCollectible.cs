using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float _forceIncrease;
    [SerializeField] private float _resetduration;

    public void Collect()
    {
        _playerController.SetJumpForce(_forceIncrease, _resetduration);
        Destroy(gameObject);
    }
}
