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
            if(current == null) current = StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade()
    {
        for (float t = fadeTime; t > 0; t -= Time.deltaTime)
        {
            if (t > fadeTime - 1)
            {
                group.alpha = (1 - (t - fadeTime + 1)) * 2;
            }
            else
            {
                float percentage = Mathf.Min(t, 1f);
                group.alpha = percentage;
            }
            yield return null;
        }

        current = null;
    }
}