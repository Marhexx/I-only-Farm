using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicBG;
    [SerializeField] private Image pauseImage;
    [SerializeField] private Sprite activeSound;
    [SerializeField] private Sprite inactiveSound;
    [SerializeField] private GameObject escPanel;
    
    private bool _isPlaying = true;
    
    public void TogglePause()
    {
        _isPlaying = !_isPlaying;
        if (_isPlaying)
        {
            musicBG.volume = 1;
            pauseImage.sprite = activeSound;
        }
        else
        {
            musicBG.volume = 0;
            pauseImage.sprite = inactiveSound;
        }
    }
    
    public void PlayGame() => SceneManager.LoadScene("Tutorial");
    
    public void ExitGame() => Application.Quit();

    public void ReturnToGame()
    {
        if (escPanel.activeSelf) escPanel.SetActive(false);   
    } 
    
    public void BackToMainMenu() => SceneManager.LoadScene("Menu");
}
