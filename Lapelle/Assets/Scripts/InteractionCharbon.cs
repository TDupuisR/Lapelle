using UnityEngine;

public class InteractionCharbon : MonoBehaviour
{
    public bool isCharbon = false;
    public bool isCharbonBas = false;
    public bool isCharbonMilieu = false;
    public bool haveObject = false;
    public bool isZoneFour = false;
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

        if (isZoneFour)
        {
            if (Input.GetKeyDown(KeyCode.I) && haveObject)
            {
                RemoveObject();
            }
        }

        if (haveObject && itemPlayer.item.nom == "Charbon")
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
            isZoneFour = true;
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
            isZoneFour = false;
        }
    }

    public void AddCharbonBas()
    {
        Debug.Log("Le joueur récupére un objet");
        haveObject = true;
        giveItemSystem.RamdomItemEnBas();
    }

    public void AddCharbonMilieu()
    {
        Debug.Log("Le joueur récupére un objet");
        haveObject = true;
        giveItemSystem.RamdomItemAuMilieu();
    }

    public void RemoveObject()
    {
        Debug.Log("Le joueur dépose un objet");
        haveObject = false;
        itemPlayer.item = null;
    }
}
