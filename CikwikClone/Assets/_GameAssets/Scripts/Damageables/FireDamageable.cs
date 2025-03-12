using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{
    private float force = 20f;
    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {
        HealtManager.instance.Damage(1);
        playerRigidbody.AddForce(-playerVisualTransform.forward * force, ForceMode.Impulse);
        Destroy(gameObject);
    }
}
