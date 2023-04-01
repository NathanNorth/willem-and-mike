using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string destination;
    public Vector2 spawnLoc;

    public void TriggerSceneChange()
    {
        Player.spawnLoc = spawnLoc;
        SceneManager.LoadScene(destination);
    }
}
