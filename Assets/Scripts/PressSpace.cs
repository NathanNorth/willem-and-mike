using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PressSpace : MonoBehaviour
{
    public string destination;
    private void  LoadGame() => SceneManager.LoadScene(destination);

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadGame();
        }
    }
}
