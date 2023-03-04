using System.Collections;
using UnityEngine;

public class RotationKeyInputController : MonoBehaviour
{
    //maximum rotation
    public float yRotation = 90f;
    public float lerpDuration = 0.5f;
    int direction = 1;
    bool isRotating;

    void Update()
    {
        // Rotate left
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotating)
        {
            direction = 1;
            StartCoroutine(Rotate90(direction));
        }

        // Rotate right
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotating)
        {
            direction = -1;
            StartCoroutine(Rotate90(direction));
        }

    }

    //Coroutine to iterate the rotation 
    IEnumerator Rotate90(int direction)
    {
        isRotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, yRotation *direction, 0);
        while (timeElapsed < lerpDuration)
        {
            // Using Slerp to smoothly move the object from the startRotation to Target rotation
            transform.rotation = Quaternion.Slerp(
                startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            //wait until the next frame
            yield return null;
        }
        transform.rotation = targetRotation;
        isRotating = false;
    }
}


