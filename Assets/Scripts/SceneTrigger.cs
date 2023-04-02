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
        Player.spawnLoc = spawnLoc;
        Player.spawnDir = spawnDir;
        SceneManager.LoadScene(destination);
    }
}
