using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    private float X_startPos;

    [Header("Menu buttons")]
    [SerializeField] private RectTransform play;
    [SerializeField] private RectTransform chooseLevel;
    [SerializeField] private RectTransform credits;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;

    private float span = 1;

    private Vector2 playPos;
    private Vector2 chooseLevelPos;
    private Vector2 creditsPos;


    private void Awake()
    {
        X_startPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x - play.sizeDelta.x/2;
        playPos = play.position;
        chooseLevelPos = chooseLevel.position;
        creditsPos = credits.position;
    }

    private void Start()
    {
        play.position = new Vector3(X_startPos, play.position.y);
        chooseLevel.position = new Vector3(X_startPos, chooseLevel.position.y);
        credits.position = new Vector3(X_startPos, credits.position.y);

        play.DOMoveX(playPos.x, duration).SetEase(curve).SetDelay(0 + span);
        chooseLevel.DOMoveX(chooseLevelPos.x, duration).SetEase(curve).SetDelay(0.2f + span);
        credits.DOMoveX(creditsPos.x, duration).SetEase(curve).SetDelay(0.4f + span);
    }
}
