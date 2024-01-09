using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] HumanPlayer player;
    
    PlayerInputActions inputActions;

    CriminalCard heldCard;
    
    void Start()
    {
        SetUpInput();
    }

    private void SetUpInput()
    {
        inputActions = new PlayerInputActions();
        
        inputActions.Enable();
        
        inputActions.Mouse.Click.performed += OnClick;
        inputActions.Mouse.Click.canceled += OnMouseRelease;
    }
    
    private void OnClick(InputAction.CallbackContext context)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.up, 0.1f);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "Card")
            {
                heldCard = hit.collider.GetComponent<CriminalCard>();
                if (heldCard.CurrentLocation == CriminalCard.Location.Den && heldCard.Owner is HumanPlayer)
                {
                    heldCard.PickUpCard();
                }
            }
        }
        
    }

    private void OnMouseRelease(InputAction.CallbackContext context)
    {
        if(heldCard != null)
        {
            heldCard.ReleaseCard();
            heldCard = null;
        }
    }
}
