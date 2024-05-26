using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColour : MonoBehaviour
{
    public MeshRenderer meshRender;

    [Header("Settup")]
    public Color color = Color.red;
    public float duration = .2f;

    private Color _defaultColor;

    private Tween _currTween;

    private void Start()
    {
        _defaultColor = meshRender.material.GetColor("_EmissionColor");
    }


    [NaughtyAttributes.Button]
    public void Flash()
    {
        if(!_currTween.IsActive())
            _currTween = meshRender.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);


    }



}
