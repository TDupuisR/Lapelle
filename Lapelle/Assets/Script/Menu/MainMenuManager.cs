using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{
    // --- RÉFÉRENCES AUX PANNEAUX ---
    [Header("Panneaux du Menu")]
    public GameObject mainMenuPanel;   // Le menu avec Jouer/Réglages/Quitter
    public GameObject settingsPanel;   // Le menu des réglages

    void Start()
    {
        // On s'assure que seul le menu principal est visible au départ
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    // --- FONCTIONS DE NAVIGATION PRINCIPALE ---

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
    // --- AUTRES FONCTIONS (JOUER, QUITTER, ETC.) ---

    public void PlayGame()
    {
        SceneManager.LoadScene("Tom_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

}