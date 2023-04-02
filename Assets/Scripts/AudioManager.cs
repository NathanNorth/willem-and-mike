using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource myAudio;
    public float fadeInDuration = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFade(myAudio, fadeInDuration));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static IEnumerator StartFade(AudioSource myAudio, float fadeInDuration)
    {
        float currentTime = 0;
        const float start = 0;
        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            myAudio.volume = Mathf.Lerp(start, 1, currentTime / fadeInDuration);
            yield return null;
        }
        yield break;
    }
}
