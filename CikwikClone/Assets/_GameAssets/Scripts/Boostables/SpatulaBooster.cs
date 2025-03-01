using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
{
    [Header("References")]
    [SerializeField] private Animator _BoostAnimation;

    [Header("Boost Settings")]
    [SerializeField] private float _forceAmount;

    private bool isActive;

    public void Boost(PlayerController player)
    {
        if (isActive) return;
        BoostAnimation();
        Rigidbody rigidbody = player.GetPlayerRigidbody();
        rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0f, rigidbody.linearVelocity.z);
        rigidbody.AddForce(transform.forward * _forceAmount, ForceMode.Impulse);
        isActive = true;
        Invoke(nameof(ResetActivate), 0.2f);
    }
    private void BoostAnimation()
    {
        _BoostAnimation.SetTrigger(Consts.SpatulaAnimations.IS_SPATULA_JUMPING);
    }
    private void ResetActivate()
    {
        isActive = false;
    }
}
