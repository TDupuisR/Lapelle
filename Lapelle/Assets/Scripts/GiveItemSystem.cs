using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


[Serializable]
public struct listItem
{
    public Item item;
    public int pourcentageEnBas;
    public int pourcentageAuMilieu;
}

public class GiveItemSystem : MonoBehaviour
{
    public List<listItem> liste_des_items = new List<listItem>();
    public ItemPlayer playerItem;
    public int maxPourcentage;
    public int randomize = 0;
    public bool isBottom = true;

    public void Start()
    {
        Debug.Log(liste_des_items.Count);
    }
    
    public void RamdomItemEnBas()
    {
        Debug.Log("En Bas debut");
        for (int i = 0; i < liste_des_items.Count; i++)
        {
            maxPourcentage = maxPourcentage + liste_des_items[i].pourcentageEnBas;
        }
        Debug.Log("Max Pourcentage = " +  maxPourcentage);
        randomize = UnityEngine.Random.Range(0, maxPourcentage);
        ResultEnBas(randomize, 0);
        Debug.Log(randomize);
    }

    public void RamdomItemAuMilieu()
    {
       Debug.Log("Au Milieu debut");
       for (int i = 0; i < liste_des_items.Count; i++)
       {
           maxPourcentage = maxPourcentage + liste_des_items[i].pourcentageAuMilieu;
       }
        Debug.Log("Max Pourcentage = " + maxPourcentage);
        randomize = UnityEngine.Random.Range(0, maxPourcentage);
       ResultAuMilieu(randomize, 0);
       Debug.Log(randomize);
    }

    public void ResultEnBas(int valueRandom, int element)
    {
          Debug.Log(valueRandom + "debut");

          if (valueRandom > liste_des_items[element].pourcentageEnBas)
          {
              valueRandom = valueRandom - liste_des_items[element].pourcentageEnBas;
                if (element == liste_des_items.Count - 1)
                {
                element = 0;
                }
                element++;
                Debug.Log("non trop petit" + valueRandom);
                
                ResultEnBas(valueRandom, element);
          }
          else
          {
              playerItem.item = liste_des_items[element].item;
              Debug.Log("Item ajouté est : " + liste_des_items[element].item.nom);
              maxPourcentage = 0;
          }
    }

    public void ResultAuMilieu(int valueRandom, int element)
    {
        Debug.Log(valueRandom + "debut");

        if (valueRandom > liste_des_items[element].pourcentageAuMilieu)
        {
            valueRandom = valueRandom - liste_des_items[element].pourcentageAuMilieu;
            if (element == liste_des_items.Count - 1)
            {
                element = 0;
            }
            element++;
            Debug.Log("non trop petit" + valueRandom);

            ResultAuMilieu(valueRandom, element);
        }
        else
        {
            playerItem.item = liste_des_items[element].item;
            Debug.Log("Item ajouté est : " + liste_des_items[element].item.nom);
            maxPourcentage = 0;
        }
    }
}
