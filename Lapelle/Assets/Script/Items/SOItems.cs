using UnityEngine;

[CreateAssetMenu(fileName = "SOItems", menuName = "Scriptable Objects/SOItems")]
public class SOItems : ScriptableObject
{
    public enum TYPE
    {
        Fuel,
        Pizza
    }

    public TYPE type;
    
    public string itemName = "NONE";
    public Sprite itemSprite = null;
    public float itemValue = .0f;
}
