using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private RSO_Art art;
    public Image image;


    private void Start()
    {
        image.sprite = art.MainMenuBackgroundSprite;
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Main");
    }


    public void QuitButton()
    {
        Application.Quit();
    }
}
