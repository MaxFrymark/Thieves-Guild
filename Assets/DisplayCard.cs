using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCard : MonoBehaviour
{
    [SerializeField] CardUI cardUI;

    public void SetUpCardDisplay(CriminalCard card)
    {
        cardUI.SetUpCardUI(card.CriminalType);
    }
}
