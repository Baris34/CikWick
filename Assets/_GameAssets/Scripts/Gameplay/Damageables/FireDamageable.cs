using UnityEngine;

public class FireDamageable : MonoBehaviour,IDamageable
{
    [Header("Settings")]
    [SerializeField] private float _forceMagnitude = 5f;
    public void GiveDamage(Rigidbody playerRigidbody, Transform playerTransform)
    {
        HealthManager.Instance.Damage(1);
        playerRigidbody.AddForce(-playerTransform.forward * _forceMagnitude,ForceMode.Impulse);
        Destroy(gameObject);
    }
}
