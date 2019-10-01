using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    

    public HashSet<string> Items = new HashSet<string>();

    //public ScenarioController ScenarioController;

    public ScenarioStream ss;
    //Scenario scenario02;
    public bool IsCheckedKey = false;

    public GameObject charactorObject;

    [HideInInspector]
    public Character Character01;

    void Start ()
    {
        var prefab = Resources.Load("Charactor") as GameObject;
        charactorObject = Instantiate(prefab);
        Character01 = charactorObject.GetComponent<Character>();
        ss = new ScenarioStream(this);
        ss.SetScene("001");
    }

    void Update()
    {
        ss.WaitClick();
        ss.SetComponents();
    }



}
