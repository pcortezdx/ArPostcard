using System.Collections;
using UnityEngine;
// Libraries required for speech recognition
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor;

public class VoiceInputController : MonoBehaviour
{
    //maximum rotation
    public float yRotation = 90f;
    public float lerpDuration = 0.5f;

    //Rotation direction
    int direction = 1;
    bool isRotating;

    //array of string commands to listen for
    private KeywordRecognizer voiceKeywordRecognizer;

    //recognizer and keyword to listen ->action dictionary
    private Dictionary<string, System.Action> animationAction = new Dictionary<string, System.Action>();

    //Animator Variable
    private Animator giftBoxAnimator;

    //Particle System
    public GameObject confetti;

    void Start()
    {
        //Deactive Confetti Particle System
        confetti.SetActive(false);

        //Get Animator Component
        giftBoxAnimator = GetComponent<Animator>();

        // Adding to the command dictionary the pair (keyword to listen, function) 
        animationAction.Add("open", OpenGift);
        animationAction.Add("turn left", RotateLeft);
        animationAction.Add("turn right", RotateRight);

        //register for the phrase recognition event       
        voiceKeywordRecognizer = new KeywordRecognizer(animationAction.Keys.ToArray());

        voiceKeywordRecognizer.OnPhraseRecognized += voiceRecognized;

        voiceKeywordRecognizer.Start();
    }

    private void voiceRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text);

        // if the keyword recognized is in our dictionary, call the Action.
        animationAction[args.text].Invoke();
    }

    void RotateLeft()
    {
        // Verify that the object is not rotating
        if (!isRotating)
        {
            direction = 1;
            StartCoroutine(Rotate90(direction));
        }
    }

    void RotateRight()
    {
        // Verify that the object is not rotating
        if (!isRotating)
        {
            direction = -1;
            StartCoroutine(Rotate90(direction));
        }
    }

    //Open gift box animation
    private void OpenGift()
    {
        confetti.SetActive(false);

        if (giftBoxAnimator != null)
        {
            //Trigger Animation
            giftBoxAnimator.SetTrigger("OpenGift");

            //Activate Confetti Particle System
            /* TODO: improve function to get the rotation angle to display
             different animations */
            if (Math.Round((decimal)transform.localEulerAngles.y, 0) == 90)
            {
                confetti.SetActive(true);
            }
            
        }
    }

    //Coroutine to iterate the rotation 
    IEnumerator Rotate90(int direction)
    {
        isRotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, yRotation * direction, 0);
        while (timeElapsed < lerpDuration)
        {
            // Using Lerp to smoothly move the object from the startRotation to Target rotation
            transform.rotation = Quaternion.Lerp(
                startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            //wait until the next frame
            yield return null;
        }
        transform.rotation = targetRotation;
        isRotating = false;
    }
}

