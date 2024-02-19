using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayUI : MonoBehaviour
{
    private static CardDisplayUI instance;
    public static CardDisplayUI Instance {  get { return instance; } }
    
    [SerializeField] InputHandler inputHandler;
    [SerializeField] Transform offScreenPointForCards;

    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject closeButton;

    Vector2 startingPoint = new Vector2(-5, 3.5f);
    Vector2 currentPoint;

    Transform collectionParent;
    List<CriminalCard> cardsDisplayed;

    TargetRule currentTargetRule;

    List<CriminalCard> targetedCards = new List<CriminalCard>();

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Error: Multiple CardDisplayUI");
            Destroy(gameObject);
        }
    }

    public void DisplayCards(Transform collectionParent, List<CriminalCard> cardsDisplayed, TargetRule targetRule)
    {
        currentPoint = startingPoint;
        this.collectionParent = collectionParent;
        this.cardsDisplayed = cardsDisplayed;
        foreach (CriminalCard card in cardsDisplayed)
        {
            card.transform.SetParent(transform, false);
            card.SetToPickedUpLayer();
            card.transform.position = currentPoint;
            if(currentPoint.x < 5)
            {
                currentPoint = new Vector2(currentPoint.x + 2.5f, currentPoint.y);
            }
            else
            {
                currentPoint = new Vector2(startingPoint.x, currentPoint.y - 3.5f);
            }
        }

        if(targetRule != null)
        {
            currentTargetRule = targetRule;
        }

        else
        {
            closeButton.SetActive(true);
        }
    }

    public void OnCardSelected(CriminalCard selectedCard)
    {
        if(currentTargetRule != null)
        {
            if(targetedCards.Contains(selectedCard))
            {
                selectedCard.SetCardHighlight(false);
                targetedCards.Remove(selectedCard);
            }
            
            else if(targetedCards.Count < currentTargetRule.NumberOfTargets)
            {
                selectedCard.SetCardHighlight(true);
                targetedCards.Add(selectedCard);
            }

            confirmButton.SetActive(targetedCards.Count == currentTargetRule.NumberOfTargets);
        }
    }

    public void OnConfirmButtonPushed()
    {
        
    }

    public void OnCloseButtonPushed()
    {
        CloseMenu();
    }

    public void CloseMenu()
    {
        foreach (CriminalCard card in cardsDisplayed)
        {
            card.transform.SetParent(collectionParent, false);
            card.SetToBaseCardLayer();
            card.transform.position = offScreenPointForCards.position;
            card.SetCardHighlight(false);
        }
        confirmButton?.SetActive(false);
        closeButton?.SetActive(false);
        cardsDisplayed = null;
        collectionParent = null;
        currentTargetRule = null;
        targetedCards.Clear();
        inputHandler.ReturnToNormalControl();
        gameObject.SetActive(false);
    }
}
