using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float openRot = -90f; // Угол открытия
    public float closeRot = 0f;  // Угол закрытия
    public float speed = 2f;
    public bool opening; // Состояние двери

    // Добавленные переменные для звука
    public AudioSource audioSource;
    public AudioClip doorSound;
    private bool wasOpening; // Предыдущее состояние

    void Start()
    {
        wasOpening = opening;
    }

    void Update()
    {
        Vector3 currentRot = door.transform.localEulerAngles;
        float currentY = NormalizeAngle(currentRot.y);
        float targetY = opening ? openRot : closeRot;
        float newY = Mathf.LerpAngle(currentY, targetY, speed * Time.deltaTime);
        door.transform.localEulerAngles = new Vector3(currentRot.x, newY, currentRot.z);

        // Добавленная логика для звука
        if (opening != wasOpening)
        {
            if (audioSource != null && doorSound != null)
            {
                audioSource.PlayOneShot(doorSound);
            }
            wasOpening = opening;
        }
    }

    float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
}