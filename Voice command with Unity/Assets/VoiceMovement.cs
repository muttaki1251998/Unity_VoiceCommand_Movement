using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceMovement : MonoBehaviour {

    public string[] keywords = { "up", "down", "right", "left" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1.0f;

    protected string word;
    private KeywordRecognizer recog;

    // Use this for initialization
    void Start () {
		
        if(keywords != null)
        {
            recog = new KeywordRecognizer(keywords, confidence);
            recog.OnPhraseRecognized += _OnPhraseRecognized;
            recog.Start();
        }

	}

    private void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        switch (word)
        {
            case "up":
                y += speed;
                break;
            case "down":
                y -= speed;
                break;
            case "left":
                x -= speed;
                break;
            case "right":
                x += speed;
                break;
        }

        transform.position = new Vector3(x, y, 0);
    }

    private void OnApplicationQuit()
    {
        if(recog == null && recog.IsRunning)
        {
            recog.OnPhraseRecognized += _OnPhraseRecognized;
            recog.Stop();
        }
    }

    private void _OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;        
    }



}
