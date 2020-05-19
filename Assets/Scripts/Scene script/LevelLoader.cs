using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public BoxCollider2D trigger;
    public float transitionTime = 1f;
    public bool CanEnterTheDungeon = false;
    private Dialogue dialogue;

    private void Start()
    {
        dialogue = GetComponent<Dialogue>();
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");
        //Wait for the animation to finish
        yield return new WaitForSeconds(transitionTime);
        //Load scene
        SceneManager.LoadScene(levelIndex);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Player" && CanEnterTheDungeon)
        {
            LevelManager.LoadLevelData();
            LoadNextLevel();
        }
        else if (collision.tag == "Player" && !CanEnterTheDungeon)
        {
            dialogue.ShowDialogue();
        }
    }
}
