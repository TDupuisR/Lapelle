using System;
using Unity.VisualScripting;
using UnityEngine;

public class OvenValues : MonoBehaviour
{
    [SerializeField] private float _tempLoss = 12f;
    [SerializeField] private float _tempMax = 90f;
    [SerializeField] private float _tempLimit = 200f;

    public float TempLoss
    {
        get => _tempLoss;
    }
    public float TempMax
    {
        get => _tempMax;
    }
    public float TempLimit
    {
        get => _tempLimit;
    }

    
    [Space(7)] [SerializeField] private float _cookingTimeMedium = 5f;
    [SerializeField] private float _cookingTimeMin = 0.5f;
    [SerializeField] private float _cookingTimeMax = 10f;
    [SerializeField] private float _pizzaScore = 100f;

    public float CookingTimeMedium
    {
        get => _cookingTimeMedium;
    }
    public float CookingTimeMin
    {
        get => _cookingTimeMin;
    }
    public float CookingTimeMax
    {
        get => _cookingTimeMax;
    }

    [Space(12)]
    private int _player1Score = 0;
    public int Player1Score { get => _player1Score; }
    private int _player2Score = 0;
    public int Player2Score { get => _player2Score; }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _player1Score = 0;
        _player2Score = 0;
    }

    public void AddScore(int a_playerNumber, float a_cookingAccuracy, float modifier = 1f)
    {
        switch (a_playerNumber)
        {
            case 1:
                _player1Score += Mathf.RoundToInt(a_cookingAccuracy * _pizzaScore * modifier);
                break;
            case 2:
                _player2Score += Mathf.RoundToInt(a_cookingAccuracy * _pizzaScore * modifier);
                break;
            
            default:
                break;
        }
    }
}
