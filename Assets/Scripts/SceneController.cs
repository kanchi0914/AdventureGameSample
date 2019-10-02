using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneController
{
    private GameController gc;
    public Actions Actions;

    private GUIManager gui;
    private SceneHolder sh;
    private SceneReader sr;
    private string tempText;

    private Sequence textSeq = DOTween.Sequence();
    private Sequence imageSeq = DOTween.Sequence();
    private bool isOptionsShowed;
    private float messageSpeed = 0.02f;

    private Scene currentScene;
    public List<Character> Characters = new List<Character>();

    public SceneController(GameController gc)
    {
        this.gc = gc;
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();
        Actions = new Actions(gc);
        sh = new SceneHolder(this);
        sr = new SceneReader(this);
        textSeq.Complete();
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

                if (!isOptionsShowed && !imageSeq.IsPlaying())
                {
                    SetNextProcess();
                }
            }
        }
    }

    public void SetComponents()
    {
        gui.ButtonPanel.gameObject.SetActive(isOptionsShowed);
        gui.Delta.gameObject.SetActive
            (!textSeq.IsPlaying() && !isOptionsShowed && !imageSeq.IsPlaying());
    }

    public void SetNextProcess()
    {
        if (textSeq.IsPlaying())
        {
            SetText(tempText);
        }
        else
        {
            sr.ReadLines(currentScene);
        }
    }

    public void SetScene(string id)
    {
        currentScene = sh.Scenes.Find(s => s.ID == id);
        currentScene = currentScene.Clone();
        if (currentScene == null) Debug.LogError("scenario not found");
        SetNextProcess();
    }

    public void SetText(string text)
    {
        tempText = text;
        if (textSeq.IsPlaying())
        {
            textSeq.Complete();
        }
        else
        {
            gui.Text.text = "";
            textSeq = DOTween.Sequence();
            textSeq.Append
                (gui.Text.DOText
                (
                    text,
                    text.Length * messageSpeed
                ).SetEase(Ease.Linear));
        }
    }

    public void SetOptionsPanel()
    {
        gui.ButtonPanel.gameObject.SetActive(isOptionsShowed);
    }

    public void SetSpeaker(string name = "")
    {
        gui.Speaker.text = name;
    }

    public void SetCharactor(string name)
    {
        Characters.ForEach(c => c.Destroy());
        Characters = new List<Character>();
        AddCharactor(name);
    }

    public void AddCharactor(string name)
    {
        if (Characters.Exists(c => c.Name == name)) return;

        var prefab = Resources.Load("Charactor") as GameObject;
        var charactorObject = Object.Instantiate(prefab);
        var character = charactorObject.GetComponent<Character>();

        character.Init(name);
        Characters.Add(character);
        imageSeq = DOTween.Sequence();
        
        for (int i = 0; i < Characters.Count; i++)
        {
            var width = Screen.width;
            var pos = Characters[i].transform.position;
            var pos2 = gui.MainCamera.ScreenToWorldPoint(Vector3.zero);
            var pos3 = gui.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            var posWidth = pos3.x - pos2.x;
            var left = pos2.x + (posWidth * (i + 1) / (Characters.Count + 1));
            var cpos = new Vector3(left, gui.MainCamera.transform.position.y, 0);
            //新しく登場する人
            if (i == Characters.Count - 1)
            {
                imageSeq.Append(Characters[i].transform.DOMove(cpos, 0f))
                    .OnComplete( () => character.Appear());
            }
            //一人目
            else if (i == 0)
            {
                imageSeq.Append(Characters[i].transform.DOMove(cpos, 1.0f)).SetEase(Ease.OutCubic);
            }
            //二人目以降
            else
            {
                cpos = new Vector3(left, cpos.y, 0);
                imageSeq.Join(Characters[i].transform.DOMove(cpos, 1.0f)).SetEase(Ease.OutCubic);
            }
        }
    }

    public void SetImage(string name, string ID)
    {
        var character = Characters.Find(c => c.Name == name);
        character.SetImage(ID);
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