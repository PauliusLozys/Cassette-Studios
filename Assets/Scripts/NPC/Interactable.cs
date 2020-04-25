using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Transform instance;
    public void showPopup()
    {
        Vector3 position = transform.position + new Vector3(0,2,0);
        instance = Instantiate((Resources.Load("Popup") as GameObject).GetComponent<Transform>(), position, Quaternion.identity);
    }
    public void hidePopup()
    {
        Destroy(instance.gameObject);
    }
}
