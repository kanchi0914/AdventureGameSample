using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Scenario
{
    public string ScenarioID;
    public List<Section> sections;
    public string Speaker;
    public List<string> Texts = new List<string>();
    public List<Option> Options = new List<Option>();
    public string NextScenarioID;

    public class Section
    {
        public string Speaker;
        public List<string> ChalactorImages;
        public string Text;
        public Action Action;
    }


    public void Init()
    {
        //InitialAction = LookAround;
    }
}
