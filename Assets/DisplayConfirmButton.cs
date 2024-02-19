using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayConfirmButton : MonoBehaviour
{
    [SerializeField] CardDisplayUI cardDisplayUI;
    
    private void OnMouseDown()
    {
        cardDisplayUI.OnConfirmButtonPushed();
    }
}
