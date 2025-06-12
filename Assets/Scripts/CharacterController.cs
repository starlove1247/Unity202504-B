using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterData characterData;

    [SerializeField]
    private PlayerInput playerInput;

    private InputAction moveAction;

    public static CharacterController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        canMove        = true;
        moveAction     = playerInput.actions.FindAction("Move");
        interactAction = playerInput.actions.FindAction("Interact");
    }

    private bool isInteractionPressed;
    // Update is called once per frame
    void Update()
    {
        isInteractionPressed = interactAction.IsPressed();
        // 如果我放開按鍵，且我手上有物品，那放開物品
        if (isInteractionPressed == false && itemInHand)
        {
            itemInHand.GetComponent<Rigidbody2D>().simulated = true;                    
            itemInHand.transform.parent                      = null;
            itemInHand                                       = null;
        }
        // 如果不能移動，就不往下執行
        if (canMove == false) return;
        var moveVector2 = moveAction.ReadValue<Vector2>();
        var direction   = new Vector3(moveVector2.x , moveVector2.y , 0); // 移動方向
        // Time.deltaTime = 1/fps 抵銷FPS的影響
        transform.position += direction * characterData.moveSpeed * Time.deltaTime;
    }

    [SerializeField]
    private Transform hand;

    private Item itemInHand;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isInteractionPressed==false) return;

        if (col.TryGetComponent<Item>(out var item))
        {
            itemInHand = item;
            item.GetComponent<Rigidbody2D>().simulated = false;
            item.transform.parent                      = hand;
            item.transform.localPosition               = new Vector3(0 , 0 , 0);
        }
    }

    /// <summary>
    /// 角色可不可以移動的狀態
    /// </summary>
    private bool canMove;

    private InputAction interactAction;

    public void SetCanMoving(bool canMove)
    {
        this.canMove = canMove;
    }
}