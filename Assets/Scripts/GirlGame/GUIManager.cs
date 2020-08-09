using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class GUIManager : MonoBehaviour
{
    public Camera MainCamera;
    public Transform ButtonPanel;
    public Button OptionButton;
    public Text Text;
    public Text Speaker;
    public GameObject Delta;

    private void Start()
    {
        Delta.transform.DOMoveY(-0.2f, 1.0f).SetRelative().SetEase(Ease.InCubic)
            .SetLoops(-1, LoopType.Restart);
    }
}