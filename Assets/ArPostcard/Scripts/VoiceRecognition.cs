using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    //Animator Variable
    private Animator giftBoxAnimator;

    //
    private KeywordRecognizer voiceKeywordRecognizer;

    //
    private Dictionary<string, System.Action> animationAction = new Dictionary<string, System.Action>();

    // Start is called before the first frame update
    void Start()
    {
     //Get Animator Component
        giftBoxAnimator = GetComponent<Animator>();

        //
        animationAction.Add("open", OpenGift);

        //
        voiceKeywordRecognizer = new KeywordRecognizer(animationAction.Keys.ToArray());

        //
        voiceKeywordRecognizer.OnPhraseRecognized += voiceRecognized;   

        voiceKeywordRecognizer.Start();
    }

    private void voiceRecognized(PhraseRecognizedEventArgs args) 
    {
            //
            Debug.Log(args.text);

            //
            animationAction[args.text].Invoke();
    }

    private void OpenGift() 
    {
         //
        if(giftBoxAnimator != null) {
            //
            giftBoxAnimator.SetTrigger("OpenGift");
        }
    }
}
