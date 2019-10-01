using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioHolder
{

 //   GameController gc;
 //   Actions actions;

 //   public List<Scenario> Scenarios = new List<Scenario>();
 //   public List<Scenario> Scenarios_csv = new List<Scenario>();

 //   public ScenarioHolder(GameController gc)
 //   {
 //       this.gc = gc;
 //       actions = new Actions(gc);
 //       //Action Action = actions.LookAround;
 //       Init();

 //       var itemFile = Resources.LoadAll<TextAsset>("ScenarioData"); /*  Resouces/CSV下のCSV読み込み */
 //       foreach (TextAsset t in itemFile)
 //       {
 //           LoadCSV(t.name, t);
 //       }

 //   }

 //   public Scenario GetScenario(string id)
 //   {
 //       return Scenarios.Find(s => s.ScenarioID == id);
 //   }
    
 //   public void LoadCSV(string fileName, TextAsset file)
 //   {
 //       StringReader reader = new StringReader(file.text);
 //       reader.ReadLine();
 //       while (reader.Peek() > -1)
 //       {
 //           string line = reader.ReadLine();
 //           Scenarios_csv.Add(Parse(fileName, line));
 //       }
 //   }

 //   public Scenario Parse(string fileName, string line)
 //   {
 //       var data = line.Split(',');
 //       var scenario = new Scenario();
 //       scenario.sections.Add(new Scenario.Section()
 //       {
 //           Speaker = data[0],
 //           ChalactorImages = new List<string>(data[1].Split('/')),
 //           Text = data[2]
 //       });
 //       return scenario;
 //   }

 //   public void Init()
 //   {
 //       var scenario01 = new Scenario()
 //       {
 //           ScenarioID = "scenario01",
 //           Texts = new List<string>()
 //           {
 //               "その晩、私は隣室のアレキサンダー君に案内されて、始めて横浜へ遊びに出かけた。" +
　//"アレキサンダー君は、そんな遊び場所に就いてなら、日本人の私なんぞよりも、遙かに詳かに心得ていた。",
 //               "アレキサンダー君は、その自ら名告るところに依れば、旧露国帝室付舞踏師で、" +
 //               "革命後上海から日本へ渡って来たのだが、踊を以て生業とすることが出来なくなって、" +
 //               "つまり街頭で、よく見かける羅紗売りより僅かばかり上等な類のコーカサス人である。",
 //               "それでも、遉にコーカサス生れの故か、髪も眼も真黒で却々眉目秀麗" +
 //               "ハンサムな男だったので、貧乏なのにも拘らず、居留地女の間では、格別可愛がられているらしい。",
 //               "　――アレキサンダー君は、露西亜語の他に、拙い日本語と、同じ位拙い英語とを喋ることが出来る。",
 //               "　桜木町の駅に降りたのが、かれこれ九時時分だったので、私達は、先ず暗い波止場の方を廻って、山下町の支那街へ行った。"
 //           },
 //           NextScenarioID = "scenario02"
 //       };
 //       Scenarios.Add(scenario01);

 //       var scenario02 = new Scenario()
 //       {
 //           ScenarioID = "scenario02",
 //           //コマンド選択させる場合、Textsの要素は一個のみ
 //           //Texts = new List<string>()
 //           //{
 //           //    "どうする--------------------------------------------------？",
 //           //},
 //           Options = new List<Option>
 //           {
 //               new Option()
 //               {
 //                   Text = "辺りを見渡す",
 //                   Action = actions.LookAround
 //               },
 //               new Option()
 //               {
 //                   Text = "鍵を拾う",
 //                   Action = actions.TakeKey,
 //                   IsFlagOK = (() =>
 //                   {
 //                       if (gc.IsCheckedKey) return true;
 //                       else return false;
 //                   })
 //               },
 //               new Option()
 //               {
 //                   Text = "扉を開ける",
 //                   Action = actions.OpenDoor,
 //               }
 //           },
 //       };
 //       Scenarios.Add(scenario02);

 //       var scenario03 = new Scenario()
 //       {
 //           ScenarioID = "scenario03",
 //           Texts = new List<string>()
 //           {
 //               "鍵がかかっていて開かない",
 //           },
 //           NextScenarioID = "scenario02"
 //       };
 //       Scenarios.Add(scenario03);

 //       var scenario04 = new Scenario()
 //       {
 //           ScenarioID = "scenario04",
 //           Texts = new List<string>()
 //           {
 //               "鍵を使って扉を開いた",
 //               "クリア！"
 //           },
 //           //NextScenarioID = "scenario02"
 //       };
 //       Scenarios.Add(scenario04);

 //       var scenario05 = new Scenario()
 //       {
 //           ScenarioID = "scenario05",
 //           Texts = new List<string>()
 //           {
 //               "足元に鍵が落ちている",
 //           },
 //           NextScenarioID = "scenario02"
 //       };
 //       Scenarios.Add(scenario05);


 //       var scenario06 = new Scenario()
 //       {
 //           ScenarioID = "scenario06",
 //           Texts = new List<string>()
 //           {
 //               "鍵を拾った",
 //           },
 //           NextScenarioID = "scenario02"
 //       };
 //       Scenarios.Add(scenario06);


        //var scenario3 = new Scenario();
        //scenario3.ScenarioID = "options01";
        //if (!gc.Items.Contains("Key"))
        //{
        //    scenario3.Texts.Add("足元に鍵が落ちている");
        //    gc.IsCheckedKey = true;
        //}
        //else
        //{
        //    scenario3.Texts.Add("足元には何もない");
        //}
        //Scenarios.Add(scenario3);

        //var scenario4 = new Scenario();
        //scenario4.ScenarioID = "options02";
        //if (gc.Items.Contains("Key"))
        //{
        //    scenario4.Texts.Add("鍵を使って扉を開いた");
        //    scenario4.Texts.Add("クリアー！");
        //}
        //else
        //{
        //    scenario4.Texts.Add("鍵がかかっていて開かない");
        //    scenario4.NextScenarioID = "scenario02";
        //}

    //}

}