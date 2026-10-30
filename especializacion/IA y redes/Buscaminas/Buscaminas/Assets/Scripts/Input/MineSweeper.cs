using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MineSweeper: MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public void OnInteract(InputAction.CallbackContext context) {
        if(!context.performed)
            return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray =mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) 
        { 
        Cell cell=hit.collider.GetComponent<Cell>();
            if (cell !=null)
            {
                cell.Interact();
            }
        }
    }
    public void rightClick(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell != null)
            {
                cell.Flag();
            }
        }
    }
}
