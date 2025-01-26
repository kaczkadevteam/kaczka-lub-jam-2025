using System;
using System.Collections.Generic;
using UnityEngine;

public class BombelekKoncowkiAnimator : MonoBehaviour
{
    [SerializeField] private GameObject aliveBombelek;
    [SerializeField] private GameObject deadBombelek;

    void Update()
    {
        if (GameManager.Instance.BabyHealth > 0) return;

        aliveBombelek.SetActive(false);
        deadBombelek.SetActive(true);
    }
}
