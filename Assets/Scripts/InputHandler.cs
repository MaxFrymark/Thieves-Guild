using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] UIHandler uiHandler;
    [SerializeField] CardDisplayUI cardDisplayUI;
    [SerializeField] Market market;
    
    HumanPlayer player;
    
    PlayerInputActions inputActions;

    CriminalCard heldCard;

    private enum InputMode { Normal, Bidding, CardDisplay }
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
        RaycastHit2D hit = CheckMousePositionForCards();
        if (hit)
        {
            if (currentInputMode == InputMode.Normal)
            {
                CriminalCard cardAtClickPosition = GetCardFromClick(hit);
                if (cardAtClickPosition != null)
                {
                    heldCard = cardAtClickPosition;
                    if (heldCard.GetCardStateLocation() == CardState.Location.Den && heldCard.Owner is HumanPlayer)
                    {
                        heldCard.PickUpCard();
                    }

                    else if (heldCard.GetCardStateLocation() == CardState.Location.Market)
                    {
                        uiHandler.SwitchToBiddingUI();
                        market.OpenBidDisplay(heldCard, player);
                        currentInputMode = InputMode.Bidding;
                        heldCard = null;
                    }

                    else
                    {
                        heldCard = null;
                    }
                }

                else if (hit.collider.gameObject.tag == "Graveyard")
                {
                    currentInputMode = InputMode.CardDisplay;
                    uiHandler.SwitchToCardDisplayUI();
                    Graveyard graveyard = hit.collider.gameObject.GetComponent<Graveyard>();
                    cardDisplayUI.gameObject.SetActive(true);
                    cardDisplayUI.DisplayCards(graveyard.transform, graveyard.CriminalsInGraveyard, null);
                }
            }

            else if (currentInputMode == InputMode.CardDisplay)
            {

            }
        }
    }

    private CriminalCard GetCardFromClick(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Card")
            {
                return hit.collider.GetComponent<CriminalCard>();
            }
        }
        return null;
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
        uiHandler.SwitchToMainUI(); ;
    }
}
