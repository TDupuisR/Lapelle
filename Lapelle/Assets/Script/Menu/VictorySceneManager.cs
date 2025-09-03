using UnityEngine;
using TMPro;

public class VictorySceneManager : MonoBehaviour
{

    void Update()
    {
        // Pressing the 'V' key will simulate a win.
         if (Input.GetKeyDown(KeyCode.V))
         {
             WinCondition();
         }
    }
    public TextMeshProUGUI winnerTextComponent;

    private void WinCondition()
    {
        string winnerName = "Player One"; 

        ShowWinner(winnerName);
    }
    private void ShowWinner(string winnerName)
    {
        if (winnerTextComponent != null)
        {
            
            winnerTextComponent.text = "WIN !\n" + winnerName;

            winnerTextComponent.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("WinnerText pas assigné");
        }
    }
}