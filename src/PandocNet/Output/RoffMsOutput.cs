﻿namespace PandocNet;

public class RoffMsOutput :
    OutputOptions
{
    public RoffMsOutput(Stream stream) :
        base(stream)
    {
    }

    public RoffMsOutput(string file) :
        base(file)
    {
    }

    public override string Format => "ms";
    //https://pandoc.org/MANUAL.html#options-affecting-specific-writers
    public bool Ascii { get; set; }
    public bool NumberSections { get; set; }

    public override IEnumerable<string> GetArguments()
    {
        foreach (var argument in base.GetArguments())
        {
            yield return argument;
        }

        if (Ascii)
        {
            yield return "--ascii";
        }

        if (NumberSections)
        {
            yield return "--number-sections";
        }
    }
}