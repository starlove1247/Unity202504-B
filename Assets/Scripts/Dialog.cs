using NaughtyAttributes;
using TMPEffects.Components;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private TMPWriter tmpWriter;

    [Button("播放打字機效果")]
    public void PlayWriter()
    {
        tmpWriter.RestartWriter();
    }

    [Button("跳過目前的打字機效果")]
    public void SkipWriter()
    {
        tmpWriter.SkipWriter();
    }

    public void SetText(string dialogText)
    {
       tmpWriter.SetText(dialogText); 
    }
}
