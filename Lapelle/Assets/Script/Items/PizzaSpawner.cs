using NUnit.Framework.Constraints;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour, IInteract
{
    [SerializeField] private SOPizza _pizzaInfos;
    [SerializeField] private GameObject _pizzaPrefab;

    [Space(7)]
    [SerializeField] ItemBehaviour _itemScript;
    [SerializeField] Transform _itemReceveiver;

    private void Start()
    {
        _itemScript = null;
    }

    public void SpawnPizza()
    {
        GameObject item = Instantiate(_pizzaPrefab, _itemReceveiver);

        if (item.TryGetComponent<ItemBehaviour>(out _itemScript))
        {
            _itemScript.Init(_pizzaInfos);
            _itemScript.transform.localPosition = Vector3.zero;
        }
    }

    public void Interact(PlayerInteractions a_player)
    {
        if (a_player.ItemInfos != null ||
            _itemScript == null)
            return;

        a_player.GiveItem(_itemScript);
    }
}
