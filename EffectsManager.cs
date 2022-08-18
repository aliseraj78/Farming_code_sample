using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class EffectsManager : MonoBehaviour
{
    // Start is called before the first frame update
    [FormerlySerializedAs("animatedCoinPrefab")]
    [Header ("UI references")]
    [SerializeField] GameObject animatedCropPrefab;

    [FormerlySerializedAs("maxCoins")]
    [Space]
    [SerializeField] int maxCrops;
    Queue<GameObject> cropsQueue = new Queue<GameObject> ();


    [Space]
    [Header ("Animation settings")]
    [SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Ease easeType;
    [SerializeField] float spread;

    Vector3 targetPosition;
    

    void Awake ()
    {
        PrepareCoins ();
    }

    void PrepareCoins ()
    {
        GameObject coin;
        for (int i = 0; i < maxCrops; i++) {
            coin = Instantiate (animatedCropPrefab);
            coin.transform.parent = transform;
            coin.SetActive (false);
            cropsQueue.Enqueue (coin);
        }
    }


    public void HarvestEffect(Vector3 farmPos, Vector3 UIPos, Crop crop)
    {
        for (int i = 0; i < 5; i++) {
            //check if there's coins in the pool
            if (cropsQueue.Count > 0) {
                //extract a coin from the pool
                GameObject coin = cropsQueue.Dequeue ();
                coin.GetComponent<SpriteRenderer>().sprite = crop.icon;
                coin.SetActive (true);

                //move coin to the collected coin pos
                coin.transform.position = farmPos + new Vector3 (Random.Range (-spread, spread), 0f, 0f);

                //animate coin to target position
                float duration = Random.Range (minAnimDuration, maxAnimDuration);
                coin.transform.DOMove (UIPos, duration)
                    .SetEase (easeType)
                    .OnComplete (() => {
                        //executes whenever coin reach target position
                        coin.SetActive (false);
                        cropsQueue.Enqueue (coin);
                    });
            }
        }
    }
}
