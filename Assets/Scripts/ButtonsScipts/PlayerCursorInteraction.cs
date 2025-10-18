using UnityEngine;

public class PlayerCursorInteraction : MonoBehaviour
{
    [Tooltip("Дальность рейкаста для взаимодействия")]
    public float playerReach = 3f;
    private Interactable currentInteractable;

    void Update()
    {
        CheckInteraction();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
            
        }
    }

    void CheckInteraction()
    {
        // Рейкаст из центра экрана
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, playerReach))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Interactable"))
            {
                Interactable interactable = hitObject.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (currentInteractable != interactable)
                    {
                        if (currentInteractable != null)
                        {
                          
                            currentInteractable.DisableOutline();
                        }
                        currentInteractable = interactable;
                        currentInteractable.EnableOutline();
                     
                    }
                    return;
                }
                
            }
            
        }
        

        // Если рейкаст не попал в нужный объект – снимаем выделение
        if (currentInteractable != null)
        {
          
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }

   
}
