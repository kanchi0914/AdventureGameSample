using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class Actions
{
    GameController gc;
    
    public Actions(GameController gc)
    {
        this.gc = gc;
    }

    //public void LookAround()
    //{
    //    gc.ScenarioController.SetScenario("scenario05");
    //    gc.IsCheckedKey = true;
    //    gc.Character01.Appear();
    //}

    //public void OpenDoor()
    //{
    //    gc.Character01.SetImage("aseri");
    //    var scenario = new Scenario();
    //    if (gc.Items.Contains("Key"))
    //    {
    //        gc.ScenarioController.SetScenario("scenario04");
    //    }
    //    else
    //    {
    //        gc.ScenarioController.SetScenario("scenario03");
    //    }

    //}

    //public void TakeKey()
    //{
    //    gc.Character01.SetImage("smile");
    //    //var scenario = new Scenario();
    //    //scenario.Texts.Add("鍵を拾った");
    //    //scenario.NextScenarioID = "scenario02";
    //    ////gc.ISetScenario(scenario);
    //    gc.Items.Add("Key");
    //    gc.ScenarioController.SetScenario("scenario06");
    //}
}
