using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectible
{
    public void CoinCollect()
    {
        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
    }
}
