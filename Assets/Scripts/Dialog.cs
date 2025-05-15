using TMPEffects.Components;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private TMPWriter tmpWriter;

    [ContextMenu("播放打字機效果")]
    /// <summary>
    /// 播放打字機效果
    /// </summary>
    public void PlayWriter()
    {
        tmpWriter.RestartWriter();
    }

    [ContextMenu("跳過目前的打字機效果")]
    /// <summary>
    /// 跳過目前的打字機效果
    /// </summary>
    public void SkipWriter()
    {
        tmpWriter.SkipWriter();
    }


}
