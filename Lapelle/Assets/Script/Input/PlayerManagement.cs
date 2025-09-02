using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagement : MonoBehaviour
{
    [SerializeField] private Color _player1;
    [SerializeField] private Color _player2;
    
    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += PlayerJoined;
        //PlayerInpurManager.instance.onPlayerLeft += PlayerLeft;
    }

    private void OnDisable()
    {
        PlayerInputManager.instance.onPlayerJoined -= PlayerJoined;
        //PlayerInpurManager.instance.onPlayerLeft -= PlayerLeft;
    }

    public void PlayerJoined(PlayerInput a_playerInput)
    {
        if (a_playerInput.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer playerSprite))
        {
            if (PlayerInputManager.instance.playerCount <= 1)
                playerSprite.color = _player1;
            else
                playerSprite.color = _player2;
        }
        else
        {
            Debug.LogError("Sprite Renderer not found");
        }
    }
}
