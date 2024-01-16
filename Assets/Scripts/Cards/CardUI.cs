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
    [SerializeField] TextMeshProUGUI influenceTextField;

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
                criminalTagsField.text += tag.ToString();
            }
        }
        strengthTextField.text = criminalType.Strength.ToString();
    }

}
