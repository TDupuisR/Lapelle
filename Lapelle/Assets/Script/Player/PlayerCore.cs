using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;
    public PlayerController Controller { get => _playerController; private set => _playerController = value; }

    [SerializeField]
    private InteractionCharbon _interactionCharbon;
    public InteractionCharbon Interaction { get => _interactionCharbon; private set => _interactionCharbon = value; }

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
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
            if (TryGetComponent<InteractionCharbon>(out InteractionCharbon o_interaction))
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
}
