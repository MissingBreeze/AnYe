using System;
using UnityEngine;


public class EnemyMoveManage : MonoBehaviour
{
    ///// <summary>
    ///// 方向,1向右，-1向左
    ///// </summary>
    public float direction;

    /// <summary>
    /// 射线检测结果
    /// </summary>
    public RaycastHit2D rayInfo;

    /// <summary>
    /// 警戒，追踪,停留
    /// </summary>
    public bool isGuard, isTrack;

    /// <summary>
    /// 警戒时间
    /// </summary>
    public float guardTime;

    /// <summary>
    /// 警戒距离
    /// </summary>
    public float guardDistance;

    /// <summary>
    /// 攻击距离
    /// </summary>
    public float attackDistance;

    /// <summary>
    /// 玩家
    /// </summary>
    public Transform player;

    /// <summary>
    /// 警戒时间计算变量
    /// </summary>
    private float guardTempTime;

    public bool isDead;

    public void Start()
    {
        direction = 1;
        guardTempTime = guardTime;
        Init();
    }

    public virtual void Init() { }

    public void Update()
    {
        if (isDead)
        {
            Dead();
            return;
        }

        Ray2D ray = new Ray2D(transform.position, Vector2.right * direction);
        rayInfo = Physics2D.Raycast(ray.origin, ray.direction, 5);
        Debug.DrawRay(ray.origin, ray.direction * 5, Color.blue);

        float distance = Vector2.Distance(transform.position, player.position);
        if(distance < guardDistance)
        {
            if (rayInfo.transform != null && rayInfo.transform.CompareTag("Player"))
            {
                if (!isGuard)
                    isGuard = true;
                guardTempTime -= Time.deltaTime;

                if(distance < 3 || guardTempTime < 0) // 距离足够近或者警戒时间足够长，进入追击攻击状态
                {
                    isTrack = true;
                    if(distance < attackDistance)
                    {
                        guardTempTime = guardTime;
                        Attack();
                    }
                    else
                    {
                        Track();
                    }
                }
                else // 保持警戒状态
                {
                    Guard();
                }
            }
            else // 玩家不在范围
            {
                if (isGuard) // 警戒时间重置
                {
                    guardTempTime -= Time.deltaTime;
                    if(guardTempTime < 0)
                    {
                        guardTempTime = guardTime;
                        isGuard = false;
                    }
                    Guard();
                }
                else  // 继续巡逻
                {
                    Patrol();
                }
            }
        }
        else
        {
            Patrol();
        }
    }

    public virtual void Dead()
    {
        
    }

    /// <summary>
    /// 巡逻
    /// </summary>
    public virtual void Patrol()
    {

    }

    /// <summary>
    /// 移动
    /// </summary>
    public virtual void Move()
    {

    }

    /// <summary>
    /// 警戒
    /// </summary>
    public virtual void Guard()
    {

    }

    /// <summary>
    /// 追击
    /// </summary>
    public virtual void Track()
    {

    }

    /// <summary>
    /// 攻击
    /// </summary>
    public virtual void Attack()
    {

    }


    //private void Move(Vector2 velocity)
    //{
    //    rd.velocity = velocity;
    //    animator.SetFloat("speed", velocity.x);
    //}


    //public void AnimationEnd()
    //{
    //    animator.SetBool("isGuard", false);
    //    animator.SetBool("isAttack", false);
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if(collision.transform.CompareTag("Ground"))
    //        rd.velocity = new Vector2(rd.velocity.x, -5);
    //}


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Ground"))
    //        rd.velocity = new Vector2(rd.velocity.x, 0);
    //}
}