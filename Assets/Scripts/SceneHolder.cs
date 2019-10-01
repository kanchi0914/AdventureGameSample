using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SceneHolder
{
    
    public List<Scene> Scenes = new List<Scene>();
    private ScenarioStream ss;

    public SceneHolder(ScenarioStream ss)
    {
        this.ss = ss;
        Load();
    }

    public void Load()
    {
        var itemFile = Resources.Load("ScenarioData/scenarios") as TextAsset; /*  Resouces/CSV下のCSV読み込み */
        var csvData = LoadCSV(itemFile);
        Scenes = Parse(csvData);
    }

    public List<string> LoadCSV(TextAsset file)
    {
        StringReader reader = new StringReader(file.text);
        var list = new List<string>();
        //reader.ReadLine();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            list.Add(line); // リストに入れる
        }
        return list;
    }

    public List<Scene> Parse(List<string> list)
    {
        var index = 0;
        var scenes = new List<Scene>();
        var scene = new Scene(ss);
        foreach (string line in list)
        {
            if (line.Contains("#scene"))
            {
                var ID = line.Replace("#scene=", "");
                scene = new Scene(ss);
                scene.ID = ID;
                scenes.Add(scene);
            }
            else
            {
                scene.Lines.Add(line);
            }
        }
        return scenes;
    }

}
