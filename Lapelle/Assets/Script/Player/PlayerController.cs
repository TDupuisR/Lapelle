using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    public PlayerCore Core { get => _playerCore; }
    
    private Vector2 _moveInput; 
    [SerializeField] private float speed = 5f;
    [SerializeField] private float knockback = 2f;
    
    [SerializeField] private bool isStunt = false;
    [SerializeField] private float _stunDuration = 3f;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float _invincibilityDuration = 1f;
    
    [SerializeField] private Rigidbody2D _rb;

    public void Start()
    {
        if (_rb == null)
                TryGetComponent<Rigidbody2D>(out _rb);

        if (_playerCore == null)
                TryGetComponent<PlayerCore>(out _playerCore);
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if(isStunt == false)
        {
            Vector3 move = new Vector3(_moveInput.x, _moveInput.y, 0f);
            _rb.MovePosition(transform.position + (move * speed * Time.fixedDeltaTime));
        }
    }
    
    public void TakeDamage(Vector3 a_position)
    {
        if (isInvincible == false)
        {
            Vector3 move = (transform.position + a_position).normalized;
            _rb.MovePosition(transform.position + (move * knockback));
            
            isStunt = true;
            Core.Interaction.DropItem();
            isInvincible = true;
            StartCoroutine(StopAttackIsStunt());
        }
    }

    private IEnumerator StopAttackIsStunt()
    {
        yield return new WaitForSeconds(_stunDuration);
        isStunt = false;
        Core.Interaction.canInteract = true;
        yield return new WaitForSeconds(_invincibilityDuration);
        isInvincible = false;
    }


}
