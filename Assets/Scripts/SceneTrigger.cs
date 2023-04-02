using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string destination;
    public Vector2 spawnLoc;
    public Dir spawnDir;
    
    public virtual void TriggerSceneChange()
    {
        FindObjectOfType<Player>().dialogLock = true;
        StartCoroutine(AudioManager.currentManager.StartFadeOut());
        Invoke(nameof(ActuallyDoTheChange), AudioManager.currentManager.fadeDuration + 0.01f);
    }

    private void ActuallyDoTheChange()
    {
        Player.spawnLoc = spawnLoc;
        Player.spawnDir = spawnDir;
        SceneManager.LoadScene(destination);
    }
}
