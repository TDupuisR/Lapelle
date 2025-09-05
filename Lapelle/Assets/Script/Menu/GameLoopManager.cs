using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoopManager : MonoBehaviour
{
    // Affiche le texte du Timer dans l'UI
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private float _time = 300f;

    private bool _gameIsRunning;
    public bool GameIsRunning { get => _gameIsRunning; }

    private bool _waitForEndInput = false;
    public bool WaitForEndInput { get => _waitForEndInput; }
    
    [SerializeField] private Image _BlackFade;
    
    [SerializeField] public Image _finalImage;
    [SerializeField] public Sprite _player1Victory;
    [SerializeField] public Sprite _player2Victory;
    [SerializeField] public TextMeshProUGUI _waitForInput;

    // Référence vers le script qui gère les scores des joueurs
    [SerializeField] private OvenManager scoreManager; 
    [SerializeField] private PlayerManagement playerManager;

    private CameraShake cameraShake;

    [Space(10)]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _musicMenu;
    [SerializeField] private AudioClip _musicGame;
    [SerializeField] private AudioClip _musicStart;
    [SerializeField] private AudioClip _musicEnd;
    
    private void Start()
    {
        cameraShake = FindAnyObjectByType<CameraShake>();
        _finalImage.color = new Color(1, 1, 1, 0);
        _gameIsRunning = false;
        _waitForEndInput = false;

        _BlackFade.color = Color.black;
        StartCoroutine(BlackFadeCoroutine(false));
        
        _musicSource.clip = _musicMenu;
        _musicSource.Play();
    }

    /*private void Update()
    {
        // Check if the 'T' key is pressed down
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Test de fin du timer avec la touche T");

            if (cameraShake != null)
            {
                cameraShake.Shake(0.5f, 0.25f);
            }

            ShowWinner();
        }
    }*/

    public void StartTimer()
    {
        _gameIsRunning = true;
        
        _musicSource.Stop();
        _musicSource.clip = _musicStart;
        _musicSource.loop = false;
        _musicSource.Play();
        
        StartCoroutine(GameTimerCoroutine());
    }
    private IEnumerator GameTimerCoroutine()
    {
        float timer_total = _time; // Minuteur de 5 minutes

        while (timer_total > 0f) // Tant que le timer est pas à 0
        {
            timer_total -= Time.fixedDeltaTime;
            _timer.text = timer_total % 60f >= 10 ? $"{(int)(timer_total / 60f)}:{(int)(timer_total % 60f)}" : $"{(int)(timer_total / 60f)}:0{(int)(timer_total % 60f)}";
            yield return new WaitForFixedUpdate();
            
            if (!_musicSource.isPlaying)
            {
                _musicSource.Stop();
                _musicSource.clip = _musicGame;
                _musicSource.loop = true;
                _musicSource.Play();
            }
        }

        int winner = DetermineWinner();
        if (winner == 0)
        {
            _timer.text = $"Overtime!";
            _timer.color = Color.red;
        }

        while (winner <= 0)
        {
            winner = DetermineWinner();
            
            yield return new WaitForFixedUpdate();
        }

        _musicSource.Stop();
        _musicSource.clip = _musicEnd;
        _musicSource.loop = false;
        _musicSource.Play();
        
        if (ShowWinner(winner))
        {
            float value = 0f;
            while (_finalImage.color.a < 1f || _musicSource.isPlaying)
            {
                value += (Time.fixedDeltaTime * 2);
                _finalImage.color = new Color(1, 1, 1, value);
                
                yield return new WaitForFixedUpdate();
            }
        }

        _waitForEndInput = true;
        
        _musicSource.Stop();
        _musicSource.clip = _musicMenu;
        _musicSource.loop = true;
        _musicSource.Play();

        float pingPong = 0f;
        
        while (true)
        {
            pingPong += Time.fixedDeltaTime;
            if (pingPong > 2f) pingPong -= 2f;

            _waitForInput.color = Color.Lerp(new Color(0, 0, 0, 0.25f), Color.black, Mathf.PingPong(pingPong, 1f));
            
            yield return new WaitForFixedUpdate();
        }
    }

    private bool ShowWinner(int a_winner)
    {
        Debug.Log($"Winner is player{a_winner}");
        playerManager.CallEnd();
        
        _finalImage.color = new Color(1, 1, 1, 0);
        
        if (_finalImage != null)
        {
            if (a_winner == 1)
                _finalImage.sprite = _player1Victory;
            else if (a_winner == 2)
                _finalImage.sprite = _player2Victory;

            return true;
        }
        else
        {
            Debug.LogError("Le composant '_finalImage' n'est pas assigné dans l'inspecteur.");
            return false;
        }
    }

    private int DetermineWinner()
    {
        if (scoreManager != null)
        {
            if (scoreManager.Player1Score > scoreManager.Player2Score)
            {
                return 1;
            }
            else if (scoreManager.Player2Score > scoreManager.Player1Score)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            Debug.LogError("Le 'scoreManager' n'est pas assigné. Impossible de déterminer le vainqueur.");
            return 0;
        }
    }

    public void ResetGame()
    {
        _waitForEndInput = false;
        StartCoroutine(BlackFadeCoroutine(true));
    }

    private IEnumerator BlackFadeCoroutine(bool a_fadeOut)
    {
        float timer = 1f;

        while (timer > 0f)
        {
            timer -= Time.fixedDeltaTime;
            
            _BlackFade.color = a_fadeOut ? Color.Lerp(new Color(0, 0, 0, 0), Color.black, 1 - timer) : 
                                          Color.Lerp(Color.black, new Color(0, 0, 0, 0), 1 - timer);

            _musicSource.volume = a_fadeOut ? timer : 1 - timer;
            
            yield return new WaitForFixedUpdate();
        }
        
        _BlackFade.color = a_fadeOut ? Color.black : new Color(0, 0, 0, 0);

        if (a_fadeOut)
        {
            SceneManager.LoadScene("Main_Scene");
        }
    }
}