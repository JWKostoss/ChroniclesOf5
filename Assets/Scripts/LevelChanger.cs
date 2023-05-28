using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    // these create customizable variables in the inspector tab.
    public Animator animator;
    public int levelToLoad;
    private bool canExit;

    // if the boolean canExit is true, then you may press E to leave to the specified scene
    // we can change the interact key to be anything, but I like E so thats what I used
    private void Update()
    {
        if(canExit && Input.GetKeyDown(KeyCode.E))
        {
            FadeToLevel(levelToLoad);
        }
    }

    // Once the player enters the designated box, canExit is set to true, meaning you can change
    // scenes while inside the collider.
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            canExit = true;
        }
    }

    // once the player leaves the designated collider, canExit is set to false, meaning you cannot
    // exit the scene if you are not on the ladder.
    private void OnTriggerExit2D(Collider2D obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            canExit = false;
        }
    }

    // this function gets the current scene (you can find it manually by clicking on File -> Build Settings)
    // and then adds 1 to it, to get the next scene. Currently unused, but it's here for reference.
    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // accesses the FadeOut animation and plays it.
    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    // this function is automatically called by the animator. if you look at the animation tab,
    // FadeOut should have an event at the end which calls this function. It switches the scenes
    // once the black image has full alpha
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
