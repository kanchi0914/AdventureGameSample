using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class Scene
{
    public string ID;
    private ScenarioStream ss;
    private int index = 0;
    public List<string> Lines = new List<string>();
    Actions actions;

    public Scene(ScenarioStream ss)
    {
        this.ss = ss;
        actions = ss.Actions;
    }

    public Scene Clone()
    {
        return new Scene(ss)
        {
            ID = this.ID,
            index = 0,
            Lines = new List<string>(Lines),
            actions = this.actions
        };
    }

    public void SendLines()
    {
        if (index >= Lines.Count) return;
        var line = Lines[index];
        var text = "";

        while (true)
        {
            if (line.Contains("#"))
            {
                line = line.Replace("#", "");
                if (line.Contains("speaker"))
                {
                    line = line.Replace("speaker=", "");
                    ss.SetSpeaker(line);
                }
                else if (line.Contains("chara"))
                {
                    line = line.Replace("chara=", "");
                    ss.SetCharactor(line);
                }
                else if (line.Contains("image"))
                {
                    line = line.Replace("image_", "");
                    var splitted = line.Split('=');
                    ss.SetImage(splitted[1]);
                }
                else if (line.Contains("next"))
                {
                    line = line.Replace("next=", "");
                    ss.SetScene(line);
                }
                else if (line.Contains("method"))
                {
                    line = line.Replace("method=", "");
                    var type = actions.GetType();
                    MethodInfo mi = type.GetMethod(line);
                    mi.Invoke(actions, new object[] { });
                }
                else if (line.Contains("options"))
                {
                    var options = new List<(string, string)>();
                    while (true)
                    {
                        index++;
                        line = line = Lines[index];
                        if (line.Contains("{"))
                        {
                            line = line.Replace("{", "").Replace("}", "");
                            var splitted = line.Split(',');
                            options.Add((splitted[0], splitted[1]));
                        }
                        else
                        {
                            ss.SetOptions(options);
                            break;
                        }
                    }
                }
                index++;
                if (index >= Lines.Count) break ;
                line = Lines[index];
            }
            else
            {
                break;
            }
        }
        if (line.Contains('{'))
        {
            line = line.Replace("{", "");
            while (true)
            {
                if (line.Contains('}'))
                {
                    line = line.Replace("}", "");
                    text += line;
                    index++;
                    break;
                }
                else
                {
                    text += line;
                }
                index++;
                if (index >= Lines.Count) break;
                line = Lines[index];
            }
        }

        if (!string.IsNullOrEmpty(text)) ss.SetText(text);

    }
}