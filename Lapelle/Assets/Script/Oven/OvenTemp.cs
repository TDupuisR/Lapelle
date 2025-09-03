using UnityEngine;
using NaughtyAttributes;

public class OvenTemp : MonoBehaviour
{
    [SerializeField] private PlayerScoring _playerScoring;

    [SerializeField] private Transform _tempCursor;
    [SerializeField] private float _maxAngle = 70;

    private float _currentTemp;
    private float currentTemp
    {
        get => _currentTemp;
        set
        {
            _currentTemp = Mathf.Clamp(value, -_playerScoring.TempLimit, _playerScoring.TempLimit);
            float sign = -Mathf.Sign(_currentTemp);

            float currentAngle = sign * ((Mathf.Abs(_currentTemp) * _maxAngle) / _playerScoring.TempLimit);
            _tempCursor.localRotation = Quaternion.Euler(0, 0, currentAngle);
        }
    }

    public bool timeRunning = true;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        currentTemp = 0;
    }

    private void FixedUpdate()
    {
        if (timeRunning)
        {
            currentTemp -= _playerScoring.TempLoss * Time.fixedDeltaTime;
            Debug.Log($"Temp : {currentTemp}");
        }
    }

    public void AddObject(Item a_givenItem)
    {
        currentTemp += a_givenItem.value;

        switch (a_givenItem.effect)
        {
            default:
                break;
        }
    }
#if UNITY_EDITOR
    [Button]
    public void AddTest()
    {
        currentTemp += 10;
    }

#endif
}
