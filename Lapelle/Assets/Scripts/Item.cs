using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public enum Effect
    {
        Aaa,
        Bbb,
        Ccc
    }

    public string nom = "NONE";
    public int value = 0;
    public Effect effect;
    public Sprite image;
    public float effectVal;
}
