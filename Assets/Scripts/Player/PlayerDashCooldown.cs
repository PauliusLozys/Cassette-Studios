using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashCooldown : MonoBehaviour
{
    public Image imageCooldown;
    private float cooldownTime;
    private PlayerController playerController;
    private bool hasDashed;
    private bool cooldownFinished;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cooldownTime = playerController.dashCoolDown;
        cooldownFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        hasDashed = playerController.GetDashStatus();

        if (cooldownFinished && hasDashed)
        {
            cooldownFinished = false;
        }

        if (!cooldownFinished)
        {
            imageCooldown.fillAmount += 1 / cooldownTime * Time.deltaTime;

            if (imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0;
                cooldownFinished = true;
            }
        }
    }
}
