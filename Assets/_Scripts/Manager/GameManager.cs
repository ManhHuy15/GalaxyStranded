using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    [SerializeField] private TMP_Text txtPause;
    bool isRun = true;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gamePauseUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        AudioManager.Instance.PlaySoundEffect(SoundEffectType.Click);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(1);
    }
    public void RestartGame()
    {
        AudioManager.Instance.PlaySoundEffect(SoundEffectType.Click);
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    public void BackMenu()
    {
        AudioManager.Instance.PlaySoundEffect(SoundEffectType.Click);
        SceneManager.LoadScene("StartGame");
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySoundEffect(SoundEffectType.Click);
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverUI is not assigned in the Inspector!");
        }
        AudioManager.Instance.GameOverMusic();
        Time.timeScale = 0;
    }

    public void ClickPause()
    {
        AudioManager.Instance.PlaySoundEffect(SoundEffectType.Click);
        if (isRun)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        gamePauseUI.SetActive(true);
        isRun = false;
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        gamePauseUI.SetActive(false);
        isRun = true;
        Time.timeScale = 1;
    }
}
