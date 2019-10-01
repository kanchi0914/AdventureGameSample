using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScenarioStream
{
    private GameController gc;
    public Actions Actions;

    private GUIManager gui;
    private SceneHolder sceneHolder;
    private Sequence seq = DOTween.Sequence();
    private bool isOptionsShowed;
    private Scene currentScene;

    public ScenarioStream(GameController gc)
    {
        this.gc = gc;
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();
        Actions = new Actions(gc);
        sceneHolder = new SceneHolder(this);
        seq.Complete();
    }

    public void WaitClick()
    {
        if (currentScene != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);

                    if (collition2d != null)
                    {
                        var button = collition2d.gameObject.GetComponent<Button>();
                        if (button != null) return;
                    }
                }

                if (!isOptionsShowed)
                {
                    SetNextProcess();
                }
            }
        }
    }

    public void SetComponents()
    {
        gui.ButtonPanel.gameObject.SetActive(isOptionsShowed);
        gui.Delta.gameObject.SetActive(!seq.IsPlaying() && !isOptionsShowed);
    }

    public void SetNextProcess()
    {
        currentScene.SendLines();
    }

    public void SetScene(string id)
    {
        currentScene = sceneHolder.Scenes.Find(s => s.ID == id);
        currentScene = currentScene.Clone();
        if (currentScene == null) Debug.LogError("scenario not found");
        SetNextProcess();
    }

    public void SetText(string text)
    {
        if (seq.IsPlaying())
        {
            seq.Complete();
        }
        else
        {
            gui.Text.text = "";
            seq = DOTween.Sequence();
            seq.Append
                (
                gui.Text.DOText
                (
                    text,    
                    text.Length * 0.02f 
                ).SetEase(Ease.Linear).OnComplete(() =>
                {
                   
                })
                );
        }
    }

    public void SetOptionsPanel()
    {
        gui.ButtonPanel.gameObject.SetActive(isOptionsShowed);
    }

    public void SetSpeaker(string s)
    {
        gui.Speaker.text = s;
    }

    public void SetCharactor(string s)
    {
        gc.Character01.Appear();
    }

    public void SetImage(string s)
    {
        gc.Character01.SetImage(s);
    }

    public void SetOptions(List<(string text, string nextScene)> options)
    {
        isOptionsShowed = true;
        foreach (var o in options)
        {
            Button b = Object.Instantiate(gui.OptionButton);
            Text text = b.GetComponentInChildren<Text>();
            text.text = o.text;
            b.onClick.AddListener(() => onClickedOption(o.nextScene));
            b.transform.SetParent(gui.ButtonPanel, false);
        }
    }

    public void onClickedOption(string nextID = "")
    {
        SetScene(nextID);
        isOptionsShowed = false;
        foreach (Transform t in gui.ButtonPanel)
        {
            UnityEngine.Object.Destroy(t.gameObject);
        }
    }

}