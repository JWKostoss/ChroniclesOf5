using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageFade : MonoBehaviour
{
    private SpriteRenderer sr;
    private float duration = 0.1f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // the door has a box collider that is set to "is trigger". once the game recognizes
    // that the player has entered the box, it will make the text message appear
    // note: this only works since the player's tag has been set to player. (top of inspector)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Appear());
        }
    }

    // once the player exits, the message disappears.
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Dissolve());
        }
    }

    // IEnumerator is an interpolation function (i think). Mathf.Lerp takes the first value and translates it
    // to the second value over "time/duration" time (i think). So the alpha of the gameObject is changed
    // to nothing. If you want it to be faster/slower, simply change the duration variable at the top.
    // yield return null tells it to stop the while loop so unity doesn't get overloaded and crash.
    // note: the while loop used to be (time < duration) but for some horrible reason the object wouldn't
    // fade completely out, so duration * 2 seemed to have fix this. I didn't actually expect it to work,
    // but it did. My best guess is because it gives the function more time to execute, thus allowing
    // the object to completely fade out.
    private IEnumerator Dissolve()
    {
        float time = 0.0f;

        while(time < duration * 2)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, time / duration);
            Color newColor = sr.color;
            newColor.a = alpha;
            sr.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }
    }

    // same thing here, just the opposite. going from 0 alpha to 255 (actual values), or 0 to 1 in the code
    private IEnumerator Appear()
    {
        float time = 0.0f;

        while(time < duration * 2)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, time / duration);
            Color newColor = sr.color;
            newColor.a = alpha;
            sr.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }
    }
}
