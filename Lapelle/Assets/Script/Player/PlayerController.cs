using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private Vector2 _moveInput; 
    public float speed = 5f;

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        Vector3 move = new Vector3(_moveInput.x, _moveInput.y, 0f);
        transform.Translate(move * speed * Time.deltaTime);
    }
}
