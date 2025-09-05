using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemFrequency
{
    public SOFuel _fuelItemInfos;
    public int frequency;
}
public class ItemsSpawner : MonoBehaviour, IInteract
{
    [SerializeField] private List<ItemFrequency> _itemFrequencies = new List<ItemFrequency>();
    private int _numberOfItems;
    [SerializeField] private GameObject _itemPrefab;
    
    [Space(7)]
    [SerializeField] private ItemBehaviour _itemScript;
    [SerializeField] private Transform _itemReceveiver;

    private void Start()
    {
        for (int i = 0; i < _itemFrequencies.Count; i++)
        {
            _numberOfItems += _itemFrequencies[i].frequency;
        }
    }

    public void SpawnItem()
    {
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
        
        GameObject itemGameObject = Instantiate(_itemPrefab, _itemReceveiver);

        if (itemGameObject.TryGetComponent<ItemBehaviour>(out _itemScript))
        {
            _itemScript.Init(item);
            _itemScript.transform.localPosition = Vector3.zero;
            _itemScript.transform.localScale = Vector3.one * 0.7f;
        }
        else
        {
            Destroy(itemGameObject);
            Debug.LogError($"Could not find 'ItemBehaviour' class on spawned item'", gameObject);
        }
    }
    
    public bool Interact(PlayerInteractions a_player)
    {
        if (a_player.ItemInfos != null ||
            _itemScript == null)
            return false;

        a_player.GiveItem(_itemScript);
        a_player.Core.audioSource.PlayOneShot(a_player.Core.digSound);
        SpawnItem();
        return true;
    }
}
