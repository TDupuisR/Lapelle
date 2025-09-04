using UnityEngine;

public enum AnimationState
{
    Idle = 0,
    Dig = 1,
    Hit = 2,
    Walk = 3
}

public class PlayerCore : MonoBehaviour, IInteract
{
    [SerializeField] private int _playerID;
    public int PlayerID { get => _playerID; }
    
    [Space(7)]
    [SerializeField] private PlayerController _playerController;
    public PlayerController Controller { get => _playerController; private set => _playerController = value; }

    [SerializeField] private PlayerInteractions _playerInteractions;
    public PlayerInteractions Interaction { get => _playerInteractions; private set => _playerInteractions = value; }

    [SerializeField] private AnimationSocket _animationSocket;
    public Transform Socket { get => _animationSocket.Socket; }
    [SerializeField] private PizzaSpawner _pizzaSpawner;
    public PizzaSpawner PizzaServing { get => _pizzaSpawner; }

    [Space(12)]
    [SerializeField] private Transform _spriteTransform;
    public Transform SpriteTransform { get => _spriteTransform; }
    [SerializeField] private Animator _animator;
    private AnimationState _animationState;
    public AnimationState SpriteState { get => _animationState; }

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
    }

    public void Init(int a_playerID, AnimationSocket a_socket)
    {
        _playerID = a_playerID;

        _animationSocket = a_socket;
        _spriteTransform = a_socket.transform;
        _animator = a_socket.PlayerAnimator;

        ChangeAnimationState(AnimationState.Idle);
        ChangeAnimationDir(Vector2.zero);
    }

    public void Interact(PlayerInteractions a_player)
    {
        a_player.Core.ChangeAnimationState(AnimationState.Hit);
        Controller.TakeDamage(a_player.transform.position);
    }

    public void ChangeAnimationState(AnimationState a_animation)
    {
        if (_animator == null)
            return;

        _animationState = a_animation;
        _animator.SetInteger("State", (int)_animationState);
    }
    public void ChangeAnimationDir(Vector2 a_dir)
    {
        if (_animator == null)
            return;

        int direction = 1;

        if (a_dir != Vector2.zero)
        {
            if (_animationState == AnimationState.Idle)
                direction = (int)PlayerController.CheckVertical(a_dir);
            else
                direction = (int)PlayerController.CheckDirection(a_dir);
        }
        else if (_animator.GetInteger("Direction") == 0)
            return;
        else
        {
            _spriteTransform.localRotation = Quaternion.Euler(0, 0, 0);
            return;
        }

        if (direction == 2 || direction == 0)
            _spriteTransform.localRotation = Quaternion.Euler(0, 180, 0);
        else
            _spriteTransform.localRotation = Quaternion.Euler(0, 0, 0);
        
        _animator.SetInteger("Direction", direction);
    }
}
