using System.Collections;
using UnityEngine;

public class ForPlayerTest : MonoBehaviour, Idamage
{
    public bool isStunt = false;
    public bool canAttack = false;
    public bool isInvincible = false;

    public void TakeDamage()
    {
        if (isStunt == false && isInvincible == false)
        {
            Debug.Log(gameObject + "à reçu des dégats");
            isStunt = true;
            isInvincible = true;
            StartCoroutine(StopAttackIsStunt());
        }
    }

    IEnumerator StopAttackIsStunt()
    {
        yield return new WaitForSeconds(1);
        isStunt = false;
        yield return new WaitForSeconds(1);
        canAttack = true;
        isInvincible = false;
    }
}
