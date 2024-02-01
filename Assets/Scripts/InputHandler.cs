using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] Market market;
    
    HumanPlayer player;
    
    PlayerInputActions inputActions;

    CriminalCard heldCard;

    private enum InputMode { Normal, Bidding }
    private InputMode currentInputMode = InputMode.Normal;
    
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

    public void AssignHumanPlayer(HumanPlayer player)
    {
        this.player = player;
    }
    
    private void OnClick(InputAction.CallbackContext context)
    {
        if (currentInputMode == InputMode.Normal)
        {
            RaycastHit2D hit = CheckMousePositionForCards();
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Card")
                {
                    heldCard = hit.collider.GetComponent<CriminalCard>();
                    if (heldCard.GetCardStateLocation() == CardState.Location.Den && heldCard.Owner is HumanPlayer)
                    {
                        heldCard.PickUpCard();
                    }

                    else if (heldCard.GetCardStateLocation() == CardState.Location.Market)
                    {
                        market.OpenBidDisplay(heldCard, player);
                        currentInputMode = InputMode.Bidding;
                        heldCard = null;
                    }

                    else
                    {
                        heldCard = null;
                    }
                }
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private RaycastHit2D CheckMousePositionForCards()
    {
        return Physics2D.Raycast(GetMousePosition(), Vector2.up, 0.1f);
    }

    private void OnMouseRelease(InputAction.CallbackContext context)
    {
        if(heldCard != null)
        {
            heldCard.ReleaseCard();
            heldCard = null;
        }
    }

    public void ReturnToNormalControl()
    {
        currentInputMode = InputMode.Normal;
    }
}
