using UnityEngine;

[CreateAssetMenu(fileName = "SOFuel", menuName = "Scriptable Objects/SOFuel")]
public class SOFuel : SOItems
{
    private SOFuel()
    {
        type = TYPE.Fuel;
    }
    
    public enum EFFECT
    {
        aaa,
        bbb,
        ccc
    }
    
    public EFFECT effect;
    public float effectValue = 1.0f;
}
