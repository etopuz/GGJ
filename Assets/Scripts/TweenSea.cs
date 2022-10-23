using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenSea : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float xMove;
    [SerializeField] float xMoveTime;
    void Start()
    {
        for (int i = 0; i < transform.childCount-1; i++)
        {
            transform.GetChild(i).DOScaleY(Random.Range(1.1f,1.2f),Random.Range(1,4)).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}
