using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    //[SerializeField] GameObject SettingsButton;
    [SerializeField] GameObject SettingsMenu;


    void Start()
    {
       // SettingsButton.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(3);
    }


    public void SettingsOpenClose()
    {
        if (SettingsMenu.activeSelf)
        {
            SettingsMenu.SetActive(false);
        }
        else
        {
            SettingsMenu.SetActive(true);
        }
    }

   

    public void Exit()
    {
        Application.Quit();
    }

}
