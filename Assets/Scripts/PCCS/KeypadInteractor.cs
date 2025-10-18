using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class KeypadInteractor : MonoBehaviour
{
    public PcController keypadController;
    public float interactionDistance = 2f;

    [Header("Cinemachine Settings")]
    public CinemachineCamera playerCamera; // Основная камера игрока
    public CinemachineCamera keypadCamera; // Камера для keypad
    public float transitionTime = 1f;
    public CinemachineBlendDefinition.Styles blendStyle = CinemachineBlendDefinition.Styles.EaseInOut;

    private GameObject player;
    private bool isKeypadActive = false;
    private CinemachineBrain cinemachineBrain;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (keypadController == null)
        {
            keypadController = GetComponent<PcController>();
        }

        // Находим или создаем CinemachineBrain
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        if (cinemachineBrain == null)
        {
            cinemachineBrain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }

        // Настраиваем переходы через код
        if (cinemachineBrain != null)
        {
            // Создаем определение смешивания с указанным стилем и временем
            var defaultBlend = new CinemachineBlendDefinition(blendStyle, transitionTime);
            cinemachineBrain.DefaultBlend = defaultBlend;
        }

        // Инициализируем приоритеты камер
        SetCameraPriorities(false);
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool isNearKeypad = distance <= interactionDistance;

        if (isNearKeypad && Input.GetKeyDown(KeyCode.E) && !isKeypadActive)
        {
            ActivateKeypad();
        }

        if (isKeypadActive && Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateKeypad();
        }

        if (isKeypadActive && Input.GetMouseButtonDown(0))
        {
            HandleButtonInteraction();
        }
    }

    private void HandleButtonInteraction()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            KeyboardKey keyButton = hit.collider.GetComponent<KeyboardKey>();
            if (keyButton != null)
            {
                keyButton.PressButton();
            }
        }
    }

    private void SetCameraPriorities(bool keypadActive)
    {
        if (playerCamera != null)
        {
            playerCamera.Priority = keypadActive ? 0 : 10;
        }

        if (keypadCamera != null)
        {
            keypadCamera.Priority = keypadActive ? 10 : 0;
        }
    }

    private void ActivateKeypad()
    {
        isKeypadActive = true;
        SetCameraPriorities(true);

        if (keypadController != null)
        {
            keypadController.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DisablePlayerControl(true);
    }

    private void DeactivateKeypad()
    {
        isKeypadActive = false;
        SetCameraPriorities(false);

        if (keypadController != null)
        {
            keypadController.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        DisablePlayerControl(false);
    }

    private void DisablePlayerControl(bool disable)
    {
        // Добавьте отключение управления игроком здесь
        // Например: player.GetComponent<PlayerMovement>().enabled = !disable;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
