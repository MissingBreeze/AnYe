
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


    private Vector2 velocity;



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
    }

    /// <summary>
    /// 巡逻
    /// </summary>
    public override void Patrol()
    {
        base.Patrol();

        if(direction == 1)
        {
            if(transform.position.x >= secondPos.x - 0.2f) // 到达点，停留
            {
                
            }


        }


        velocity = Vector2.right * direction;



    }

    /// <summary>
    /// 移动
    /// </summary>
    public override void Move()
    {
        base.Move();
        rigidbody2D.velocity = velocity;



    }

    /// <summary>
    /// 警戒
    /// </summary>
    public override void Guard()
    {
        base.Guard();

    }

    /// <summary>
    /// 追击
    /// </summary>
    public override void Track()
    {
        base.Track();

    }

    /// <summary>
    /// 攻击
    /// </summary>
    public override void Attack()
    {
        base.Attack();

    }
}

