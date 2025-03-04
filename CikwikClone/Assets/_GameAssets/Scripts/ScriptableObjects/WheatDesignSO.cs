using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float _IncreaseDecreaseAmount;
    [SerializeField] private float _effectDuration;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _inactiveSprite;
    [SerializeField] private Sprite _activeWheatSprite;
    [SerializeField] private Sprite _inactiveWheatSprite;


    public float IncreaseDecreaseAmount => _IncreaseDecreaseAmount;
    public float EffectDuration => _effectDuration;
    public Sprite ActiveSprite => _activeSprite;
    public Sprite InactiveSprite => _inactiveSprite;
    public Sprite ActiveWheatSprite => _activeWheatSprite;
    public Sprite InactiveWheatSprite => _inactiveWheatSprite;

}
