using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawningPlace : MonoBehaviour
{
    [SerializeField]
    public bool IsLeft = false;

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if (!LevelManager.LeftOnLeft && IsLeft)
        {
            Debug.Log("Player entered on Left");
            player.position = transform.position;
        }
        else if (LevelManager.LeftOnLeft && !IsLeft)
        {
            Debug.Log("Player entered on Right");
            player.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsLeft)
            {
                Debug.Log("Player left on left");
                LevelManager.LeftOnLeft = true;
                // Go to the left scene

                if(SceneManager.GetActiveScene().name != "Level 1")
                    SceneManager.LoadScene("Level 1");
            }
            else
            {
                Debug.Log("Player left on right");
                LevelManager.LeftOnLeft = false;
                // Go to the right scene
                if (SceneManager.GetActiveScene().name != "Level 2")
                    SceneManager.LoadScene("Level 2");
            }
        }


    }
}
