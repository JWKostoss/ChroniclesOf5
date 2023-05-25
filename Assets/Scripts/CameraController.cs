using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0,1)]
    public float smoothTime;

    public Vector3 positionOffset;

    // it says x y in the same row, but trust the column. so put x min max in the first row, y min max in the second
    // this just creates an extra function in the inspector tab for the script
    [Header("Axis Limitation | (min max)")]
    public Vector2 xLimit;
    public Vector2 yLimit;

    private void Start()
    {
        // once again we find player one time when the game is loaded, then use the variable "target" to latch onto
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // honestly, im not so sure what this does. I was "inspired" by youtube. best guess? SmoothDamp allows the camera to lag a little bit
        // in a range of 0 to 1 as previously declared. Mathf.Clamp allows the camera to move? maybe? I have no clue what the -10 is for...
        // targetPosition most likely allows the camera to move from any previous position, so it always follows where our player is
        Vector3 targetPosition = target.position + positionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
