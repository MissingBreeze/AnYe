using UnityEngine;


public class EnemyMoveManage : MonoBehaviour
{

    //public Vector2 firstPos;

    //public Vector2 secondPos;

    /// <summary>
    /// 速度，追踪速度
    /// </summary>
    public float speed, runSpeed;

    /// <summary>
    /// 跳跃力大小
    /// </summary>
    public float JumpForce;

    ///// <summary>
    ///// 方向,1向右，-1向左
    ///// </summary>
    public float direction;

    //private Rigidbody2D rd;

    private Animator animator;

    /// <summary>
    /// 警戒，追踪,停留
    /// </summary>
    public bool isGuard, isTrack, isStand;

    private float standTime, guardTime,attackTime;

    public Transform player;

    //private Vector2 distance;

    public void Start()
    {
        direction = 1;
        //rd = transform.GetComponent<Rigidbody2D>();
        //animator = transform.GetComponent<Animator>();
        //animator.SetFloat("stateDir", dir);
        Init();
    }

    public virtual void Init() { }

    public void Update()
    {
        Ray2D ray = new Ray2D(transform.position, Vector2.right * direction);
        RaycastHit2D rayInfo = Physics2D.Raycast(ray.origin, ray.direction, 5);
        Debug.DrawRay(ray.origin, ray.direction * 5, Color.blue);
        //Vector2 velocity;

        float distance = Vector2.Distance(transform.position, player.position);
        Debug.LogError(distance);
        if(distance < 5)
        {
            if(rayInfo.transform != null && rayInfo.transform.CompareTag("Player"))
            {
                if (!isGuard)
                    isGuard = true;
                guardTime += Time.deltaTime;

                if(distance < 3 || guardTime > 2) // 距离足够近或者警戒时间足够长，进入追击攻击状态
                {
                    isTrack = true;
                    if(distance < 1)
                    {
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
                    guardTime = 0;
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



        //Ray2D ray = new Ray2D(transform.position, Vector2.right * dir);
        //RaycastHit2D rayInfo = Physics2D.Raycast(ray.origin, ray.direction, 5);
        //Debug.DrawRay(ray.origin, ray.direction, Color.blue);
        //Vector2 velocity;

        //if (rayInfo.transform != null && rayInfo.transform.tag == "Player" && !isGuard && Mathf.Abs(transform.position.x - rayInfo.transform.position.x) < 5)
        //{
        //    player = rayInfo.transform;
        //    isGuard = true;
        //    guardTime = 0;
        //}

        //if (isGuard)
        //{
        //    if (player.position.x - transform.position.x < 0)
        //        dir = -1;
        //    else
        //        dir = 1;

        //    distance.x = Mathf.Abs(transform.position.x - player.position.x);
        //    distance.y = Mathf.Abs(transform.position.y - player.position.y);
        //    guardTime += Time.deltaTime;
        //    rd.velocity = Vector2.zero;
        //    if ((distance.x < 5 && guardTime > 2) || distance.x < 3) // 距离小于2或者警戒时间大于2，进入追踪攻击状态
        //    {
        //        isTrack = true;
        //    }
        //    if(distance.x > 5 || distance.y > 2)
        //    {
        //        if (isTrack)
        //        {
        //            guardTime = 0;
        //            isTrack = false;
        //        }
        //        else if(guardTime > 2)
        //        {
        //            isGuard = false;
        //        }
        //    }

        //    if (isTrack)  // 追击，进攻
        //    {
        //        if (distance.x > 1) // 追击
        //        {
        //            velocity = new Vector2(runSpeed * dir, rd.velocity.y);
        //            Move(velocity);
        //            animator.SetFloat("stateDir", dir);
        //            animator.speed = 1.5f;
        //            attackTime = 0;
        //        }
        //        else// 攻击
        //        {
        //            if (attackTime <= 0)
        //            {
        //                attackTime = 2;
        //                animator.SetFloat("speed", 0);
        //                animator.SetBool("isAttack", true);
        //            }
        //            else
        //                attackTime -= Time.deltaTime;
        //        }
        //    }
        //    else
        //    {
        //        animator.SetBool("isGuard", true);
        //    }
        //    return;
        //}

        //if (!isStand) // 不是处在停留状态，进行移动
        //{
        //    if (transform.position.x >= secondPos.x || transform.position.x <= firstPos.x) // 到达或超过左右点原地警戒等待
        //    {
        //        isStand = true;
        //        standTime = 0;
        //        animator.SetFloat("speed", 0);
        //    }
        //    else
        //    {
        //        velocity = new Vector2(speed * dir, rd.velocity.y);
        //        if (rayInfo.transform != null)
        //        {
        //            if (rayInfo.transform.tag == "Wall" && Mathf.Abs(transform.position.x - rayInfo.point.x) < 0.5f)
        //            {
        //                velocity += new Vector2(0, JumpForce);
        //            }
        //        }
        //        Move(velocity);
        //    }
        //}
        //else
        //{
        //    rd.velocity = Vector2.zero;
        //    standTime += Time.deltaTime;
        //    if(standTime > 2)
        //    {
        //        if(dir == 1)
        //        {
        //            transform.position = new Vector3(secondPos.x - 0.1f, transform.position.y, transform.position.z);
        //        }
        //        else
        //        {
        //            transform.position = new Vector3(firstPos.x + 0.1f, transform.position.y, transform.position.z);
        //        }
        //        dir *= -1;
        //        isStand = false;
        //    }
        //}
        //animator.SetFloat("stateDir", dir);
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