using UnityEngine;

public class TasCharbon : MonoBehaviour , IInteract
{
    [SerializeField] SOItems[] listItems;
    private SOItems thisItems;

    public void Interact(PlayerInteractions a_player)
    {
        //if(a_player.Item != null)
        //{
        //    RandomItem();
        //    a_player.GiveItem(thisItems);
        //}
    }

    void RandomItem()
    {
        int randomize = Random.Range(0, listItems.Length);
        thisItems = listItems[randomize];
    }
}
