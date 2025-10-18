using UnityEngine;

public class TeleportToStart : MonoBehaviour
{
    [SerializeField] GameObject rocket1;
    [SerializeField] GameObject rocket2;

    [SerializeField] TimeToFail timeToFail;

    public void Teleport()
    {
        transform.position = new Vector3(-3.04f, 1.915f, 2.867f);
        timeToFail.Timer = 25f;

        rocket1.SetActive(false);
        rocket2.SetActive(true);
    }

}
