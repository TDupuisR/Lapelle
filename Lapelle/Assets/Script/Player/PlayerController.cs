using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Direction
{
    Top = 0,
    Bottom = 1,
    Left = 2,
    Right = 3
}
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    public PlayerCore Core { get => _playerCore; }
    
    private Vector2 _moveInput; 
    [SerializeField] private float speed = 5f;
    [SerializeField] private float knockback = 200f;
    
    [SerializeField] private bool isStunt = false;
    [SerializeField] private float _stunDuration = 1f;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float _invincibilityDuration = 1f;
    
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _itemContainer;

    private Vector2 _playerDir;

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
    public Direction CheckDirection(Vector2 a_dir)
    {
        if (Mathf.Abs(a_dir.x) > Mathf.Abs(a_dir.y))
        {
            if (a_dir.x >= 0)
                return Direction.Right;
            else
                return Direction.Left;
        }
        else
        {
            if (a_dir.y <= 0)
                return Direction.Top;
            else
                return Direction.Bottom;
        }
    }
    public Direction CheckVertical(Vector2 a_dir)
    {
        if (a_dir.y <= 0)
            return Direction.Top;
        else
            return Direction.Bottom;
    }
    
    public void TakeDamage(Vector3 a_position)
    {
        if (isInvincible == false)
        {
            Debug.Log("Take Damage");

            Vector3 move = (transform.position - a_position).normalized;
            _rb.AddForce(move * knockback);
            
            isStunt = true;
            Core.Interaction.canInteract = false;
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
