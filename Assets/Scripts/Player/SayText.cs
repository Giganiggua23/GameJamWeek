using UnityEngine;
using TMPro;

public class SayText : MonoBehaviour
{
    [SerializeField] float TimeTimer = 6;
    float Timer;

    [SerializeField] public TextMeshProUGUI Text;

    void Start()
    {
        Timer = TimeTimer;
    }


    void Update()
    {
        if (Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }


        if (Timer < 0)
        {
            Text.text = (" ");
        }

    }

    public void FirstText()
    {
        Text.text = ("Чёрт, запуск ракеты уже начался\r\nно кажется мне все равно стоит добраться\r\nдо лаборатории, профессор говорил мне про секретную разработку\r\n");
        Timer = TimeTimer;

    }

    void SecondText()
    {
        Timer = TimeTimer;
        Text.text = ("вот оно что, профессор разработал машину для перемещении\r\nво времени, если она работает - то это мой единственный шанс");


    }

    public void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player"))
        {
            SecondText();
        }
    }
}
