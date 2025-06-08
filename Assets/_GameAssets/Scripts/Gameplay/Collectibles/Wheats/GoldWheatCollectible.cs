using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private PlayerController _playerController;
    
    [SerializeField] private float _increaseMovementSpeed;
    [SerializeField] private float _resetDuration;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_increaseMovementSpeed,_resetDuration);
        Destroy(gameObject);
    }
}
