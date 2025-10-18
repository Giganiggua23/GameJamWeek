using UnityEngine;

public class AlarmLampOn : MonoBehaviour
{

    [SerializeField] GameObject AlarmLamp;
    

   
    TimeToFail timeTo;

    bool a;

    public void Start()
    {
        timeTo = FindObjectOfType<TimeToFail>();

        AlarmLamp.SetActive(false);

        a = false;
    }


    void FixedUpdate()
    {

        if (timeTo.OnTrig == true && !a)
        {
            Debug.Log("Activated");

            AlarmLamp.SetActive(true);
            a = true;
        }
    }
}
