using System;
using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace;
using NaughtyAttributes;
using Scripts.Custom;
using TMPEffects.Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialog : MonoBehaviour
{
#region Public Variables

    public static Dialog Instance;

#endregion

#region Private Variables

    /// <summary>
    /// 目前的對話資料Index，從0開始
    /// </summary>
    private int dialogIndex = 0;

    private List<string> dialogTexts;

    /// <summary>
    /// 紀錄是不是在對話中的狀態
    /// </summary>
    private bool isInDialog;

    [SerializeField]
    private TMPWriter tmpWriter;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private GameObject nextDialogHintUI;

    

#endregion

#region Unity events

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        nextDialogHintUI.SetActive(false);
        tmpWriter.OnFinishWriter.AddListener(OnFinishWriter);
        tmpWriter.OnStartWriter.AddListener(OnStartWriter);
    }

#endregion

#region Public Methods

    [Button("關閉對話框")]
    public void CloseDialog()
    {
        gameObject.SetActive(false);
    }

    public bool IsInDialog()
    {
        return isInDialog;
    }

    public void PlayFirstDialog()
    {
        // 沒有任何對話資料，不做任何事情
        if (dialogTexts.Count == 0)
        {
            Debug.LogError("錯誤，對話資料鎮列為空");
            return;
        }

        EventBus.Raise<DialogStartEvent>(_ => _.OnDialogStarted());
        // characterController.SetCanMoving(false);
        isInDialog  = true;
        dialogIndex = 0; // 重置Index
        var dialogText = dialogTexts[dialogIndex];
        SetText(dialogText);
        PlayWriter();
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

    [Button("播放打字機效果")]
    public void PlayWriter()
    {
        tmpWriter.RestartWriter();
    }

    public void SetDialogTexts(List<string> texts)
    {
        dialogTexts = texts;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetText(string dialogText)
    {
        tmpWriter.SetText(dialogText);
    }

    [Button("跳過目前的打字機效果")]
    public void SkipWriter()
    {
        tmpWriter.SkipWriter();
    }

#endregion

#region Private Methods

    /// <summary>
    /// 按互動鍵去播放下一段對話
    /// </summary>
    /// <param name="obj"></param>
    private void InteractActionOnperformed(InputAction.CallbackContext obj)
    {
        Debug.Log($"互動鍵按下");
        // 如果對話完畢(最後一段話)，則關閉對話框
        if (tmpWriter.IsWriting == false && dialogIndex + 1 == dialogTexts.Count)
        {
            // characterController.SetCanMoving(true);
            EventBus.Raise<DialogEndEvent>(_ => _.OnDialogEnded());
            isInDialog = false;
            CloseDialog();
            return;
        }

        // 如果還在播放打字機效果，則Skip打字機效果
        if (tmpWriter.IsWriting) SkipWriter();
        // 如果打字機效果結束，則播放下一段文字
        else if (tmpWriter.IsWriting == false) PlayNextDialog();
    }

    private void OnDisable()
    {
        var interactAction = playerInput.actions.FindAction("Interact");
        interactAction.performed -= InteractActionOnperformed;
    }

    private void OnEnable()
    {
        var interactAction = playerInput.actions.FindAction("Interact");
        interactAction.performed += InteractActionOnperformed;
    }

    private void OnFinishWriter(TMPWriter arg0)
    {
        nextDialogHintUI.SetActive(true);
    }

    private void OnStartWriter(TMPWriter arg0)
    {
        nextDialogHintUI.SetActive(false);
    }

#endregion
}