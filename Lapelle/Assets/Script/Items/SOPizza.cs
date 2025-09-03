using UnityEngine;

[CreateAssetMenu(fileName = "SOPizza", menuName = "Scriptable Objects/SOPizza")]
public class SOPizza : SOItems
{
    private SOPizza()
    {
        type = TYPE.Pizza;
        itemValue = 1.0f;
    }
}
