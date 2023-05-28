// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class PauseMenu : MonoBehaviour
// {
//     [SerializeField] GameObject pauseMenu;
//     private float duration = 0.1f;

//     private void Update()
//     {
//         if(Input.GetKeyDown(KeyCode.P) && pauseMenu.activeSelf == false)
//         {
//             Time.timeScale = 0f;
//             StartCoroutine(Pause());
//             pauseMenu.SetActive(true);
//         }

//         if(Input.GetKeyDown(KeyCode.P) && pauseMenu.activeSelf == true)
//         {
//             Time.timeScale = 1f;
//             StartCoroutine(Resume());
//             pauseMenu.SetActive(false);
//         }
//     }

//     public void Home(int sceneID)
//     {
//         // timescale is set to realtime, and the scene is changed to whichever ID was presented.
//         Time.timeScale = 1f;
//         SceneManager.LoadScene(sceneID);
//     }

//     // same functions used in MessageFade. Same concepts, same code.
//     private IEnumerator Resume()
//     {
//         float time = 0.0f;

//         while(time < duration * 2)
//         {
//             float alpha = Mathf.Lerp(1.0f, 0.0f, time / duration);
//             Color newColor = pauseMenu.color;
//             newColor.a = alpha;
//             pauseMenu.color = newColor;

//             time += Time.deltaTime;
//             yield return null;
//         }
//     }

//     private IEnumerator Pause()
//     {
//         float time = 0.0f;

//         while(time < duration * 2)
//         {
//             float alpha = Mathf.Lerp(0.0f, 1.0f, time / duration);
//             Color newColor = pauseMenu.color;
//             newColor.a = alpha;
//             pauseMenu.color = newColor;

//             time += Time.deltaTime;
//             yield return null;
//         }
//     }
// }
