using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;

    [Header("攝影機移動速度")]
    public float speed;

    void Start()
    {
        player = GameObject.Find("player").transform;
    }

    void Update()
    {
        Track();
    }

    /// <summary>
    /// 攝影機跟隨玩家
    /// </summary>
    private void Track()
    {
        Vector3 pointPlayer = new Vector3(player.position.x, player.position.y, -10);
        Vector3 pointCamera = new Vector3(transform.position.x, transform.position.y, -10);

        transform.position = Vector3.Lerp(pointCamera, pointPlayer, 0.5f * Time.deltaTime * speed);

    }
}
