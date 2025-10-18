using UnityEngine;
using System.Collections.Generic;


public class KeyPad : MonoBehaviour
{
    [Header("Настройки дверей и кнопок")]
    [Tooltip("Список дверей, на которых будет действовать эта панель")]
    public List<DoorController> doors;
    [Tooltip("Список кнопок, необходимых для активации")]
    public List<KeypadKey> requiredKeys;

    [Tooltip("Если true – кнопки открывают дверь, если false – закрывают")]
    public bool openDoorAction = true;

    private HashSet<KeypadKey> pressedKeys = new HashSet<KeypadKey>();

    public void RegisterKeyPress(KeypadKey key)
    {
        // Если эта кнопка указана в списке requiredKeys и ещё не нажата
        if (requiredKeys.Contains(key) && !pressedKeys.Contains(key))
        {
            pressedKeys.Add(key);
            Debug.Log("[KeyPad] Зарегистрирована кнопка: " + key.gameObject.name);
            CheckCombination();
        }
    }

    private void CheckCombination()
    {
        if (pressedKeys.Count == requiredKeys.Count)
        {
            Debug.Log("[KeyPad] Все требуемые кнопки нажаты. Выполнение действия: " + (openDoorAction ? "Открыть" : "Закрыть") + " дверь(и)");
            foreach (var door in doors)
            {
                // Управляем переменной открытия
                door.opening = openDoorAction;
            }
            ResetKeys();
        }
    }

    public void ResetKeys()
    {
        pressedKeys.Clear();
        foreach (var key in requiredKeys)
        {
            key.ResetKey();
        }
    }
}