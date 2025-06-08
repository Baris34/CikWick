using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    
    [SerializeField] private float _decreaseMovementSpeed;
    [SerializeField] private float _resetDuration;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_decreaseMovementSpeed, _resetDuration);
        Destroy(gameObject);

    }
}
