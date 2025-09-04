using UnityEngine;
using TMPro;
using System.Collections;

public class VictorySceneManager : MonoBehaviour
{
    // Affiche le texte du vainqueur dans l'UI
    public TextMeshProUGUI winnerTextComponent;

    // Référence vers le script qui gère les scores des joueurs
    public OvenValues scoreManager; 

    private void Start()
    {
        StartCoroutine(GameTimerCoroutine());
    }

    private void Update()
    {
        // Check if the 'T' key is pressed down
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Testing win screen via 'T' key press.");
            ShowWinner();
        }
    }

    private IEnumerator GameTimerCoroutine()
    {
        float timer_total = 300f; // Minuteur de 5 minutes

        while (timer_total > 0f) // Tant que le timer est pas à 0
        {
            timer_total -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // Le minuteur est à 0, on montre le vainqueur
        ShowWinner();
    }

    private void ShowWinner()
    {
        string winnerName = DetermineWinner();

        if (winnerTextComponent != null)
        {
            winnerTextComponent.text = "WINNER !\n" + winnerName;
            winnerTextComponent.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Le composant texte 'winnerTextComponent' n'est pas assigné dans l'inspecteur.");
        }
    }

    private string DetermineWinner()
    {
        if (scoreManager != null)
        {
            if (scoreManager.Player1Score > scoreManager.Player2Score)
            {
                return "Player 1";
            }
            else if (scoreManager.Player2Score > scoreManager.Player1Score)
            {
                return "Player 2";
            }
            else
            {
                return "It's a tie!";
            }
        }
        else
        {
            Debug.LogError("Le 'scoreManager' n'est pas assigné. Impossible de déterminer le vainqueur.");
            return "No winner found";
        }
    }
}