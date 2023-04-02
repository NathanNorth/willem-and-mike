using System.Collections;
using TMPro;
using UnityEngine;

public class ProgressLockedSceneTrigger: SceneTrigger
{
    public string errorMessage;
    public Progress.GameStage lockUntilStageIs;

    public float fadeTime = 5f;

    public CanvasGroup group;
    public TextMeshProUGUI text;

    private Coroutine current = null;

    private void Start()
    {
        text.text = errorMessage;
        group.alpha = 0f;
    }

    public override void TriggerSceneChange()
    {
        if(Progress.stage ==  lockUntilStageIs) base.TriggerSceneChange();
        else
        {
            if(current != null) StopCoroutine(current);
            current = StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade()
    {
        for (float t = fadeTime; t > 0; t -= Time.deltaTime)
        {
            float percentage = Mathf.Min(t, 1f);
            group.alpha = percentage;
            yield return null;
        }
    }
}