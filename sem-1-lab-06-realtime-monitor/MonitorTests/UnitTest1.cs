using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using System;
using System.IO;

public class MonitorTests
{
    [Test]
    public void RenderGraph_WithTwentyMeasurements_PrintsThreeGraphs()
    {
        var originalOutput = Console.Out;
        using var output = new StringWriter();

        Program.cpu.Clear();
        Program.ram.Clear();
        Program.temperature.Clear();

        try
        {
            for (int i = 0; i < 20; i++)
            {
                Program.SaveData(50, 40, 60);
            }

            Console.SetOut(output);

            Program.RenderGraph();

            string result = output.ToString();

            Assert.That(result, Does.Contain("CPU GRAPH"));
            Assert.That(result, Does.Contain("RAM GRAPH"));
            Assert.That(result, Does.Contain("TEMEPRATURE GRAPH"));
            Assert.That(result, Does.Not.Contain("Waiting for data"));
        }
        finally
        {
            Console.SetOut(originalOutput);

            Program.cpu.Clear();
            Program.ram.Clear();
            Program.temperature.Clear();
        }
    }
}