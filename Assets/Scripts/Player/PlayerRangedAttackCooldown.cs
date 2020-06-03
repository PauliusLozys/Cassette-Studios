using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRangedAttackCooldown : MonoBehaviour
{
    public Image imageCooldown;
    private float cooldownTime;
    private PlayerCombatController playerCombatController;
    private bool cooldownFinished;

    // Start is called before the first frame update
    void Start()
    {
        playerCombatController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        cooldownTime = playerCombatController.GetRangedAttackCooldown();
        cooldownFinished = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (cooldownFinished && Input.GetButtonDown("Fire1") && cooldownFinished)
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
