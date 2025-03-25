using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManager.Instance.PlaySoundEffect(SoundEffectType.Click);
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySoundEffect(SoundEffectType.Click);
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
