using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterData characterData;

    [SerializeField]
    private PlayerInput playerInput;

    private InputAction moveAction;

    private void Start()
    {
        canMove    = true;
        moveAction = playerInput.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        // 如果不能移動，就不往下執行
        if (canMove == false) return;
        var moveVector2 = moveAction.ReadValue<Vector2>();
        var direction   = new Vector3(moveVector2.x , moveVector2.y , 0); // 移動方向
        // Time.deltaTime = 1/fps 抵銷FPS的影響
        transform.position += direction * characterData.moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 角色可不可以移動的狀態
    /// </summary>
    private bool canMove ;

    public void SetCanMoving(bool canMove)
    {
        this.canMove = canMove;
    }
}