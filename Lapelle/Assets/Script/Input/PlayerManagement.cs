using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagement : MonoBehaviour
{
    [Space(7)]
    [SerializeField] private Color _playerColor1;
    [SerializeField] private Color _playerColor2;

    [Space(7)]
    [SerializeField] private Transform _player1Spawn;
    [SerializeField] private Transform _player2Spawn;

    private PlayerCore _player1;
    private PlayerCore _player2;
    
    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += PlayerJoined;
        PlayerInputManager.instance.onPlayerLeft += PlayerLeft;
    }

    // private void OnDisable()
    // {
    //     PlayerInputManager.instance.onPlayerJoined -= PlayerJoined;
    //     PlayerInputManager.instance.onPlayerLeft -= PlayerLeft;
    // }

    public void PlayerJoined(PlayerInput a_playerInput)
    {
        if (PlayerInputManager.instance.playerCount > 2)
            return;

        if (a_playerInput.gameObject.TryGetComponent<PlayerCore>(out PlayerCore o_player))
        {
            Debug.Log($"Player Count : { PlayerInputManager.instance.playerCount}");

            if (o_player == null)
            {
                Debug.LogError($"PlayerCore is null", a_playerInput.gameObject);
            }
            else if (_player1 == null)
            {
                _player1 = o_player;
                _player1.SpriteComponent.color = _playerColor1;
                _player1.transform.position = _player1Spawn.position;

                Debug.Log($"Player 1", _player1.gameObject);
            }
            else if (_player2 == null)
            {
                _player2 = o_player;
                _player2.SpriteComponent.color = _playerColor2;
                _player2.transform.position = _player2Spawn.position;

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
}
