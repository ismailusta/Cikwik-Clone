using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.WheatTags.GOLD_WHEAT))
        {
            other.gameObject.GetComponent<GoldWheatCollectible>().CoinCollect();
        }
        if (other.CompareTag(Consts.WheatTags.ROTTEN_WHEAT))
        {
            other.gameObject.GetComponent<RottenWheatCollectible>().CoinCollect();
        }
        if (other.CompareTag(Consts.WheatTags.HOLY_WHEAT))
        {
            other.gameObject.GetComponent<HolyWheatCollectible>().CoinCollect();
        }
    }
}
