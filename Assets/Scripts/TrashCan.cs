using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public enum TrashCanType
    {
        Plastic,
        Glass,
        Paper,
        Metal
    }
    [SerializeField] TrashCanType tct;

    public TrashCanType TCType
    {
        get => tct;
    }
}
