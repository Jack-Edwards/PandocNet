﻿using VerifyNUnit;
using NUnit.Framework;
using Pandoc;

[TestFixture]
public class Tests
{
    [Test]
    public async Task BinaryToText()
    {
        var result = await PandocInstance.Convert<DocxIn, HtmlOut>("sample.docx", "output.html");

        await Verifier.VerifyFile("output.html")
            .AppendValue("command", result.Command);
    }

    [Test]
    public async Task DataDirectory()
    {
        var result = await PandocInstance.Convert<CommonMarkIn, HtmlOut>(
            "sample.md", 
            "output.html",
            options: new()
            {
                DataDirectory = Environment.CurrentDirectory
            });

        await Verifier.VerifyFile("output.html")
            .AppendValue("command", result.Command);
    }

    [Test]
    public async Task Files()
    {
        var result = await PandocInstance.Convert<CommonMarkIn, HtmlOut>("sample.md", "output.html");

        await Verifier.VerifyFile("output.html")
            .AppendValue("command", result.Command);
    }

    [Test]
    public async Task Streams()
    {
        var result = await PandocInstance.Convert<CommonMarkIn, HtmlOut>("sample.md", "output.html");

        await Verifier.VerifyFile("output.html")
            .AppendValue("command", result.Command);
    }
    [Test]
    public async Task InputOutput()
    {
        Pandoc.Result result;

        {
            await using var inStream = File.OpenRead("sample.md");
            await using var outStream = File.OpenWrite("output.html");
            result = await PandocInstance.Convert<CommonMarkIn, HtmlOut>(inStream, outStream);
        }
        
        await Verifier.VerifyFile("output.html")
            .AppendValue("command", result.Command);
    }

    [Test]
    public async Task Text()
    {
        var (command, value) = await PandocInstance.ConvertToText<CommonMarkIn, HtmlOut>("*text*");

        await Verifier.Verify(value).UseExtension("html")
            .AppendValue("command", command);
    }

    [Test]
    public async Task CustomOptions()
    {
        var (command, value) = await PandocInstance.ConvertToText(
            @"# Heading1",
            new CommonMarkIn
            {
                ShiftHeadingLevelBy = 2
            },
            new HtmlOut
            {
                NumberOffsets = new[] {3}
            });

        await Verifier.Verify(value).UseExtension("html")
            .AppendValue("command", command);
    }
}