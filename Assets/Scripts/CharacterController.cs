using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        // 水平方向 -1 ~ 1
        var horizontal = Input.GetAxisRaw("Horizontal");
        // 垂直方向
        var vertical  = Input.GetAxisRaw("Vertical");
        var direction = new Vector3(horizontal , vertical , 0); // 移動方向
        // Time.deltaTime = 1/fps 抵銷FPS的影響
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}