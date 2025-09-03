using UnityEngine;

public class PlayerScoring : MonoBehaviour
{
    [SerializeField] private float _tempLoss = 3;
    [SerializeField] private float _tempMax = 90;
    [SerializeField] private float _tempLimit = 50;

    public float TempLoss { get => _tempLoss; }
    public float TempMax { get => _tempMax; }
    public float TempLimit { get => _tempLimit; }
}
