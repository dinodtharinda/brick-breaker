using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Breaker : MonoBehaviour

{
    [SerializeField] private GameObject giftPrefab;

    private int ballCollisionCount = 0;
    private Tilemap tilemap;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    Vector3 hitPosition;
    Vector3Int cellPosition;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ballCollisionCount++;

            hitPosition = collision.GetContact(0).point;


            cellPosition = tilemap.WorldToCell(hitPosition);


            if (tilemap.GetTile(cellPosition))
            {
                tilemap.SetTile(cellPosition, null);

                if (ballCollisionCount > 20)
                {
                    DropGiftPrefab(hitPosition);
                    ballCollisionCount = 0;
                }


                Debug.Log("Collision Bricks " + ballCollisionCount);
            }
            ballCollisionCount++;
        }



    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ballCollisionCount++;

            hitPosition = collision.GetContact(0).point;


            cellPosition = tilemap.WorldToCell(hitPosition);


            if (tilemap.GetTile(cellPosition))
            {
                tilemap.SetTile(cellPosition, null);

                if (ballCollisionCount > 20)
                {
                    DropGiftPrefab(hitPosition);
                    ballCollisionCount = 0;
                }


                Debug.Log("Collision Bricks " + ballCollisionCount);
            }
            ballCollisionCount++;
        }



    }

    private void DropGiftPrefab(Vector3 position)
    {
        Instantiate(giftPrefab, position, Quaternion.identity);
    }

}
