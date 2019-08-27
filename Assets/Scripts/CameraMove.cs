using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Vector2 MinPos;

    public Vector2 MaxPos;

    public Transform player;

    /// <summary>
    /// 相机移动速度
    /// </summary>
    public float speed = 2;

    private void Update()
    {
        Vector3 pos = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed);
        if (pos.x <= MinPos.x || pos.x >= MaxPos.x)
        {
            pos.x = transform.position.x;
        }
        if (pos.y <= MinPos.y || pos.y >= MaxPos.y)
        {
            pos.y = transform.position.y;
        }
        transform.position = new Vector3(pos.x, pos.y, -10);

    }



}
