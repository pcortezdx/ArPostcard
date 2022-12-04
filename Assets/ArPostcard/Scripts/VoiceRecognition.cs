using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    //Animator Variable
    private Animator giftBoxAnimator;

    //array of string commands to listen for
    private KeywordRecognizer voiceKeywordRecognizer;

    //recognizer and keyword->action dictionary
    private Dictionary<string, System.Action> animationAction = new Dictionary<string, System.Action>();

    // Start is called before the first frame update
    void Start()
    {
     //Get Animator Component
        giftBoxAnimator = GetComponent<Animator>();

        // action to be performed when this keyword "open" is spoken
        animationAction.Add("open", OpenGift);

        //register for the phrase recognition event       
        voiceKeywordRecognizer = new KeywordRecognizer(animationAction.Keys.ToArray());

        voiceKeywordRecognizer.OnPhraseRecognized += voiceRecognized;   

        voiceKeywordRecognizer.Start();
    }

    private void voiceRecognized(PhraseRecognizedEventArgs args) 
    {
            //Debug for 
            Debug.Log(args.text);

            // if the keyword recognized is in our dictionary, call that Action.
            animationAction[args.text].Invoke();
    }

    //Open gift box animation
    private void OpenGift() 
    {
        if(giftBoxAnimator != null) {
            //Trigger Animation
            giftBoxAnimator.SetTrigger("OpenGift");
        }
    }
}
