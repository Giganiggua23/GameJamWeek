using UnityEngine;
using TMPro;

public class TimeToFail : MonoBehaviour
{

    [SerializeField] float Time_ = 30;
    public float Timer; // all sec
    int TimeMin;
    int TimeSec;


    public TMP_Text timeText1;
    public TMP_Text timeText2;
    public TMP_Text timeText3;


    //Trigger
    public bool OnTrig = false;




    //Rocket anim ~
    public Animator animator;
    [SerializeField] GameObject RocketVFX;

    private bool animationPlay = false;


    public SayText sayText;


    void Start()
    {
        Timer = Time_;

        timeText1.text = $"{TimeMin:00}:{TimeSec:00}";
        timeText2.text = $"{TimeMin:00}:{TimeSec:00}";
        timeText3.text = $"{TimeMin:00}:{TimeSec:00}";

        RocketVFX.SetActive(false);
    }

    void Update()
    {
        if (OnTrig == true)
        {
            Timer -= Time.deltaTime;

            if (Timer > 0)
            {
                TimeMin = Mathf.FloorToInt(Timer / 60f);   // min
                TimeSec = Mathf.FloorToInt(Timer % 60f);   // sec
            }

            timeText1.text = $"{TimeMin:00}:{TimeSec:00}";
            timeText2.text = $"{TimeMin:00}:{TimeSec:00}";
            timeText3.text = $"{TimeMin:00}:{TimeSec:00}";

            if (animationPlay == false && Timer <= 0)
            {
                PlayAnim();
            }
        }

    }


    void PlayAnim()
    {
        //RocketParalalala

        animator.Play("RocketAnim");
        RocketVFX.SetActive(true);
        animationPlay = true;

        sayText.FirstText();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTrig = true;
        }
    }

}