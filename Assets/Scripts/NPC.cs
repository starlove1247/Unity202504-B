using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController_1 = CharacterController;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NPC_DialogData dialogData;

    [SerializeField]
    private CanvasGroup npc_UIPanel;

    [SerializeField]
    private PlayerInput playerInput;

    private void Start()
    {
        npc_UIPanel.alpha = 0;
        Dialog.Instance.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        playerInput.actions.FindAction("Interact").performed += Onperformed;
    }

    private void OnDisable()
    {
        playerInput.actions.FindAction("Interact").performed -= Onperformed;
    }

    private void Onperformed(InputAction.CallbackContext obj)
    {
        Debug.Log($"NPC 互動鍵按下");
        // 如果已經在對話中，則不再進行對話
        if (Dialog.Instance.IsInDialog()) return;
        // 角色在範圍內，開始對話
        if (characterInTrigger)
        {
            // 互動建案下
            Dialog.Instance.gameObject.SetActive(true);
            PlayFirstDialog();
        }
    }

    /// <summary>
    /// 角色在偵測範圍內的狀態
    /// </summary>
    private bool characterInTrigger;

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 是角色
        // if (col.gameObject.GetComponent<CharacterController>() != null)
        if (col.gameObject.TryGetComponent(out CharacterController characterController))
        {
            characterInTrigger = true;
            ShowHintUI();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out CharacterController characterController))
        {
            characterInTrigger = false;
            HideHintUI();
        }
    }

    [Button("顯示對話提示")]
    private void ShowHintUI()
    {
        npc_UIPanel.DOFade(1 , 0.5f);
    }

    [Button("隱藏對話提示")]
    private void HideHintUI()
    {
        npc_UIPanel.DOFade(0 , 0.5f);
    }

    [Button("顯示第一段話")]
    public void PlayFirstDialog()
    {
        Dialog.Instance.SetPosition(transform.position);
        Dialog.Instance.SetDialogTexts(dialogData.dialogTexts);
        Dialog.Instance.PlayFirstDialog();
    }

    [Button("顯示下一段話")]
    public void PlayNextDialog()
    {
        Dialog.Instance.PlayNextDialog();
    }

    [Button("Skip對話")]
    public void SkipDialog()
    {
        Dialog.Instance.SkipWriter();
    }
}