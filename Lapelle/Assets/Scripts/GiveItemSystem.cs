using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


[Serializable]
public struct listItem
{
    public Item item;
    public int pourcentage;
}

public class GiveItemSystem : MonoBehaviour
{
    public List<listItem> liste_des_items = new List<listItem>();
    public ItemPlayer playerItem;
    public int maxPourcentage;
    public int randomize = 0;

    public void Start()
    {
        for (int i = 0; i < liste_des_items.Count; i++)
        {
            maxPourcentage = maxPourcentage + liste_des_items[i].pourcentage;
        }
    }
    //playerItem.item = liste_des_items[randomize].item; 
    public void RamdomItem()
    {
        randomize = UnityEngine.Random.Range(0, maxPourcentage);
        Result(randomize,0);
        Debug.Log(randomize);
    }
    public void Result(int valueRandom,int element)
    {
        Debug.Log(valueRandom + "debut");


        if (valueRandom > liste_des_items[element].pourcentage)
        {
            valueRandom = valueRandom - liste_des_items[element].pourcentage;
            element++;
            Debug.Log("non trop petit" +  valueRandom);
            Result(valueRandom,element);
        }
        else
        {
            playerItem.item = liste_des_items[element].item;
            Debug.Log("ok" + element);
        }
    }

}
