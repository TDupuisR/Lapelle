using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField] private SOItems _itemInfos;
    public SOItems ItemInfos { get => _itemInfos; }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public float SpriteAlpha { set => _spriteRenderer.color = new Color(1, 1, 1, value); }

    private void Start()
    {
        if (_spriteRenderer == null)
            TryGetComponent<SpriteRenderer>(out _spriteRenderer);
    }

    public void Init(SOItems a_itemInfos)
    {
        _itemInfos = a_itemInfos;
        
        gameObject.name = _itemInfos.itemName;
        _spriteRenderer.sprite = _itemInfos.itemSprite;
    }
}
