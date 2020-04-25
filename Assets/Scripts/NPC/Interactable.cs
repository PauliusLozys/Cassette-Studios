using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform popup;
    private Transform instance;

    public void showPopup()
    {
        Vector3 position = transform.position + new Vector3(0,2,0);
        instance = Instantiate(popup, position, Quaternion.identity);
    }
    public void hidePopup()
    {
        Destroy(instance.gameObject);
    }
}
