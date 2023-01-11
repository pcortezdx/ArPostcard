using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    //Animator Variable
    private Animator giftBoxAnimator;

    public AudioSource AudioSource;
    public AudioClip song;

    public Material newMaterial;

    // Define your colors array
    public List<Color> Colors = new List<Color>();

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
        animationAction.Add("Happy Holidays", OpenGift);

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
            AudioSource.PlayOneShot(song);
            InvokeRepeating ("ChangeColor", 0f, 1f);
        }
    }

    private void ChangeColor () {
        newMaterial.color =  Colors[Random.Range(0, Colors.Count)];
    }
 
}
