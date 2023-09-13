using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

public class MediaMetadataRemover
{
    public static void Main()
    {
        Console.Title = "MediaMetadataRemover | Made by https://github.com/GabryB03/";

        if (!Directory.Exists("inputs"))
        {
            Directory.CreateDirectory("inputs");
        }

        if (!Directory.Exists("outputs"))
        {
            Directory.CreateDirectory("outputs");
        }
        else
        {
            Directory.Delete("outputs", true);
            Directory.CreateDirectory("outputs");
        }

        foreach (string file in Directory.GetFiles("inputs"))
        {
            new Thread(() => RunFFMpeg($"-i \"{Path.GetFullPath(file)}\" -c copy -map_metadata -1 -map_chapters -1 -fflags +bitexact -flags:v +bitexact -flags:a +bitexact \"{Path.GetFullPath("outputs")}\\{Path.GetFileName(file)}\"")).Start();
        }
    }

    private static void RunFFMpeg(string arguments)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "ffmpeg.exe",
            Arguments = $"-threads {Environment.ProcessorCount} {arguments}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        }).WaitForExit();
    }
}