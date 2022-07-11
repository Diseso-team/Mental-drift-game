using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[Serializable]
public struct CollapseObj
{
    public RectTransform obj;
    [ReadOnly] public Vector2 primalPos;
    [ReadOnly] public Vector2 primalScale;

    public void InitializePos()
    {
        primalPos = obj.position;
    }
    public void InitializeScale()
    {
        primalScale = obj.localScale;
    }
}
public class Menu : MonoBehaviour
{
    [Header("Menu objects")]
    [SerializeField] private CollapseObj playButton;
    [SerializeField] private CollapseObj chooseLevelButton;
    [SerializeField] private CollapseObj creditsButton;
    [Header("Level objects")]
    [SerializeField] private CollapseObj levelPanel;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;

    private const float span = 1;

    private void Awake()
    {
        playButton.InitializePos();
        chooseLevelButton.InitializePos();
        creditsButton.InitializePos();

        levelPanel.InitializeScale();
    }

    private void Start()
    {
        OpenButton(playButton, duration, curve);
        OpenButton(chooseLevelButton, duration, curve, 0.2f);
        OpenButton(creditsButton, duration, curve, 0.4f);
    }
    
    public void Play()
    {
        CloseButton(playButton, duration/2);
        CloseButton(chooseLevelButton, duration/2, 0.1f);
        CloseButton(creditsButton, duration/2, 0.2f);
        OpenLevelPanel(levelPanel, duration, curve);
    }


    private void OpenLevelPanel(CollapseObj a, float duration_, AnimationCurve curve_)
    {
        a.obj.gameObject.SetActive(true);
        a.obj.localScale = Vector3.one/10;
        a.obj.DOScale(1, duration_).SetEase(curve_);
    }

    private void CloseButton(CollapseObj a, float duration_, float delay = 0)
    {
        float startX = Camera.main.ScreenToWorldPoint(Vector3.zero).x - a.obj.sizeDelta.x / 2;

        Vector2 posToMove = new(startX, a.obj.position.y);

        a.obj.DOMove(posToMove, duration_).SetDelay(delay);
    }
    private void OpenButton(CollapseObj a, float duration_, AnimationCurve curve_, float delay = 0)
    {
        float startX = Camera.main.ScreenToWorldPoint(Vector3.zero).x - a.obj.sizeDelta.x / 2;
        a.obj.position = new(startX, a.obj.position.y);
        a.obj.DOMove(a.primalPos, duration_).SetEase(curve_).SetDelay(delay + span);
    }
}
