using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour,ICollectible
{
    [SerializeField] WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetDuration);
        Destroy(gameObject);

    }
}
