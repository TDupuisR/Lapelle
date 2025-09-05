using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagement : MonoBehaviour
{
    [Space(7)]
    [SerializeField] private GameObject _playerPrefab1;
    [SerializeField] private GameObject _playerPrefab2;

    [Space(7)]
    [SerializeField] private Transform _player1Spawn;
    [SerializeField] private Transform _player2Spawn;

    [Space(7)]
    [SerializeField] private ReadyZone _zonePlayer1;
    [SerializeField] private ReadyZone _zonePlayer2;

    [Space(7)]
    [SerializeField] private OvenManager _ovenManager;
    
    [Space(7)]
    [SerializeField] private List<ItemsSpawner> _itemsSpawners = new List<ItemsSpawner>();
    [SerializeField] private List<PizzaSpawner> _pizzaSpawners = new List<PizzaSpawner>();
    [SerializeField] private SandItemsSpawner _sandItemsSpawner;
    
    [Space(10)]
    [SerializeField] private GameLoopManager _victoryManager;
    
    [Space(10)]
    [SerializeField] private AudioSource _audioSource;

    private PlayerCore _player1;
    private PlayerCore _player2;

    private bool _hasGameStarted 
    {
        get => _victoryManager.GameIsRunning;
    }

    private float StartTimer;
    [SerializeField] private TextMeshProUGUI _timerText;
    
    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += PlayerJoined;
        PlayerInputManager.instance.onPlayerLeft += PlayerLeft;
        
        _ovenManager.ActiveOvens(false);
        StartTimer = 3f;
    }

    // private void OnDisable()
    // {
    //     PlayerInputManager.instance.onPlayerJoined -= PlayerJoined;
    //     PlayerInputManager.instance.onPlayerLeft -= PlayerLeft;
    // }

    private void Update()
    {
        if (!_hasGameStarted && _zonePlayer1.IsReady && _zonePlayer2.IsReady)
        {
            if (StartTimer > 0f)
            {
                if (!_audioSource.isPlaying)
                    _audioSource.Play();
                StartTimer -= Time.deltaTime;
                _timerText.text = $"{Mathf.CeilToInt(StartTimer)}...";
            }
            else
            {
                _audioSource.Stop();
                _timerText.text = "";
                CallStart();
            }
        }
        else if (!_hasGameStarted)
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
            StartTimer = 3f;
            _timerText.text = "";
        }
    }

    public void PlayerJoined(PlayerInput a_playerInput)
    {
        if (PlayerInputManager.instance.playerCount > 2)
            return;

        if (a_playerInput.gameObject.TryGetComponent<PlayerCore>(out PlayerCore o_player))
        {
            if (o_player == null)
            {
                Debug.LogError($"PlayerCore is null", a_playerInput.gameObject);
            }
            else if (_player1 == null)
            {
                _player1 = o_player;
                
                GameObject animator = Instantiate(_playerPrefab1, _player1.transform);
                AnimationSocket socket = animator.GetComponent<AnimationSocket>();
                _player1.transform.position = _player1Spawn.position;

                _player1.Init(1, socket,_victoryManager);
                Debug.Log($"Player 1", _player1.gameObject);
            }
            else if (_player2 == null)
            {
                _player2 = o_player;
                GameObject animator = Instantiate(_playerPrefab2, _player2.transform);
                AnimationSocket socket = animator.GetComponent<AnimationSocket>();
                _player2.transform.position = _player2Spawn.position;

                _player2.Init(2, socket, _victoryManager);
                Debug.Log($"Player 2", _player2.gameObject);
            }
        }
        else
        {
            Debug.LogError($"PlayerCore not found", a_playerInput.gameObject != null ? a_playerInput.gameObject : null);
        }
    }

    public void PlayerLeft(PlayerInput a_playerInput)
    {
        if (a_playerInput.gameObject.GetInstanceID() == _player1.gameObject.GetInstanceID())
            _player1 = null;
        else if (a_playerInput.gameObject.GetInstanceID() == _player2.gameObject.GetInstanceID())
            _player2 = null;
    }

    private void CallStart()
    {
        _victoryManager.StartTimer();
        
        _ovenManager.ActiveOvens(true);

        foreach (ItemsSpawner itemSpawner in _itemsSpawners)
        {
            itemSpawner.SpawnItem();
        }

        foreach (PizzaSpawner pizzaSpawner in _pizzaSpawners)
        {
            pizzaSpawner.SpawnPizza();
        }

        _sandItemsSpawner.interactable = true;

        _zonePlayer1.gameObject.SetActive(false);
        _zonePlayer2.gameObject.SetActive(false);
    }

    public void CallEnd()
    {
        _ovenManager.ActiveOvens(false);
        
        _player1.Interaction.canInteract = false;
        _player2.Interaction.canInteract = false;
    }
}
