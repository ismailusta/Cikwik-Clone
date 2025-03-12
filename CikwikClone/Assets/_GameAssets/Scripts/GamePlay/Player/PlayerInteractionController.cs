using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] Transform playerVisualTransform;
    private PlayerController playerController;
    private Rigidbody playerRigidbody;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.CoinCollect();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(playerController);
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(playerRigidbody, playerVisualTransform);
        }
    }
}
