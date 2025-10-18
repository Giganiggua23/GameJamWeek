using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PcController : MonoBehaviour
{
    public string password;
    public int passwordLimit = 6;
    public TMP_Text passwordText;

    [Header("Scene Settings")]
    public float sceneChangeDelay = 2f; // Время задержки перед сменой сцены
    public int targetSceneIndex = 4; // Индекс целевой сцены

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;
    public string mixerGroupName = "Everything";

    private bool isActive = false;
    private bool passwordCorrect = false;
    private float originalVolume;

    private void Start()
    {
        if (passwordText != null)
        {
            passwordText.text = "";
        }

        // Получаем исходную громкость
        if (audioMixer != null)
        {
            audioMixer.GetFloat(mixerGroupName, out originalVolume);
        }
    }

    public void SetActive(bool active)
    {
        isActive = active;
        if (!active && !passwordCorrect)
        {
            Clear();
        }
    }

    public void PasswordEntry(string number)
    {
        if (!isActive || passwordCorrect) return;

        if (number == "Clear")
        {
            Clear();
            return;
        }
        else if (number == "Enter")
        {
            Enter();
            return;
        }

        if (passwordText != null && passwordText.text.Length <= passwordLimit - 1)
        {
            passwordText.text += number;
        }
    }

    public void Clear()
    {
        if (passwordText != null && !passwordCorrect)
        {
            passwordText.text = "";
            passwordText.color = Color.white;
        }
    }

    private void Enter()
    {
        if (passwordText != null && passwordText.text == password)
        {
            if (audioSource != null && correctSound != null)
                audioSource.PlayOneShot(correctSound);

            passwordText.color = Color.green;
            passwordCorrect = true;

            Time.timeScale = 0f;
            StartCoroutine(MuteMixerGroupCoroutine());

            // Запускаем корутину для смены сцены через указанное время
            StartCoroutine(ChangeSceneAfterDelay());
        }
        else
        {
            if (audioSource != null && wrongSound != null)
                audioSource.PlayOneShot(wrongSound);

            passwordText.color = Color.red;
            StartCoroutine(WaitAndClear());
        }
    }

    private IEnumerator WaitAndClear()
    {
        yield return new WaitForSecondsRealtime(0.75f); // Используем WaitForSecondsRealtime
        Clear();
    }

    private IEnumerator MuteMixerGroupCoroutine()
    {
        yield return null;
        if (audioMixer != null)
        {
            audioMixer.SetFloat(mixerGroupName, -80f);
        }
    }

    private IEnumerator UnmuteMixerGroupCoroutine()
    {
        yield return null;
        if (audioMixer != null)
        {
            audioMixer.SetFloat(mixerGroupName, originalVolume);
        }
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        // Ждем указанное время (используем реальное время, так как Time.timeScale = 0)
        yield return new WaitForSecondsRealtime(sceneChangeDelay);

        // Восстанавливаем время перед сменой сцены
        Time.timeScale = 1f;

        // Загружаем целевую сцену
        SceneManager.LoadScene(targetSceneIndex);
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
        StartCoroutine(UnmuteMixerGroupCoroutine());
        passwordCorrect = false;
        Clear();
    }
}