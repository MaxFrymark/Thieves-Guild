using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayForCardInNeighborhood : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDisplay;
    private CriminalCard attachedCard;
    public CriminalCard AttachedCard { get { return attachedCard; } }

    public void SetUpDisplay(CriminalCard card)
    {
        transform.localScale = Vector3.one;
        attachedCard = card;
        textDisplay.text = card.CriminalType.CriminalName;
        textDisplay.color = card.Owner.PlayerColor;
    }    

    public void ReturnToObjectPool()
    {
        attachedCard = null;
        transform.SetParent(ObjectPool.Instance.transform, false);
        gameObject.SetActive(false);
    }
}
