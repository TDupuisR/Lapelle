using UnityEngine;

public class InteractionCharbon : MonoBehaviour
{
    public bool isCharbon = false;
    public bool isCharbonBas = false;
    public bool isCharbonMilieu = false;
    public bool haveCharbon = false;
    public bool isZoneCharbon = false;
    public GameObject chabonSprite;
    public ItemPlayer itemPlayer;
    public GiveItemSystem giveItemSystem;

    // Update is called once per frame
    void Update()
    {
        if (isCharbon && isCharbonBas)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                AddCharbonBas();
            }
        }

        if (isCharbon && isCharbonMilieu)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                AddCharbonMilieu();
            }
        }

        if (isZoneCharbon)
        {
            if (Input.GetKeyDown(KeyCode.I) && haveCharbon)
            {
                RemoveCharbon();
            }
        }

        if (haveCharbon && itemPlayer.item.nom == "Charbon")
        {
            chabonSprite.SetActive(true);
        }
        else
        {
            chabonSprite.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TasCharbonBas"))
        {
            isCharbonBas = true;
            isCharbon = true;

        }

        else if (collision.CompareTag("TasCharbonMilieu"))
        {
            isCharbonMilieu = true;
            isCharbon = true;

        }

        else if (collision.CompareTag("Four"))
        {
            isZoneCharbon = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TasCharbonBas"))
        {
            isCharbonBas = false;
            isCharbon = false;
        }

        else if (collision.CompareTag("TasCharbonMilieu"))
        {
            isCharbonMilieu = false;
            isCharbon = false;
        }

        if (collision.CompareTag("Four"))
        {
            isZoneCharbon = false;
        }
    }

    public void AddCharbonBas()
    {
        Debug.Log("Le joueur r�cup�re un objet");
        haveCharbon = true;
        giveItemSystem.RamdomItemEnBas();
    }

    public void AddCharbonMilieu()
    {
        Debug.Log("Le joueur r�cup�re un objet");
        haveCharbon = true;
        giveItemSystem.RamdomItemAuMilieu();
    }

    public void RemoveCharbon()
    {
        Debug.Log("Le joueur d�pose un objet");
        haveCharbon = false;
        itemPlayer.item = null;
    }
}
