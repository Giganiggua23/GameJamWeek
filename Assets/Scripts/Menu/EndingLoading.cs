using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingLoading : MonoBehaviour
{
    float time = 25;


    void Update()
    {
        
        time -= Time.deltaTime;
        
        if (time <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
