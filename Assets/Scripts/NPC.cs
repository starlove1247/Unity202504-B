using NaughtyAttributes;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NPC_DialogData dialogData;

    [SerializeField]
    private Dialog dialog;

    /// <summary>
    /// 目前的對話資料Index，從0開始
    /// </summary>
    private int dialogIndex=0;

    [Button("顯示第一段話")]
    public void PlayFirstDialog()
    {
        dialogIndex = 0; // 重置Index
        var dialogText = dialogData.dialogTexts[dialogIndex];
        dialog.SetText(dialogText);
        dialog.PlayWriter();
    }

    [Button("顯示下一段話")]
    public void PlayNextDialog()
    {
        dialogIndex++;
        var dialogText = dialogData.dialogTexts[dialogIndex];
        dialog.SetText(dialogText);
        dialog.PlayWriter();
    }

    [Button("Skip對話")]
    public void SkipDialog()
    {
        dialog.SkipWriter();
    }

}