using UnityEngine;

public class HammerAttack : MonoBehaviour
{
    //Animation
    [SerializeField] Animator animator;


    public float playerReach = 4f;

    Health health;
    [SerializeField] int damage = 20;

    //CoolDown
    bool coolDown = false;
    [SerializeField] float timerCoolDown = 0f;
    float Timer;

    [SerializeField] float timerForAttack = 0f;
    float TimerForAttack = 1.3f;

    void Start()
    {
        TimerForAttack = timerForAttack;
        Timer = timerCoolDown;
    }

    void Update()
    {
        if (coolDown)
        {
            TimerForAttack -= Time.deltaTime;
            Timer -= Time.deltaTime;


            if (Timer <= 0)
            {
                coolDown = false;
                Timer = timerCoolDown;
            }
        }




        if (Input.GetMouseButtonDown(0) && !coolDown)
        {
            animator.Play("HammerAnim");

            MeleeAttack();
            coolDown = true;
        }
    }

    //Debug.DrawRay(transform.position, transform.forward * 5f, Color.red, 5f);

    void MeleeAttack()
    {
        if (TimerForAttack <= 0)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, playerReach))
            {
                Debug.DrawRay(transform.position, transform.forward * 5f, Color.red, 5f);
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Enemy"))
                {
                    //Debug.DrawRay(transform.position, transform.forward * 5f, Color.red, 5f);
                    health = hitObject.GetComponent<Health>();
                    health.TakeDamage(damage);
                    TimerForAttack = timerForAttack;
                }
            }
        }
    }

}
