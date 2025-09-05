using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuPanel;
    public GameObject settingsPanel;

    void Update()
    {
        // Détecte la touche echap
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    // --- Fonctions de navigation entre les panneaux ---

    public void OpenSettings()
    {
        // On quitte le menu pause pour aller aux réglages
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        // On quitte les réglages pour retourner au menu pause
        settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    // --- Fonctions de gestion du jeu ---

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Menu_Scene");
    }
}
