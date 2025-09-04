using System.Collections;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour, IInteract
{
    [SerializeField] private SOPizza _pizzaInfos;
    [SerializeField] private GameObject _pizzaPrefab;

    [Space(7)]
    [SerializeField] private ItemBehaviour _itemScript;
    [SerializeField] private Transform _itemReceveiver;
    [Space(5)]
    [SerializeField] private Transform _animStart;
    [SerializeField] private Transform _animEnd;
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private float _animTime;

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
        else
        {
            Destroy(item);
            Debug.LogError($"Could not find 'ItemBehaviour' class on spawned item'", gameObject);
        }
        
        StartCoroutine(SpawnAnim());
    }
    private IEnumerator SpawnAnim()
    {
        float currentTime = 0;

        while (currentTime < _animTime)
        {
            currentTime += Time.fixedDeltaTime;
            
            _itemReceveiver.localPosition = Vector3.Lerp(_animStart.localPosition, _animEnd.localPosition, _animCurve.Evaluate(currentTime / _animTime));
            _itemScript.SpriteAlpha = _animCurve.Evaluate(currentTime / _animTime);
            yield return new WaitForFixedUpdate();
        }

        _itemReceveiver.localPosition = _animEnd.localPosition;
        yield return null;
    }

    public void Interact(PlayerInteractions a_player)
    {
        if (a_player.ItemInfos != null ||
            _itemScript == null)
            return;

        a_player.GiveItem(_itemScript);
    }
}
