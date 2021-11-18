﻿namespace PandocNet;

public class DocBook5Output :
    OutputOptions
{
    public DocBook5Output(Stream stream) :
        base(stream)
    {
    }

    public DocBook5Output(string file) :
        base(file)
    {
    }

    public override string Format => "docbook5";
    //https://pandoc.org/MANUAL.html#options-affecting-specific-writers
    public TopLevelDivision? TopLevelDivision { get; set; }
    public string? IdPrefix { get; set; }

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
        if (IdPrefix != null)
        {
            yield return $"--id-prefix={IdPrefix}";
        }
    }
}