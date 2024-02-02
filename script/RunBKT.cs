
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class RunBKT : MonoBehaviour
{
    public void runBKTwithPython(int user_id, int correct)
    {
        string pythonPath = @"C:\Python27\python.exe";
        string scriptPath = System.IO.Path.Combine(Environment.CurrentDirectory, "../model/CoPillars-model/bkt.py");
        UnityEngine.Debug.Log("LAAAAAAAAAAAAAAAAAAAAAAAAAAA" + scriptPath);

        // Chemin vers le script Python à exécuter
        // string scriptPath = @"chemin\vers\votre_script.py";

        // Vérifiez si les fichiers existent avant de les exécuter

        if ( System.IO.File.Exists(scriptPath))
        {
            // Créer le processus pour exécuter le script Python
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = $"{scriptPath} {user_id} {correct}";
            startInfo.FileName = pythonPath;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            // Démarrer le processus
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            // Lire la sortie standard du processus (si nécessaire)
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(); // Attendre la fin de l'exécution du processus
            UnityEngine.Debug.Log("Sortie de Python : " + output);
        }
        else
        {
            UnityEngine.Debug.LogError("Les fichiers spécifiés n'existent pas.");
        }
    }
}

