using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    [Header("移动设置")]
    public float tileSize = 1f;           // 每个瓦片的大小
    public float moveSpeed = 2f;           // 移动速度
    public LayerMask obstacleLayer;        // 障碍物层

    [Header("状态")]
    public bool isMoving = false;          // 是否正在移动

    private Vector2 targetPosition;         // 目标位置
    private Rigidbody2D playerRB;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        // 对齐到网格
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / tileSize) * tileSize,
            Mathf.Round(transform.position.y / tileSize) * tileSize,
            transform.position.z
        );

        targetPosition = transform.position;
    }

    void Update()
    {
        // 如果正在移动，不接收新输入
        if (isMoving) return;

        // 获取输入（限制为第一次按下的方向）
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 计算移动方向（优先处理水平方向）
        Vector2 moveDirection = Vector2.zero;

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            moveDirection = new Vector2(Mathf.Sign(horizontal), 0);
        }
        else if (Mathf.Abs(vertical) > 0.1f)
        {
            moveDirection = new Vector2(0, Mathf.Sign(vertical));
        }

        // 如果有移动输入
        if (moveDirection != Vector2.zero)
        {
            Vector2 newPosition = (Vector2)transform.position + moveDirection * tileSize;

            // 检查目标格子是否可以走
            if (CanMoveTo(newPosition))
            {
                StartCoroutine(MoveToPosition(newPosition));
            }
        }
    }

    bool CanMoveTo(Vector2 position)
    {
        // 检查是否有障碍物
        Collider2D hit = Physics2D.OverlapCircle(position, 0.1f, obstacleLayer);
        return hit == null;
    }

    IEnumerator MoveToPosition(Vector2 newPos)
    {
        isMoving = true;
        targetPosition = newPos;

        Vector2 startPos = transform.position;
        float distance = Vector2.Distance(startPos, newPos);
        float duration = distance / moveSpeed; // 根据距离计算移动时间
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // 线性插值移动
            transform.position = Vector2.Lerp(startPos, newPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保精确到达目标位置
        transform.position = newPos;

        isMoving = false;
    }

    // 可视化
    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, Vector3.one * (tileSize * 0.8f));

            if (isMoving)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(targetPosition, Vector3.one * (tileSize * 0.8f));
            }
        }
    }
}