using NaughtyAttributes;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NPC_DialogData dialogData;

    [SerializeField]
    private Dialog dialog;

  

    [Button("顯示第一段話")]
    public void PlayFirstDialog()
    {
        dialog.SetDialogTexts(dialogData.dialogTexts);
        dialog.PlayFirstDialog();
    }

    [Button("顯示下一段話")]
    public void PlayNextDialog()
    {
        dialog.PlayNextDialog();
    }

    [Button("Skip對話")]
    public void SkipDialog()
    {
        dialog.SkipWriter();
    }
}