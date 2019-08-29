using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Vector2 MinPos;

    public Vector2 MaxPos;

    public Transform player;

    /// <summary>
    /// 摄像机宽度
    /// </summary>
    public float cameraWidth = 12.8f;

    /// <summary>
    /// 摄像机高度
    /// </summary>
    private float cameraHeight;

    /// <summary>
    /// 相机移动速度
    /// </summary>
    public float speed = 2;

    /// <summary>
    /// 偏移量
    /// </summary>
    public float offset;

    /// <summary>
    /// 方向
    /// </summary>
    public float direction;

    private void Start()
    {
        float aspectRatio = Screen.width * 1.0f / Screen.height;
        //float cameraHeight = transform.GetComponent<Camera>().orthographicSize * 2;
        //float cameraWidth = cameraHeight * aspectRatio;
        //Debug.LogError("camera size in unit=" + cameraWidth + "," + cameraHeight);
        cameraHeight = cameraWidth / aspectRatio;
        transform.GetComponent<Camera>().orthographicSize = cameraHeight / 2;
        
        //Debug.LogError(transform.GetComponent<BoxCollider2D>().bounds.size);

    }

    private void Update()
    {

        //Vector3 pos = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed);
        //if (pos.x <= MinPos.x || pos.x >= MaxPos.x)
        //{
        //    pos.x = transform.position.x;
        //}
        //if (pos.y <= MinPos.y || pos.y >= MaxPos.y)
        //{
        //    pos.y = transform.position.y;
        //}
        //transform.position = new Vector3(pos.x, pos.y, -10);

    }

    private void FixedUpdate()
    {
        Vector3 pos1 = new Vector3(player.position.x + (direction * offset), player.position.y, player.position.z);
        Vector3 pos = Vector3.Lerp(transform.position, pos1/*player.position*/, Time.deltaTime * speed);
        if (pos.x <= MinPos.x + cameraWidth  / 2 || pos.x >= MaxPos.x - cameraWidth / 2)
        {
            pos.x = transform.position.x;
        }
        if (pos.y <= MinPos.y + cameraHeight / 2 || pos.y >= MaxPos.y - cameraHeight / 2)
        {
            pos.y = transform.position.y;
        }
        transform.position = new Vector3(pos.x, pos.y, -10);




    }

}
