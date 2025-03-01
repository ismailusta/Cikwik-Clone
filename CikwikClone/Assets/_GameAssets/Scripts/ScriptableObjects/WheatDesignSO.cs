using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float _IncreaseDecreaseAmount;
    [SerializeField] private float _effectDuration;

    public float IncreaseDecreaseAmount => _IncreaseDecreaseAmount;
    public float EffectDuration => _effectDuration;
}
