using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rd;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float Speed;
    /// <summary>
    /// 跳跃高度
    /// </summary>
    public float JumpForce;    // recommend range: 1500 - 10000

    /// <summary>
    /// 血量
    /// </summary>
    public int Hp = 5;


    public CameraMove cameraScript;

    /// <summary>
    /// 是否在地面上
    /// </summary>
    private bool isGrounded;

    /// <summary>
    /// 是否在播放普通攻击动画
    /// </summary>
    private bool isAttack;

    private Animator animator;

    /// <summary>
    /// 方向
    /// </summary>
    private float directionNol;


    private RaycastHit2D rayInfo;


    private bool isDead;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
        animator.SetFloat("Direction", 1);
        directionNol = 1;

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void FixedUpdate()
    {
        if (isDead)
            return;

        AnimatorStateInfo aniStateInto = animator.GetCurrentAnimatorStateInfo(0);
        if (aniStateInto.IsName("AttackTree"))
        {
            rd.velocity = Vector2.zero;
            if (!isAttack)
                isAttack = true;
        }
        else if(isAttack)
        {
            animator.SetBool("isAttack", false);
            isAttack = false;
        }

        float walkFactor = Input.GetAxisRaw("Horizontal");
        float jumpFactor = Input.GetAxisRaw("Jump");  // joystick button 3

        // Calculate walk velocity
        Vector2 walkDirection = (Vector2.right * walkFactor).normalized;
        Vector2 walkVelocity = walkDirection * Speed;

        if(walkDirection.x != 0)
        {
            directionNol = walkDirection.x;
        }

        if (Input.GetKeyDown(KeyCode.X) && !isAttack)
        {
            isAttack = true;
            animator.SetBool("isAttack", true);
            animator.SetFloat("AttackDir", directionNol);
            Ray2D ray = new Ray2D(transform.position, Vector2.right * directionNol);

            rayInfo = Physics2D.Raycast(ray.origin, ray.direction,1);
            //Debug.LogError(rayInfo.distance);
            if(rayInfo.transform != null && rayInfo.transform.tag == "Enemy")
            {
                animator.SetFloat("AttackDis", rayInfo.distance);
                if(rayInfo.distance < 1)
                {
                    Time.timeScale = 0;
                }
            }
            else
            {
                animator.SetFloat("AttackDis", 1f);
            }

        }
        else if(!isAttack)
        {
            // Calculate jump velocity
            if (jumpFactor > 0 && isGrounded)
            {
                isGrounded = false;
                rd.AddForce(Vector2.up * JumpForce * jumpFactor);
                animator.SetBool("isJump", true);
            }
            Vector2 jumpVelocity = new Vector2(0, rd.velocity.y);
            // Calculate move velocity
            Vector2 moveVelocity = walkVelocity + jumpVelocity;
            rd.velocity = moveVelocity;
            animator.SetFloat("JumpDir", directionNol);
            MoveAnimation(moveVelocity.x, walkDirection.x);
            //cameraScript.direction = walkDirection.x;
        }


    }

    /// <summary>
    /// 人物移动动画播放
    /// </summary>
    /// <param name="speed">X轴移动速度</param>
    /// <param name="directionNor">人物左右方向</param>
    private void MoveAnimation(float speed, float directionNor)
    {
        if (speed != 0)
        {
            animator.SetFloat("Direction", directionNor);
            cameraScript.direction = directionNor;
        }
        animator.SetFloat("Speed", speed);
    }

    /// <summary>
    /// 动画结束重置状态
    /// </summary>
    public void AnimationEnd()
    {
        animator.SetBool("isAttack", false);
        if(Time.timeScale == 0)
            Time.timeScale = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJump", false);
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        //    isGrounded = false;
    }


    /// <summary>
    /// 受伤
    /// </summary>
    public void Injured(int hurt)
    {
        Hp -= hurt;
        if(Hp <= 0)
        {
            isDead = true;
            transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PlayerDie");
            animator.enabled = false;
            Destroy(transform.GetComponent<Rigidbody2D>());
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.35f, transform.position.z);
            Instantiate(Resources.Load("Prefabs/UI"), GameObject.Find("Canvas").transform);
            Time.timeScale = 0;
        }
    }


    public void Demage(int num)
    {
        if(rayInfo.transform!= null && rayInfo.transform.CompareTag("Enemy"))
        {
            rayInfo.transform.GetComponent<EnemyControl>().Injured(num);
        }
    }
}
