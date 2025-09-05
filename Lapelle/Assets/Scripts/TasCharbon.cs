using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public struct PourcentageItem
{
    public SOItems listItems;
    public int pourcentageBas;
    public int pourcentageMilieu;
}

[Serializable]
public enum TypeTas
{
    TasBas,
    TasMilieu
}

public class TasCharbon : MonoBehaviour , IInteract
{
    [SerializeField] private TypeTas typeTasCharbon;
    [SerializeField] SOItems[] listItem;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] SOItems randomItem;
    private GameObject newItemObj;
    private ItemBehaviour newItem;
    [SerializeField] private PourcentageItem[] listItemsAvecPourcentage;

    //pour random
    private int maxPourcentage = 0;
    private int randomize = 0;
    private int resultFinal = 0;

    public void Generated()
    {
       randomItem = RandomItem();
       newItemObj = UnityEngine.Object.Instantiate(itemPrefab);
       newItem = newItemObj.GetComponent<ItemBehaviour>();
    }

    public bool Interact(PlayerInteractions a_player)
    {
        return false;
        
        //if (a_player.ItemInfos == null)
        if(!a_player.HasItem())
        {
            Generated();
            newItem.Init(randomItem);
            // give au joueur
            a_player.GiveItem(newItem);
        }
        else
        {
            Debug.Log("Le joueur porte d�j� un objet !");
        }
    }
    SOItems RandomItem()
    {
         //int randomize = UnityEngine.Random.Range(0, listItem.Length);
         //Debug.Log(randomize);
            
         for (int i = 0; i < listItemsAvecPourcentage.Length; i++)
         {
             maxPourcentage = maxPourcentage + listItemsAvecPourcentage[i].pourcentageBas;
         }
        randomize = UnityEngine.Random.Range(0, maxPourcentage);

        resultFinal = ResultEnBas(randomize,0);

        Debug.Log("Le joueur � obtenu l'objet : " + listItem[resultFinal]);
        return listItem[resultFinal];
    }

    public int ResultEnBas(int valueRandom, int element)
    {
        if (valueRandom > listItemsAvecPourcentage[element].pourcentageBas)
        {
            valueRandom = valueRandom - listItemsAvecPourcentage[element].pourcentageBas;
            if (element == listItemsAvecPourcentage.Length - 1)
            {
                element = 0;
            }
            element++;
            return ResultEnBas(valueRandom, element);
        }
        else
        {
            maxPourcentage = 0;
            return element;
        }
    }
}

