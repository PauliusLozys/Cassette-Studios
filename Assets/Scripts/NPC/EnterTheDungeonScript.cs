using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheDungeonScript : MonoBehaviour
{
    public PlayerStats player;
    public LevelLoader LevelLoader;
    public bool hasGivenMoney = false;
    private Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && dialogue.isDialogueOpen && !hasGivenMoney)
        {
            LevelLoader.CanEnterTheDungeon = true;
            hasGivenMoney = true;
            if(player.GetPlayerMoney() > 0)
                dialogue.ChangeDialogueText("That is some good cash bro.\nYou can go to the dungeon now ;)");
            else
                dialogue.ChangeDialogueText("Wow, you are so broke I may just let you in for free");

            player.DecreaseMoney(player.GetPlayerMoney());
            Debug.Log("Player can now enter the dungeon");
        }
        else if(Input.GetKeyDown(KeyCode.E) && dialogue.isDialogueOpen && hasGivenMoney)
        {
            dialogue.ChangeDialogueText("You paid me already mate.\nYou can go to the dungeon now ;)");
        }
    }
}
