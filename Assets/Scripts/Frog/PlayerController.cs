using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private enum Direction
    {
        Up,
        Right,
        Left
    }
    public TerrainManager terrainManager;

    private Direction direction;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer renderer;
    private PlayerInput playerInput;
    private BoxCollider2D collider;

    [Header("得分")]
    public int stepPoint;
    public int pointResult;

    [Header("跳跃")]
    public float jumpDistance;
    private float moveDistance;
    private bool buttonHold;
    private Vector2 destination;
    private bool isJump;
    private bool canJump;

    private bool isDead;

    private Vector2 touchPosition;

    // 射线检测
    private RaycastHit2D[] result = new RaycastHit2D[2];

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        destination = transform.position;

        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (isJump)
        {
            rigidBody.position = Vector2.Lerp(transform.position, destination, 0.134f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("on trigger stay");
        if (!isJump && collision.CompareTag("Water"))
        {
            bool inWater = true;

            Physics2D.RaycastNonAlloc(transform.position + Vector3.up * 0.1f, Vector2.zero, result);
            foreach (var hit in result)
            {
                if (hit.collider == null) {
                    Debug.Log("raycast hit nothing");
                    continue; 
                }
                if (hit.collider.CompareTag("Wood"))
                {
                    Debug.Log("raycast hit wood");
                    transform.parent = hit.collider.transform;
                    inWater = false;
                    break;
                }
            }

            if (inWater && !isJump)
            {
                // Game Over
                Debug.Log("Game over on water");
                isDead = true;
            }

        }

        if (collision.CompareTag("Border") || collision.CompareTag("Car"))
        {
            Debug.Log("Game over on Border or Car");
            isDead = true;
        }



        if (!isJump && collision.CompareTag("Obstacle"))
        {
            Debug.Log("Game over on Obstacle");
            isDead = true;
        }

        if (isDead)
        {
            collider.enabled = false;
            EventHandler.CallGameOverEvent();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }


    private void Update()
    {
        if (isDead)
        {
            disableInput();
            return;
        }
        if (canJump)
        {
            TriggerJump();
        }
    }

    #region 回调

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (isJump)
        {
            return;
        }

        if (ctx.performed)
        {
            moveDistance = jumpDistance;
            canJump = true;
            AudioManager.instance.SetJumpClip(false);
        }

        if (direction == Direction.Up)
        {
            pointResult += stepPoint;
        }
    }

    public void LongJump(InputAction.CallbackContext ctx)
    {
        if (isJump)
        {
            return;
        }

        if (ctx.performed)
        {
            moveDistance = jumpDistance * 2;
            pointResult += stepPoint * 2;
            buttonHold = true;
            
        }

        if (ctx.canceled && buttonHold)
        {
            canJump = true;
            buttonHold = false;
            AudioManager.instance.SetJumpClip(true);
        }

    }

    public void GetTouchPosition(InputAction.CallbackContext context)
    {

        if (isJump)
        {
            return;
        }

        if (context.performed)
        {
            Vector2 screenPosition = context.ReadValue<Vector2>();
            touchPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            var offset = ((Vector3)touchPosition - transform.position).normalized;

            if (Mathf.Abs(offset.x) <= 0.7)
            {
                direction = Direction.Up;
            }
            else if (offset.x < 0)
            {
                direction = Direction.Left;
            }
            else
            {
                direction = Direction.Right;
            }
        }
    }

    #endregion

    /// <summary>
    /// 触发jump动画
    /// </summary>
    private void TriggerJump()
    {
        canJump = false;

        switch (direction)
        {
            case Direction.Right:
                animator.SetBool("isSide", true);
                destination = new Vector2(transform.position.x + moveDistance, transform.position.y);
                transform.localScale = new Vector3(-1, 1, 1);
                break;

            case Direction.Left:
                animator.SetBool("isSide", true);
                destination = new Vector2(transform.position.x - moveDistance, transform.position.y);
                transform.localScale = Vector3.one;
                break;

            case Direction.Up:
                animator.SetBool("isSide", false);
                destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
                transform.localScale = Vector3.one;

                break;
        }

        animator.SetTrigger("Jump");
    }

    public void JumpAnimationEvent()
    {
        AudioManager.instance.PlayJumpFx();
        Debug.Log("Jump event!");
        isJump = true;

        renderer.sortingLayerName = "Front";

        transform.parent = null;
    }

    public void FinishJumpAnimationEvent() 
    {
        Debug.Log("Jump event finish!");
        renderer.sortingLayerName = "Middle";
        isJump = false;

        if (direction == Direction.Up && !isDead)
        {
            //TODO 得分
            EventHandler.CallGetPointEvent(pointResult);
        }
    }

    private void disableInput()
    {
        playerInput.enabled = false;
    }

    private void enableInput()
    {
        playerInput.enabled = true ;
    }
}
