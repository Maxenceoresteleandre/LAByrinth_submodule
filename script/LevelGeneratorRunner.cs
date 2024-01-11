using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

public class LevelGeneratorRunner : MonoBehaviour
{
    public int n_cols = 4;
    public int n_rows = 4;
    public int n_walls = 2;
    public int n_hex = 3;
    public int n_colors = 2;
    public List<int> n_square_by_color = new List<int>(){2, 1};
    public List<int> n_sun_by_color = new List<int>(){2, 1};

    public string level_path = "/demoScene/LAByrinth/LevelGenerator/level.json";

    public string solution_path = "/demoScene/LAByrinth/LevelGenerator/solution.csv";


    void Start()
    {
        string exePath = Application.dataPath + "/demoScene/LAByrinth/LevelGenerator/LevelGeneratorConsole.exe";

        string[] arguments = new string[]{
            n_cols.ToString(),
            n_rows.ToString(),
            n_walls.ToString(),
            n_hex.ToString(),
            n_colors.ToString(),
            string.Join(",", n_square_by_color),
            string.Join(",", n_sun_by_color),
            level_path,
            solution_path
        };

        // Run the process
        UnityEngine.Debug.Log("run process");
        RunProcess(exePath, arguments);
    }

    void RunProcess(string exePath, string[] arguments)
    {
        Process process = new Process();
        process.StartInfo.FileName = exePath;
        process.StartInfo.Arguments = string.Join(" ", arguments);
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();

        // Do something with the output if needed
        string output = process.StandardOutput.ReadToEnd();
        UnityEngine.Debug.Log(output);

        process.WaitForExit();
        process.Close();
        UnityEngine.Debug.Log("process throuroughly ran");
    }
}