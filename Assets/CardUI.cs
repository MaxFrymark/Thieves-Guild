using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI criminalNameTextField;
    [SerializeField] TextMeshProUGUI criminalTypeTextField;
    [SerializeField] TextMeshProUGUI criminalTagsField;
    [SerializeField] TextMeshProUGUI criminalDescriptionTextField;
    [SerializeField] TextMeshProUGUI strengthTextField;

    public void SetUpCardUI(CriminalType criminalType)
    {
        criminalNameTextField.text = criminalType.CriminalName;
        switch (criminalType)
        {
            case GuildMember:
                criminalTypeTextField.text = "Member";
                break;
            case Agent:
                criminalTypeTextField.text = "Agent";
                break;
        }
        criminalDescriptionTextField.text = criminalType.CriminalDescription;
        criminalTagsField.text = "";
        if (criminalType.Tags.Count > 0)
        {
            foreach (var tag in criminalType.Tags)
            {
                criminalTagsField.text += tag.ToString();
            }
        }
        strengthTextField.text = criminalType.Strength.ToString();
    }

}
