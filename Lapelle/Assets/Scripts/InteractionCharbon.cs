using UnityEngine;

public class InteractionCharbon : MonoBehaviour
{
    public bool isCharbon = false;
    public int nbrCharbon = 0;
    public bool isZoneCharbon = false;
    public GameObject chabonSprite;

    void Start()
    {
        Debug.Log("Salut");
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharbon)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Le joueur récupére un charbon");
                nbrCharbon++;
            }
        }

        if (isZoneCharbon)
        {
            if (Input.GetKeyDown(KeyCode.I) && nbrCharbon > 0)
            {
                Debug.Log("Le joueur dépose un charbon");
                nbrCharbon--;
            }
        }

        if(nbrCharbon > 0)
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
        if (collision.CompareTag("Charbon"))
        {
            isCharbon = true;
        }

        if (collision.CompareTag("ZoneCharbon"))
        {
            isZoneCharbon = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Charbon"))
        {
            isCharbon = false;
        }

        if (collision.CompareTag("ZoneCharbon"))
        {
            isZoneCharbon = false;
        }
    }
}
