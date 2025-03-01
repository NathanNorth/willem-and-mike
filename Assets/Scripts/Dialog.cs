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

    protected bool inDialog = false;
    protected int i = 0;
    protected Player player;

    // Start is called before the first frame update
    private void Awake()
    {
        canvas.gameObject.SetActive(false);
        // TriggerDialog();
    }

    // Update is called once per frame
    protected virtual void LateUpdate()
    {
        // Debug.Log(i + " , " + text.Length);
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

    public virtual void TriggerDialog(Player player)
    {
        inDialog = true;
        canvas.gameObject.SetActive(true);
        StartCoroutine(TextReveal(text[i], speakingSpeed));
        i++;

        this.player = player;
        this.player.dialogLock = true;
    }

    protected const float textScrollSpeed = .05f; //lower is faster
    protected bool revealLock = false;
    protected IEnumerator TextReveal(string text, float time)
    {
        revealLock = true;
        time = time * textScrollSpeed * text.Length;
        
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                i += Time.deltaTime * 2;
            }
            output.text = text.Substring(0, (int)((Mathf.Min(1, i / time)) * text.Length));
            
            yield return null;
        }
        output.text = text;

        revealLock = false;
    }
}
