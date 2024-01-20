using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] SpriteRenderer cardBackground;
    [SerializeField] SpriteRenderer cardImage;
    
    [SerializeField] TextMeshProUGUI criminalNameTextField;
    [SerializeField] TextMeshProUGUI criminalTypeTextField;
    [SerializeField] TextMeshProUGUI criminalTagsField;
    [SerializeField] TextMeshProUGUI criminalDescriptionTextField;
    [SerializeField] TextMeshProUGUI strengthTextField;
    [SerializeField] TextMeshProUGUI influenceTextField;

    private string cardLayer = "Cards";
    private string pickedUpLayer = "Picked Up Card";

    public void SetUpCardUI(CriminalType criminalType)
    {
        criminalNameTextField.text = criminalType.CriminalName;
        switch (criminalType)
        {
            case GuildMember:
                criminalTypeTextField.text = "Member";
                influenceTextField.gameObject.SetActive(true);
                influenceTextField.text = criminalType.Influence.ToString();
                break;
            case Agent:
                criminalTypeTextField.text = "Agent";
                influenceTextField.gameObject.SetActive(false);
                break;
        }
        criminalDescriptionTextField.text = criminalType.CriminalDescription;
        criminalTagsField.text = "";
        if (criminalType.Tags.Count > 0)
        {
            foreach (var tag in criminalType.Tags)
            {
                criminalTagsField.text += tag.ToString() + " ";
            }
        }
        strengthTextField.text = criminalType.Strength.ToString();
    }

    public void SetCardImage(Sprite sprite)
    {
        cardImage.sprite = sprite;
    }

    public void SetToPickedUpLayer()
    {
        canvas.sortingLayerName = pickedUpLayer;
        cardBackground.sortingLayerName= pickedUpLayer;
        cardImage.sortingLayerName= pickedUpLayer;
    }

    public void SetToBaseCardLayer()
    {
        canvas.sortingLayerName = cardLayer;
        cardBackground.sortingLayerName = cardLayer;
        cardImage.sortingLayerName = cardLayer;
    }

}
