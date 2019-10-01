using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Option
{
    public string ID;
    public string Text;
    public Action Action;
    public string NextScenarioID;
    public Func<bool> IsFlagOK = () => { return true; };

    public void A()
    {

    }

}