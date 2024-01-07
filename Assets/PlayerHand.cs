using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] Transform[] cardSlots;
    GameObject[] hand = new GameObject[5];

    public bool AddCardToHand(GameObject card)
    {
        for(int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == null)
            {
                hand[i] = card;
                card.transform.position = cardSlots[i].transform.position;
                return true;
            }
        }

        return false;
    }

    public void RemoveCardFromHand(GameObject card)
    {
        for(int i = 0;i < hand.Length;i++)
        {
            if (hand[i] == card)
            {
                hand[i] = null;
                return;
            }
        }

        Debug.Log("Invalid Card Attemped to be removed from hand");
    }
}
