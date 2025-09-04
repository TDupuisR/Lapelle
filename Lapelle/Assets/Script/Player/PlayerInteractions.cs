using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    public PlayerCore Core { get => _playerCore; }

    public bool canInteract;
    
    private List<IInteract> _interactors = new List<IInteract>();

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
        _item = null;
        canInteract = false;
    }
    
    public void OnAction1(InputValue value)
    {
        if (canInteract && value.Get<float>() > 0f)
        {
            if (_interactors.Count > 0)
            {
                _interactors[0].Interact(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteract>(out IInteract o_interactor))
        {
            if (!_interactors.Contains(o_interactor))
            {
                _interactors.Add(o_interactor);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteract>(out IInteract o_interactor))
        {
            _interactors.Remove(o_interactor);
        }
    }
}
