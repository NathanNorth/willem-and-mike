using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    
    public string destination;
    private void  LoadGame() => SceneManager.LoadScene(destination);
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(LoadGame), 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
