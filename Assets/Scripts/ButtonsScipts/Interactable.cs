using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [Tooltip("Сообщение для отладки (не обязательно)")]
    public string message;
    [Tooltip("Событие, выполняемое при взаимодействии")]
    public UnityEvent onInteraction;

    private Outline outline;

    private void Awake()
    {
        // Если у объекта есть компонент Outline, выключаем его по умолчанию
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    public void Interact()
    {
        Debug.Log("[Interactable] " + gameObject.name + " получило событие взаимодействия. " + message);
        onInteraction.Invoke();
    }

    public void EnableOutline()
    {
        if (outline != null)
            outline.enabled = true;
    }

    public void DisableOutline()
    {
        if (outline != null)
            outline.enabled = false;
    }
}
