using System.Collections;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [Header("CHANGE ME")]
    public string[] text;
    public float speakingSpeed;
    
    [Header("Object Links (DO NOT CHANGE)")]
    public Canvas canvas;
    public TextMeshProUGUI output;

    private bool inDialog = false;
    private int i = 0;
    private Player player;

    // Start is called before the first frame update
    private void Awake()
    {
        canvas.gameObject.SetActive(false);
        // TriggerDialog();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(i + " , " + text.Length);
        if (!inDialog) return;
        if (Input.GetKeyDown(KeyCode.Space) && !revealLock) //make sure we are not locked
        {
            if(i == text.Length) {
                this.player.dialogLock = false; //unlock player
                //kill everything else
                canvas.gameObject.SetActive(false);
                inDialog = false;
                i = 0; //rest i
                return;
            }

            StartCoroutine(TextReveal(text[i], speakingSpeed));
            i++;
        }
    }

    public void TriggerDialog(Player player)
    {
        inDialog = true;
        canvas.gameObject.SetActive(true);
        StartCoroutine(TextReveal(text[i], speakingSpeed));
        i++;

        this.player = player;
        this.player.dialogLock = true;
    }

    private bool revealLock = false;
    private IEnumerator TextReveal(string text, float time)
    {
        revealLock = true;
        
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            output.text = text.Substring(0, (int)((i / time) * text.Length));
            
            yield return null;
        }
        output.text = text;

        revealLock = false;
    }
}
