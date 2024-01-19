using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborhoodBorder : MonoBehaviour
{
    [SerializeField] SpriteRenderer border;

    Color currentBorderColor = Color.white;

    public void ChangeBorderColor(Color color)
    {
        border.color = color;
    }

    public void ReturnToBaseColor()
    {
        border.color = currentBorderColor;
    }
}
