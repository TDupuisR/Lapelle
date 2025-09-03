using UnityEngine;

public class OvenValues : MonoBehaviour
{
    [SerializeField] private float _tempLoss = 12f;
    [SerializeField] private float _tempMax = 90f;
    [SerializeField] private float _tempLimit = 200f;
    public float TempLoss { get => _tempLoss; }
    public float TempMax { get => _tempMax; }
    public float TempLimit { get => _tempLimit; }

    [Space(7)]
    [SerializeField] private float _cookingTimeMedium = 5f;
    [SerializeField] private float _cookingTimeMin = 0.5f;
    [SerializeField] private float _cookingTimeMax = 10f;
    [SerializeField] private float _pizzaScore = 100f;

    public float CookingTimeMedium { get => _cookingTimeMedium; }
    public float CookingTimeMin { get => _cookingTimeMin; }
    public float CookingTimeMax { get => _cookingTimeMax; }

    public void AddScore(int a_playerNumber, float a_cookingAccuracy, float modifier = 1f)
    {

    }
}
