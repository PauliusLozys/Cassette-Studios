using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Transform instance;
    public string text;
    public void showPopup()
    {
        Vector3 position = transform.position + new Vector3(0,2,-0.1f);
        instance = Instantiate((Resources.Load("Popup") as GameObject).GetComponent<Transform>(), position, Quaternion.identity);
        if (!text.Equals(""))
        {
            instance.GetComponent<TextMeshPro>().SetText(text);
        }
    }
    public void hidePopup()
    {
        Destroy(instance.gameObject);
    }
}
