using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    float timer = 35;


    void Update()
    {

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
