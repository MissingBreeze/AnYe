
using System;
using UnityEngine;

public class EnemyControl: EnemyMoveManage
{
    /// <summary>
    /// 巡逻范围左边的点坐标
    /// </summary>
    public Vector2 firstPos;

    /// <summary>
    /// 巡逻范围右边的点坐标
    /// </summary>
    public Vector2 secondPos;

    /// <summary>
    /// 巡逻速度
    /// </summary>
    public float walkSpeed;

    /// <summary>
    /// 追击速度
    /// </summary>
    public float runSpeed;

    /// <summary>
    /// 跳跃力大小
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// 停留时间
    /// </summary>
    public float standTime;

    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float attackTime;

    /// <summary>
    /// 血量
    /// </summary>
    public int hp = 5;

    /// <summary>
    /// 是否是在停留状态
    /// </summary>
    private bool isStand;

    private Vector2 velocity;

    /// <summary>
    /// 攻击间隔计算变量
    /// </summary>
    private float attackTempTime;

    /// <summary>
    /// 停留时间计算变量
    /// </summary>
    private float standTempTime;

    /// <summary>
    /// 2D物理组件
    /// </summary>
    private Rigidbody2D rigidbody2D;

    /// <summary>
    /// 动画播放器
    /// </summary>
    private Animator animator;

    /// <summary>
    /// 初始化参数
    /// </summary>
    public override void Init()
    {
        base.Init();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
        attackTempTime  = standTempTime = 0;
    }

    /// <summary>
    /// 巡逻
    /// </summary>
    public override void Patrol()
    {
        base.Patrol();
        if (isStand)
        {
            velocity = Vector2.zero;
            Move();
            Stand();
            return;
        }

        if (direction == 1)
        {
            if (transform.position.x > secondPos.x) // 到达点，停留
            {
                isStand = true;
                return;
            }
        }
        else
        {
            if (transform.position.x < firstPos.x) // 到达点，停留
            {
                isStand = true;
                return;
            }
        }

        velocity = new Vector2(walkSpeed * direction, -5);// y轴值保证在悬空时有下落速度（模拟重力？）

        if (rayInfo.transform != null && rayInfo.transform.CompareTag("Wall") && Vector2.Distance(transform.position, rayInfo.point) < 0.4f)
        {
            velocity = new Vector2(velocity.x, jumpForce);
        }
        Move();
    }

    /// <summary>
    /// 停留
    /// </summary>
    private void Stand()
    {
        standTempTime += Time.deltaTime;
        if(standTempTime > standTime)
        {
            standTempTime = 0;
            isStand = false;
            direction *= -1;
        }
    }

    /// <summary>
    /// 移动
    /// </summary>
    public override void Move()
    {
        base.Move();
        rigidbody2D.velocity = velocity;
        animator.SetFloat("stateDir", direction);
        animator.SetFloat("speed", velocity.x);
    }

    /// <summary>
    /// 警戒
    /// </summary>
    public override void Guard()
    {
        base.Guard();
        velocity = Vector2.zero;
        Move();
        animator.SetBool("isGuard", true);
    }

    /// <summary>
    /// 追击
    /// </summary>
    public override void Track()
    {
        base.Track();
        velocity = new Vector2(runSpeed * direction, -5);
        Move();
    }

    /// <summary>
    /// 攻击
    /// </summary>
    public override void Attack()
    {
        base.Attack();
        velocity = new Vector2(0, -5);
        Move();
        attackTempTime -= Time.deltaTime;
        if(attackTempTime <= 0)
        {
            animator.SetBool("isAttack", true);
            attackTempTime = attackTime;
        }
    }

    /// <summary>
    /// 动画结束重置状态
    /// </summary>
    public void AnimationEnd()
    {
        animator.SetBool("isGuard", false);
        animator.SetBool("isAttack", false);
    }

    public void Demage()
    {
        if(Vector2.Distance(transform.position,player.position) <= attackDistance)
        {
            player.GetComponent<PlayerControl>().Injured(1);
        }
    }


    public void Injured(int hurt)
    {
        hp -= hurt;
        if (hp <= 0)
        {
            isDead = true;
            animator.enabled = false;
            transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("EnemyDie");
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.34f, transform.position.z);
            Destroy(transform.GetComponent<Rigidbody2D>());
            Destroy(transform.GetComponent<BoxCollider2D>());
        }
    }


    public override void Dead()
    {
        base.Dead();
        //rigidbody2D.velocity = Vector2.zero;
    }
}

