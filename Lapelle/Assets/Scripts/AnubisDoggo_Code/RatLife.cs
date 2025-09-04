using UnityEngine;

public class RatLife : MonoBehaviour , Idamage
{
    public Collider2D playerCollider;
    public PlayerCore playerCore;

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Player"))
        {
            playerCore = enemy.GetComponent<PlayerCore>();

            if (playerCore.PlayerID == 0)
            {
                Debug.Log("Joueur 1 à touché le rat");
            }

            else if (playerCore.PlayerID == 1)
            {
                Debug.Log("Joueur 2 à touché le rat");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Player"))
        {
            playerCore = null;
        }
    }

    public void TakeDamage()
    {
        
    }
}
