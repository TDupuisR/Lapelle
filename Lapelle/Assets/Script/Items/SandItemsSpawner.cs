using UnityEngine;
using System.Collections.Generic;

public class SandItemsSpawner : MonoBehaviour
{
    [SerializeField] private List<ItemFrequency> _itemFrequencies = new List<ItemFrequency>();
    private int _numberOfItems;
    [SerializeField] private GameObject _itemPrefab;
    
    [Space(7)]
    [SerializeField] private int _digRepeat = 20;
    private int _digCount;
    
    private void Start()
    {
        for (int i = 0; i < _itemFrequencies.Count; i++)
        {
            _numberOfItems += _itemFrequencies[i].frequency;
        }
        
        _digCount = _digRepeat;
    }

    public void Interact(PlayerInteractions a_player)
    {
        if (a_player.ItemInfos != null)
            return;

        if (_digCount > 0)
        {
            _digCount--;
            return;
        }
        else
        {
            _digCount = _digRepeat;
            
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
    }
}
