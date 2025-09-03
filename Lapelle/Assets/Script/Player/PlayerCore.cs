using UnityEngine;

public class PlayerCore : MonoBehaviour, IInteract
{
    [SerializeField] private int _playerID;
    public int PlayerID { get => _playerID; }
    
    [Space(7)]
    [SerializeField] private PlayerController _playerController;
    public PlayerController Controller { get => _playerController; private set => _playerController = value; }

    [SerializeField] private PlayerInteractions _playerInteractions;
    public PlayerInteractions Interaction { get => _playerInteractions; private set => _playerInteractions = value; }

    [SerializeField] private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteComponent { get => _spriteRenderer; set => _spriteRenderer = value; }

    //public string PlayerName { get => gameObject.name; set => gameObject.name = value; }

    private void Start()
    {
        if (Controller == null)
        {
            if (TryGetComponent<PlayerController>(out PlayerController o_playerControl))
            {
                Controller = o_playerControl;
            }
            else
            {
                Debug.LogError($"Player does not reference any PlayerController", gameObject);
            }
        }

        if (Interaction == null)
        {
            if (TryGetComponent<PlayerInteractions>(out PlayerInteractions o_interaction))
            {
                Interaction = o_interaction;
            }
            else
            {
                Debug.LogError($"Player does not reference any InteractionComponent", gameObject);
            }
        }

        if (SpriteComponent == null)
        {
            if (TryGetComponent<SpriteRenderer>(out SpriteRenderer o_spriteRenderer))
            {
                SpriteComponent = o_spriteRenderer;
            }
            else
            {
                Debug.LogError($"Player does not reference any SpriteRenderer", gameObject);
            }
        }
    }

    public void Init(int a_playerID/*, SOPlayerInfos a_playerInfos*/)
    {
        _playerID = a_playerID;
    }

    public void Interact(PlayerInteractions a_player)
    {
        Controller.TakeDamage(a_player.transform.position);
    }
}
