using NUnit.Framework.Internal;
using UnityEngine;

public class TasCharbon : MonoBehaviour , IInteract
{
    [SerializeField] SOItems[] listItem;
    [SerializeField] GameObject itemPrefab;

    public void Interact(PlayerInteractions a_player)
    {
        //if (a_player.ItemInfos == null)
        if(!a_player.HasItem())
        {
            SOItems randomItem = RandomItem();

            GameObject newItemObj = Object.Instantiate(itemPrefab);
            ItemBehaviour newItem = newItemObj.GetComponent<ItemBehaviour>();
            newItem.Init(randomItem);
            // give au joueur
            a_player.GiveItem(newItem);
        }
        else
        {
            Debug.Log("Le joueur porte déjà un objet !");
        }
    }
    SOItems RandomItem()
    {
        int randomize = Random.Range(0, listItem.Length);
        Debug.Log(randomize);
        return listItem[randomize];
    }
}

