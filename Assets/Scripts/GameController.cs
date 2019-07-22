using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField]
    Text scenarioMessage;
    List<Scenario> scenarios = new List<Scenario>();

    [SerializeField]
    Transform buttonPanel;

    [SerializeField]
    Button optionButton;

    Scenario currentScenario;
    int index = 0;

    HashSet<string> items = new HashSet<string>();

    class Scenario
    {
        public string ScenarioID;
        public List<string> Texts = new List<string>();
        public List<Option> Options = new List<Option>();
        public string NextScenarioID;
    }

    class Option
    {
        public string Text;
        public Action Action;
        public Func<bool> IsFlagOK = () => { return true; };
    }

    Scenario scenario02;
    bool isCheckedKey = false;

    void Start ()
    {
        var scenario01 = new Scenario()
        {
            ScenarioID = "scenario01",
            Texts = new List<string>()
            {
                "目を覚ますと、知らない部屋にいた"
            },
            NextScenarioID = "scenario02"
        };

        scenario02 = new Scenario()
        {
            ScenarioID = "scenario02",
            //コマンド選択させる場合、Textsの要素は一個のみ
            Texts = new List<string>()
            {
                "どうする？",
            },
            Options = new List<Option>
            {
                new Option()
                {
                    Text = "辺りを見渡す",
                    Action = LookAround
                },
                new Option()
                {
                    Text = "鍵を拾う",
                    Action = TakeKey,
                    IsFlagOK = () =>
                    {
                        return isCheckedKey && !items.Contains("Key");
                    }
                },
                new Option()
                {
                    Text = "扉を開ける",
                    Action = OpenDoor,
                }
            }

        };
        scenarios.Add(scenario02);
        SetScenario(scenario01);
    }

    public void LookAround()
    {
        var scenario = new Scenario();
        scenario.NextScenarioID = "scenario02";
        if (!items.Contains("Key"))
        {
            scenario.Texts.Add("足元に鍵が落ちている");
            isCheckedKey = true;
        }
        else
        {
            scenario.Texts.Add("足元には何もない");
        }
        SetScenario(scenario);
    }

    public void OpenDoor()
    {
        var scenario = new Scenario();
        if (items.Contains("Key"))
        {
            scenario.Texts.Add("鍵を使って扉を開いた");
            scenario.Texts.Add("クリアー！");
        }
        else
        {
            scenario.Texts.Add("鍵がかかっていて開かない");
            scenario.NextScenarioID = "scenario02";
        }
        SetScenario(scenario);
    }

    public void TakeKey()
    {
        var scenario = new Scenario();
        scenario.Texts.Add("鍵を拾った");
        scenario.NextScenarioID = "scenario02";
        SetScenario(scenario);
        items.Add("Key");
    }
    
    void Update()
    {
        if (currentScenario != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //ボタンをクリックしたときに反応しないようにする
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                if (buttonPanel.GetComponentsInChildren<Button>().Length < 1)
                {
                    SetNextMessage();
                }
            }
        }
    }

    void SetScenario(Scenario scenario)
    {
        currentScenario = scenario;
        scenarioMessage.text = currentScenario.Texts[0];
        if (currentScenario.Options.Count > 0)
        {
            SetNextMessage();
        }
    }

    void SetNextMessage()
    {
        if (currentScenario.Texts.Count > index + 1)
        {
            index++;
            scenarioMessage.text = currentScenario.Texts[index];
        }
        else
        {
            ExitScenario();
        }
    }

    void ExitScenario()
    {
        index = 0;
        if (currentScenario.Options.Count > 0)
        {
            SetOptions();
        }
        else
        {
            scenarioMessage.text = "";
            var nextScenario = scenarios.Find
            (s => s.ScenarioID == currentScenario.NextScenarioID);
            if (nextScenario != null)
            {
                SetScenario(nextScenario);
            }
            else
            {
                currentScenario = null;
            }
        }
    }

    void SetOptions()
    {
        foreach (Option o in currentScenario.Options)
        {
            if (o.IsFlagOK())
            {
                Button b = Instantiate(optionButton);
                Text text = b.GetComponentInChildren<Text>();
                text.text = o.Text;
                b.onClick.AddListener(() => o.Action());
                b.onClick.AddListener(() => ClearButtons());
                b.transform.SetParent(buttonPanel, false);
            }
        }
    }

    void ClearButtons()
    {
        foreach (Transform t in buttonPanel)
        {
            Destroy(t.gameObject);
        }
    }

}
