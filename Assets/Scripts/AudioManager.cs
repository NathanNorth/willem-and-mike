using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager currentManager;

    public AudioSource myAudio;
    public float fadeDuration = 1;

    public CanvasGroup myCanvas;
    // private  myImageColor = myImage.color;
    
    // Start is called before the first frame update
    void Start()
    {
        currentManager = this;
        StartCoroutine(StartFadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartFadeIn()
    {
        float currentTime = 0;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            myAudio.volume = Mathf.Lerp(0, 1, currentTime / fadeDuration);
            myCanvas.alpha = Mathf.Lerp(1, 0, currentTime / fadeDuration);
            yield return null;
        }
        yield break;
    }
    
    public IEnumerator StartFadeOut()
    {
        float currentTime = 0;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            myAudio.volume = Mathf.Lerp(1, 0, currentTime / fadeDuration);
            myCanvas.alpha = Mathf.Lerp(0, 1, currentTime / fadeDuration);
            yield return null;
        }
        yield break;
    }
}
