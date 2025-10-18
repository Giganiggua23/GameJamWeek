using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    float timer = 4;


    void Update()
    {
        
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
