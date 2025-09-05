using UnityEngine;

public class ReadyZone : MonoBehaviour
{
    [SerializeField] private PlayerManagement _playerManager;

    [SerializeField] private int _playerID = 0;
    [SerializeField] private SpriteRenderer _sprite;

    [SerializeField] private Color _colorTarget;

    [SerializeField] private float _timeToReady = 2f;
    private float _timeCurrent = 2f;

    private bool _isPlayerIn;

    public bool IsReady { get => _timeCurrent <= 0; }

    private void Start()
    {
        _timeCurrent = _timeToReady;
    }

    private void FixedUpdate()
    {
        if (_isPlayerIn && _timeCurrent > 0)
        {
            _timeCurrent -= Time.fixedDeltaTime;
            _sprite.color = Color.Lerp(_colorTarget, Color.white, _timeCurrent / _timeToReady);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerCore>(out PlayerCore o_player))
        {
            if (o_player.PlayerID == _playerID)
            {
                _isPlayerIn = true;
                _timeCurrent = _timeToReady;
                _sprite.color = Color.Lerp(_colorTarget, Color.white, _timeCurrent / _timeToReady);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerCore>(out PlayerCore o_player))
        {
            if (o_player.PlayerID == _playerID)
            {
                _isPlayerIn = false;
                _timeCurrent = _timeToReady;
                _sprite.color = Color.Lerp(_colorTarget, Color.white, _timeCurrent / _timeToReady);
            }
        }
    }
}
