using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour , Idamage
{
    private Vector2 _moveInput; 
    public float speed = 5f;
    public bool canAttack = true;
    public bool isStunt = false;
    public bool isInvincible = false;
    public float reload = 1f;
    public bool canAttackEnemy = false;
    public Collider2D enemyCollider;

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        if(isStunt == false)
        {
            Vector3 move = new Vector3(_moveInput.x, _moveInput.y, 0f);
            transform.Translate(move * speed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0) && enemyCollider != null)
        {
            if (canAttack == true && isStunt == false)
            {
                Idamage target = enemyCollider.GetComponent<Idamage>();
                target.TakeDamage();
                Debug.Log("Attack de" + gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Player"))
        {
            canAttackEnemy = true;
            enemyCollider = enemy;
        }
    }
    private void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Player"))
        {
            canAttackEnemy = false;
            enemyCollider = null;
        }
    }
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
