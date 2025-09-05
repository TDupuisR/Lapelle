using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    public PlayerCore Core { get => _playerCore; }

    public bool canInteract;
    
    private List<IInteract> _interactors = new List<IInteract>();
    private IInteract _playerInteract = null;

    private ItemBehaviour _item;
    public SOItems ItemInfos
    {
        get => _item != null ? _item.ItemInfos : null;
    }

    private void Start()
    {
        if (_playerCore == null)
            TryGetComponent<PlayerCore>(out _playerCore);
    }

    public bool GiveItem(ItemBehaviour a_item)
    {
        if (_item == null)
        {
            _item = a_item;
            _item.transform.parent = Core.Socket;
            _item.transform.localPosition = Vector3.zero;
            
            return true;
        }
        else
            return false;
    }
    public SOItems TakeItem()
    {
        if (_item == null)
            return null;

        SOItems item = _item.ItemInfos;
        Destroy(_item.gameObject);
        _item = null;
        return item;
    }

    public void DropItem()
    {
        if (_item == null)
            return;
        
        if (_item.ItemInfos.type == SOItems.TYPE.Pizza)
        {
            Core.PizzaServing.SpawnPizza();
        }
        Destroy(_item.gameObject);
        _item = null;
    }
    
    public void OnAction1(InputValue value)
    {
        if (canInteract && value.Get<float>() > 0f)
        {
            if (_playerInteract != null)
                _playerInteract.Interact(this);
            else
            {
                for (int i = 0; i < _interactors.Count; i++)
                {
                    if (_interactors[0].Interact(this))
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteract>(out IInteract o_interactor))
        {
            if (!_interactors.Contains(o_interactor))
            {
                if (collision.TryGetComponent<PlayerCore>(out PlayerCore o_playerCore))
                {
                    _playerInteract = o_interactor;
                }
                else
                {
                    _interactors.Add(o_interactor);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteract>(out IInteract o_interactor))
        {
            if (o_interactor == _playerInteract)
                _playerInteract = null;
            else
                _interactors.Remove(o_interactor);
        }
    }

    public bool HasItem()
    {
        return _item != null;
    }
}
