using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string destination;

    public void TriggerSceneChange()
    {
        SceneManager.LoadScene(destination);
    }
}
