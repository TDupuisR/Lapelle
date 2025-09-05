using UnityEngine;
using System.Collections.Generic;

public class SandItemsSpawner : MonoBehaviour, IInteract
{
    [SerializeField] private List<ItemFrequency> _itemFrequencies = new List<ItemFrequency>();
    private int _numberOfItems;
    [SerializeField] private GameObject _itemPrefab;
    
    public bool interactable = false;
    
    [Space(7)]
    [SerializeField] private float _digChance = 0.1f;
    
    private void Start()
    {
        for (int i = 0; i < _itemFrequencies.Count; i++)
        {
            _numberOfItems += _itemFrequencies[i].frequency;
        }
        
        interactable = false;
    }

    public bool Interact(PlayerInteractions a_player)
    {
        if (a_player.ItemInfos != null ||
            !interactable)
            return false;

        float digging = Random.Range(0f, 1f);
        
        if (digging > _digChance)
        {
            a_player.Core.ChangeAnimationState(AnimationState.Dig);
            a_player.Core.audioSource.PlayOneShot(a_player.Core.digSound);
        }
        else
        {
            a_player.Core.ChangeAnimationState(AnimationState.Dig);
            a_player.Core.audioSource.PlayOneShot(a_player.Core.digSound);
            
            int rand = Random.Range(1, _numberOfItems);
            SOFuel item = null;

            for (int i = 0; i < _itemFrequencies.Count; i++)
            {
                if (_itemFrequencies[i].frequency >= rand)
                {
                    item = _itemFrequencies[i]._fuelItemInfos;
                    break;
                }
                else
                {
                    rand -= _itemFrequencies[i].frequency;
                }
            }
            
            GameObject itemGameObject = Instantiate(_itemPrefab);
            if (itemGameObject.TryGetComponent<ItemBehaviour>(out ItemBehaviour _itemScript))
            {
                _itemScript.Init(item);
            }
            else
            {
                Destroy(itemGameObject);
                Debug.LogError($"Could not find 'ItemBehaviour' class on spawned item'", gameObject);
            }
            
            a_player.GiveItem(_itemScript);
        }
        
        return true;
    }
}
