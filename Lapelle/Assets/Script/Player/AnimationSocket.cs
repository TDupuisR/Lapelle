using UnityEngine;

public class AnimationSocket : MonoBehaviour
{
    [SerializeField] private Transform _socket;
    public Transform Socket { get => _socket; }
    [SerializeField] private Animator _animator;
    public Animator PlayerAnimator { get => _animator; }
    [SerializeField] private GameObject _stunEffect;
    public GameObject StunEffect { get => _stunEffect; }
}
