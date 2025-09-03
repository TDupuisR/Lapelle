using UnityEngine;

public class RatLife : MonoBehaviour , Idamage
{
    public Collider2D playerCollider;

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Player"))
        {
            playerCollider = enemy;
        }
        
    }
    private void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Player"))
        {
            playerCollider = null;
        }
    }
    public void TakeDamage()
    {
        Debug.Log(playerCollider.gameObject + "A toucher le rat gg");
    }
}
