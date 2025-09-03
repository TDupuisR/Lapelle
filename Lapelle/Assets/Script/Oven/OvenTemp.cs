using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

public class OvenTemp : MonoBehaviour, IInteract
{
    [SerializeField] private int _playerAssigned;
    public int PlayerAssigned { get => _playerAssigned; }

    [Space(7)]
    [SerializeField] private OvenValues _ovenValues;

    [SerializeField] private Transform _tempCursor;
    [SerializeField] private float _maxAngle = 70;

    private float _currentTemp;
    public float currentTemp
    {
        get => _currentTemp;
        private set
        {
            _currentTemp = Mathf.Clamp(value, -_ovenValues.TempLimit, _ovenValues.TempLimit);
            float sign = -Mathf.Sign(_currentTemp);

            float currentAngle = sign * ((Mathf.Abs(_currentTemp) * _maxAngle) / _ovenValues.TempLimit);
            _tempCursor.localRotation = Quaternion.Euler(0, 0, currentAngle);
        }
    }

    private SOPizza _pizzaInfos = null;
    [SerializeField] bool _isPizzaIn = false;
    [SerializeField] private float _pizzaStatus = 0.0f;

    [SerializeField] private Transform _cookingGaugeTransform;
    [SerializeField] private SpriteRenderer _cookingGaugeSprite;


    [SerializeField] public bool FireIsRunning = true;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        currentTemp = 0;
        _isPizzaIn = false;
        FireIsRunning = false;
    }

    private void FixedUpdate()
    {
        if (FireIsRunning)
        {
            currentTemp -= _ovenValues.TempLoss * Time.fixedDeltaTime;
        }

        if (_isPizzaIn)
        {
            float timeToCook;

            if (currentTemp >= 0)
            {
                float multi = Mathf.Clamp01(currentTemp / _ovenValues.TempMax);
                timeToCook = _ovenValues.CookingTimeMedium - (multi * (_ovenValues.CookingTimeMedium - _ovenValues.CookingTimeMin));
            }
            else
            {
                float multi = Mathf.Clamp01(Mathf.Abs(currentTemp) / _ovenValues.TempMax);
                timeToCook = _ovenValues.CookingTimeMedium + (multi * (_ovenValues.CookingTimeMax - _ovenValues.CookingTimeMedium));
            }
            
            _pizzaStatus = Mathf.Clamp(_pizzaStatus + ((100f / timeToCook) * Time.fixedDeltaTime), 0f, 200f);

            CookingGaugeUpdate();
        }
    }

    public void Interact(PlayerInteractions a_player)
    {
        if (PlayerAssigned != a_player.Core.PlayerID)
        {
            if (a_player.Item != null &&
                a_player.Item.type == SOItems.TYPE.Fuel)
            {
                AddInFire((SOFuel)a_player.TakeItem());
            }

            return;
        }
        
        if (a_player.Item != null)
        {
            switch (a_player.Item.type)
            {
                case SOItems.TYPE.Pizza:
                    if (_isPizzaIn)
                        RemovePizza();
                    else
                        AddPizza((SOPizza)a_player.TakeItem());
                    break;
                case SOItems.TYPE.Fuel:
                    AddInFire((SOFuel)a_player.TakeItem());
                    break;
            }
        }
        else
        {
            if (_isPizzaIn)
                RemovePizza();
        }
    }
    
    private void AddInFire(SOFuel a_fuelItem)
    {
        currentTemp += a_fuelItem.itemValue;

        switch (a_fuelItem.effect)
        {
            default:
                break;
        }
    }
    
    private bool AddPizza(SOPizza a_pizza)
    {
        if (a_pizza != null && !_isPizzaIn)
        {
            _pizzaInfos = a_pizza;
            _isPizzaIn = true;
            _pizzaStatus = 0.0f;

            return true;
        }
        else
            return false;
    }
    [Button]
    private void RemovePizza()
    {
        if (!_isPizzaIn)
            return;
        
        if (_pizzaStatus <= 100f)
        {
            Debug.Log($"Between Undercook and Perfect cook : {_pizzaStatus}");
        }
        else if (_pizzaStatus > 100f)
        {
            _pizzaStatus -= 100f;
            _pizzaStatus = Mathf.Clamp(100f - _pizzaStatus, 0, 100);

            Debug.Log($"Between Perfect cook and Overcook : {_pizzaStatus}");
        }

        _ovenValues.AddScore(PlayerAssigned, _pizzaStatus / 100f, _pizzaInfos ? _pizzaInfos.itemValue : 1.0f);

        _pizzaInfos = null;
        _isPizzaIn = false;

        _cookingGaugeTransform.localPosition = new Vector3(-0.5f, 0, 0);
        _cookingGaugeTransform.localScale = new Vector3(0f, 1, 1);
    }

    private void CookingGaugeUpdate()
    {
        if (_pizzaStatus <= 100f)
        {
            _cookingGaugeTransform.localPosition = new Vector3(Mathf.Lerp(-0.5f, 0f, _pizzaStatus / 100f), 0, -1);
            _cookingGaugeTransform.localScale = new Vector3(Mathf.Lerp(0f, 1f, _pizzaStatus / 100f), 1, 1);
            _cookingGaugeSprite.color = Color.Lerp(Color.blue, Color.green, _pizzaStatus / 100f);
        }
        else
        {
            _cookingGaugeTransform.localPosition = -Vector3.forward;
            _cookingGaugeTransform.localScale = Vector3.one;
            _cookingGaugeSprite.color = Color.Lerp(Color.green, Color.red, (_pizzaStatus - 100f) / 100f);
        }
    }

#if UNITY_EDITOR
    [Button]
    public void AddTest()
    {
        currentTemp += 10;
    }
    [Button]
    private bool AddPizza()
    {
        if (!_isPizzaIn)
        {
            _isPizzaIn = true;
            _pizzaStatus = 0.0f;

            return true;
        }
        else
            return false;
    }
#endif
}
