using NaughtyAttributes;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NPC_DialogData dialogData;

    [SerializeField]
    private Dialog dialog;

    [Button("顯示對話")]
    public void PlayDialog()
    {
        var dialogText = dialogData.dialogTexts[0];
        dialog.SetText(dialogText);
        dialog.PlayWriter();
    }

    [Button("Skip對話")]
    public void SkipDialog()
    {
        dialog.SkipWriter();
    }
}