//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using DG.Tweening;

//public class ScenarioController
//{
//    GameController gc;
//    private ScenarioHolder scenarioHolder;
//    private GUIManager gui;

//    Sequence seq = DOTween.Sequence();

//    bool isTextSending;

//    private bool isOptionsShowed;

//    Scenario currentScenario;
//    int index = 0;


//    public ScenarioController(GameController gc)
//    {
//        this.gc = gc;
//        scenarioHolder = new ScenarioHolder(gc);
//        gui = GameObject.Find("GUI").GetComponent<GUIManager>();
//    }

//    public void WaitClick()
//    {
//        if (currentScenario != null)
//        {
//            if (Input.GetMouseButtonDown(0))
//            {
//                if (EventSystem.current.IsPointerOverGameObject())
//                {
//                    Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//                    Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);

//                    if (collition2d != null)
//                    {
//                        var button = collition2d.gameObject.GetComponent<Button>();
//                        if (button != null) return;
//                    }
//                }

//                if (!isOptionsShowed)
//                {
//                    SetNextMessage();
//                }
//            }
//        }
//    }

//    public void SetOptionsPanel()
//    {
//        gui.ButtonPanel.gameObject.SetActive(isOptionsShowed);
//    }

//    public void SetScenario(string id)
//    {
//        currentScenario = scenarioHolder.GetScenario(id);
//        if (currentScenario == null) Debug.LogError("scenario not found");
//        SetNextMessage();
//    }

//    public void SetNextMessage()
//    {
//        if (currentScenario.Texts.Count > index)
//        {
//            var message = currentScenario.Texts[index];
//            if (seq.IsPlaying())
//            {
//                seq.Complete();
//            }
//            else
//            {
//                isTextSending = true;
//                gui.Text.text = "";
//                seq = DOTween.Sequence();
//                seq.Append
//                    (
//                    gui.Text.DOText
//                    (
//                        message,    // 表示したい文字列
//                         message.Length * 0.02f // 全てを表示するのにかかる時間
//                    ).SetEase(Ease.Linear).OnComplete(() => 
//                    {
//                        //ExitSendingText();
//                        index++;
//                    })
//                    );
//            }

//        }
//        else
//        {
//            ExitScenario();
//        }
//    }

//    //public void ExitSendingText()
//    //{
//    //    index++;
//    //}

//    public void ExitScenario()
//    {
//        index = 0;
//        if (currentScenario.Options.Count > 0)
//        {
//            SetOptions();
//        }
//        else
//        {
//            //gui.ScenarioMessage.text = "";
//            //var nextScenario = scenarioHolder.Scenarios.Find
//            //(s => s.ScenarioID == currentScenario.NextScenarioID);
//            if (!string.IsNullOrEmpty(currentScenario.NextScenarioID))
//            {
//                SetScenario(currentScenario.NextScenarioID);
//            }
//            else
//            {
//                currentScenario = null;
//            }
//        }
//    }

//    public void SetOptions()
//    {
//        isOptionsShowed = true;
//        foreach (Option o in currentScenario.Options)
//        {
//            if (o.IsFlagOK())
//            {
//                Button b = UnityEngine.Object.Instantiate(gui.OptionButton);
//                Text text = b.GetComponentInChildren<Text>();
//                text.text = o.Text;
//                b.onClick.AddListener(() => o.Action());
//                b.onClick.AddListener(() => onClickedOption());
//                b.transform.SetParent(gui.ButtonPanel, false);
//            }
//        }
//    }

//    public void onClickedOption()
//    {
//        isOptionsShowed = false;
//        foreach (Transform t in gui.ButtonPanel)
//        {
//            UnityEngine.Object.Destroy(t.gameObject);
//        }
//    }
//}