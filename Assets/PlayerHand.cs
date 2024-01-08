using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] Transform[] cardSlots;
    CardControl[] hand = new CardControl[5];

    public bool AddCardToHand(CardControl card)
    {
        for(int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == null)
            {
                hand[i] = card;
                card.transform.position = cardSlots[i].transform.position;
                card.SetOriginPoint(cardSlots[i]);
                return true;
            }
        }

        return false;
    }

    public void RemoveCardFromHand(CardControl card)
    {
        for(int i = 0;i < hand.Length;i++)
        {
            if (hand[i] == card)
            {
                hand[i] = null;
                card.SetOriginPoint(null);
                return;
            }
        }

        Debug.Log("Invalid Card Attemped to be removed from hand");
    }
}
