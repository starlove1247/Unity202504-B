using System;
using Scripts.Custom;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class GameFlow : MonoBehaviour
    {
        [SerializeField]
        private CharacterController characterPrefab;

        [SerializeField]
        private Dialog dialogPrefab;

        [SerializeField]
        private NPC npcPrefab;

        private void Start()
        {
            // 註冊事件處理器
            EventBus.Subscribe<DialogEventHandler>();
            // 遊戲開始，產生角色、對話、NPC
            Instantiate(characterPrefab);
            Instantiate(dialogPrefab);
            Instantiate(npcPrefab);
        }
    }
}