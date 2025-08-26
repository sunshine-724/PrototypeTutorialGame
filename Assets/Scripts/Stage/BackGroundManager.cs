using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] List<GameObject> bgs;
    [SerializeField] GameObject bgsObjRow;
    [SerializeField] Player player;

    private SpriteRenderer[] spriteRenderersOfBgsObjRow;

    private Vector3 previousPlayerPosition;
    private float bgWidth;
    private float totalXDistance = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgs = bgs.OrderBy(bg => bg.transform.position.x).ToList(); // 昇順ソート

        // Row親ゲームオブジェクトから全ての子SpriteRenderコンポーネントを取得して、boundsを結合し、横幅を取得する
        spriteRenderersOfBgsObjRow = bgsObjRow.GetComponentsInChildren<SpriteRenderer>()
                                        .Where(sr => sr.gameObject != this.gameObject)
                                        .ToArray();
        Bounds combinedBounds = spriteRenderersOfBgsObjRow[0].bounds;
        for (int i = 1; i < spriteRenderersOfBgsObjRow.Length; i++)
        {
            combinedBounds.Encapsulate(spriteRenderersOfBgsObjRow[i].bounds);
        }
        bgWidth = combinedBounds.size.x;

        previousPlayerPosition = player.transform.position;

        Debug.Log("bgWidth: " + bgWidth);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = player.transform.position;
        float deltaPositionX = currentPosition.x - previousPlayerPosition.x; //正: プレイヤーは右に移動 負: プレイヤーは左に移動
        previousPlayerPosition = currentPosition;

        totalXDistance += deltaPositionX;

        if (Mathf.Abs(totalXDistance) >= bgWidth)
        {
            if (totalXDistance >= 0f)
            {
                Vector3 tmp = bgs.Last().gameObject.transform.position;
                tmp.x += bgWidth;
                bgs.First().transform.position = tmp;
            }
            else
            {
                Vector3 tmp = bgs.First().gameObject.transform.position;
                tmp.x -= bgWidth;
                bgs.Last().transform.position = tmp;
            }

            totalXDistance = 0f;
            bgs = bgs.OrderBy(bg => bg.transform.position.x).ToList();
        }
    }
}
