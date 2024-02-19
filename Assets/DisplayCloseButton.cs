using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCloseButton : MonoBehaviour
{
    [SerializeField] CardDisplayUI cardDisplayUI;

    private void OnMouseDown()
    {
        cardDisplayUI.OnCloseButtonPushed();
    }
}
