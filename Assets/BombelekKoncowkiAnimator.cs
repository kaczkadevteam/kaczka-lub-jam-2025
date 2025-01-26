using System;
using System.Collections.Generic;
using UnityEngine;

public class BombelekKoncowkiAnimator : MonoBehaviour
{
    [SerializeField] private List<Animator> animatoryBombelka;

    void Update()
    {
        if (GameManager.Instance.BabyHealth > 0) return;

        foreach (var animatorBombelka in animatoryBombelka)
        {
            animatorBombelka.enabled = false;
        }
    }
}
