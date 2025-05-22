using System.Collections.Generic;
using System.Globalization;
using NaughtyAttributes;
using TMPEffects.Components;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private TMPWriter tmpWriter;

    /// <summary>
    /// 目前的對話資料Index，從0開始
    /// </summary>
    private int dialogIndex = 0;
    private List<string> dialogTexts;
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

    public void PlayFirstDialog()
    {
        // 沒有任何對話資料，不做任何事情
        if (dialogTexts.Count == 0)
        {
            Debug.LogError("錯誤，對話資料鎮列為空");
            return;
        }

        dialogIndex = 0; // 重置Index
        var dialogText = dialogTexts[dialogIndex];
        SetText(dialogText);
        PlayWriter(); 
    }

    public void SetDialogTexts(List<string> texts)
    {
        dialogTexts = texts;
    }

    public void PlayNextDialog()
    {
        // 沒有任何對話資料，不做任何事情
        if (dialogTexts.Count == 0)
        {
            Debug.LogError("錯誤，對話資料鎮列為空");
            return;
        }

        // 沒有下一段話，就不做任何事情
        if (dialogIndex + 1 == dialogTexts.Count) return;

        dialogIndex++;
        var dialogText = dialogTexts[dialogIndex];
        SetText(dialogText);
        PlayWriter(); 
    }
}
