﻿namespace PandocNet;

public class TeiOutput :
    OutputOptions
{
    public TeiOutput(Stream stream) :
        base(stream)
    {
    }

    public TeiOutput(string file) :
        base(file)
    {
    }

    public override string Format => "tei";  
    //https://pandoc.org/MANUAL.html#options-affecting-specific-writers
    public TopLevelDivision? TopLevelDivision { get; set; }
    public bool NumberSections { get; set; }

    public override IEnumerable<string> GetArguments()
    {
        foreach (var argument in base.GetArguments())
        {
            yield return argument;
        }

        if (TopLevelDivision != null)
        {
            yield return $"--top-level-division={TopLevelDivision}";
        }
        if (NumberSections)
        {
            yield return "--number-sections";
        }
    }
}