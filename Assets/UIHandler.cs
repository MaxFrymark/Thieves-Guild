using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject marketUI;
    [SerializeField] GameObject cardDisplayUI;
    [SerializeField] GameObject biddingUI;

    public void SwitchToMainUI()
    {
        mainUI.SetActive(true);
        marketUI.SetActive(true);
        cardDisplayUI.SetActive(false);
        biddingUI.SetActive(false);
    }

    public void SwitchToBiddingUI()
    {
        biddingUI.SetActive(true);
        marketUI.SetActive(true);
        cardDisplayUI.SetActive(false);
        mainUI.SetActive(false);
    }

    public void SwitchToCardDisplayUI()
    {
        cardDisplayUI.SetActive(true);
        biddingUI.SetActive(false);
        mainUI.SetActive(false);
        marketUI.SetActive(false);
    }
}
