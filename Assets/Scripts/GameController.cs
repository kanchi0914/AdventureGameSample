using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public ScenarioStream ss;
    public GameObject charactorObject;

    [HideInInspector]
    public Character Character01;

    void Start ()
    {
        var prefab = Resources.Load("Charactor") as GameObject;
        charactorObject = Instantiate(prefab);
        Character01 = charactorObject.GetComponent<Character>();
        SetFirstScene();
    }

    void Update()
    {
        ss.WaitClick();
        ss.SetComponents();
    }

    void SetFirstScene()
    {
        ss = new ScenarioStream(this);
        ss.SetScene("001");
    }



}
