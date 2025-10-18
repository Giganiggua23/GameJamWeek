using UnityEngine;
using System.Collections;

public class KeypadKey : MonoBehaviour
{
    [Header("Настройки анимации")]
    [Tooltip("Насколько глубоко опускается кнопка")]
    public float pressDepth = 0.0025f;
    [Tooltip("Общее время анимации нажатия")]
    public float pressDuration = 0.1f;

    [Header("Настройки звука")]
    public AudioSource audioSource;
    public AudioClip pressSound;

    private Vector3 originalPosition;
    private KeyPad keypad;
    private Coroutine pressCoroutine;

    private void Start()
    {
        originalPosition = transform.localPosition;
        keypad = GetComponentInParent<KeyPad>();
    }

    // Этот метод будет вызван через событие onInteraction компонента Interactable
    public void PerformPress()
    {
        if (pressCoroutine == null)
        {
            pressCoroutine = StartCoroutine(PressButton());
        }
    }

    private IEnumerator PressButton()
    {
        // Проигрываем звук нажатия
        if (audioSource != null && pressSound != null)
        {
            audioSource.PlayOneShot(pressSound);
        }

        // Анимация опускания (по локальной оси Y)
        float elapsed = 0f;
        Vector3 targetPosition = originalPosition + Vector3.down * pressDepth; // Используем локальное направление
        while (elapsed < pressDuration / 2f)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsed / (pressDuration / 2f));
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPosition;

        // Регистрируем нажатие в панели KeyPad, если она есть
        if (keypad != null)
        {
            keypad.RegisterKeyPress(this);
        }
        else
        {
            Debug.LogWarning("[KeypadKey] KeyPad не найден в иерархии у: " + gameObject.name);
        }

        // Ждём оставшуюся часть времени
        yield return new WaitForSeconds(pressDuration / 2f);

        // Анимация возврата
        elapsed = 0f;
        while (elapsed < pressDuration / 2f)
        {
            transform.localPosition = Vector3.Lerp(targetPosition, originalPosition, elapsed / (pressDuration / 2f));
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
        pressCoroutine = null;
    }

    public void ResetKey()
    {
        if (pressCoroutine != null)
        {
            StopCoroutine(pressCoroutine);
            pressCoroutine = null;
        }
        transform.localPosition = originalPosition;
    }
}