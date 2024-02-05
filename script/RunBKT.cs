
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class RunBKT : MonoBehaviour
{
    public float runBKT_p_success(int user_id, int skill_id, int correct)
    {
        string pythonPath = @"C:\Users\ameli\AppData\Local\Microsoft\WindowsApps\python.exe";
        string scriptPath = System.IO.Path.Combine(Environment.CurrentDirectory, "../model/CoPillars-model/bkt.py");
        string fileToReadPath = System.IO.Path.Combine(Environment.CurrentDirectory, "../model/CoPillars-model/data/all_data.csv");
        string fileToLoadPath = System.IO.Path.Combine(Environment.CurrentDirectory, "../model/CoPillars-model/model.pkl");

        UnityEngine.Debug.Log("LAAAAAAAAAAAAAAAAAAAAAAAAAAA" + scriptPath);

        // Chemin vers le script Python à exécuter
        // string scriptPath = @"chemin\vers\votre_script.py";

        // Vérifiez si les fichiers existent avant de les exécuter

        if ( System.IO.File.Exists(scriptPath))
        {
            // Créer le processus pour exécuter le script Python
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = $"{scriptPath} {user_id} {skill_id} {correct} {fileToReadPath} {fileToLoadPath}";
            // startInfo.Arguments = scriptPath;
            startInfo.FileName = pythonPath;
            startInfo.UseShellExecute = false;
            // startInfo.Verb = "runas";
            startInfo.RedirectStandardOutput = true;

            // Démarrer le processus
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            // process.Start();

            // process.Exited += (sender, e) =>
            // {
            //     // Cette méthode sera appelée lorsque le processus se termine
            //     UnityEngine.Debug.Log("Le processus Python s'est terminé.");
            // };


            // Lire la sortie standard du processus (si nécessaire)
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(); // Attendre la fin de l'exécution du processus
            UnityEngine.Debug.Log("Sortie de Python : " + output);
            
            return float.Parse(RemplacerPointParVirgule(output));
        }
        else
        {
            UnityEngine.Debug.LogError("Les fichiers spécifiés n'existent pas.");
        }
        return 0f;
    }

    static string RemplacerPointParVirgule(string chaine)
    {
        char[] caracteres = chaine.ToCharArray();

        for (int i = 0; i < caracteres.Length; i++)
        {
            if (caracteres[i] == '.')
            {
                caracteres[i] = ',';
            }
        }

        return new string(caracteres);
    }
    public void runBKT_partial_fit(int user_id, int skill_id, int correct)
    {
        string pythonPath = @"C:\Users\ameli\AppData\Local\Microsoft\WindowsApps\python.exe";
        string scriptPath = System.IO.Path.Combine(Environment.CurrentDirectory, "../model/CoPillars-model/partial_fit.py");
        // string fileToReadPath = System.IO.Path.Combine(Environment.CurrentDirectory, "../model/CoPillars-model/data/all_data.csv");
        string fileToLoadPath = System.IO.Path.Combine(Environment.CurrentDirectory, "../model/CoPillars-model/model.pkl");

        UnityEngine.Debug.Log("LAAAAAAAAAAAAAAAAAAAAAAAAAAA" + scriptPath);

        // Chemin vers le script Python à exécuter
        // string scriptPath = @"chemin\vers\votre_script.py";

        // Vérifiez si les fichiers existent avant de les exécuter

        if ( System.IO.File.Exists(scriptPath))
        {
            // Créer le processus pour exécuter le script Python
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = $"{scriptPath} {user_id} {skill_id} {correct} {fileToLoadPath}";
            // startInfo.Arguments = scriptPath;
            startInfo.FileName = pythonPath;
            startInfo.UseShellExecute = false;
            // startInfo.Verb = "runas";
            startInfo.RedirectStandardOutput = true;

            // Démarrer le processus
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            // process.Start();

            // process.Exited += (sender, e) =>
            // {
            //     // Cette méthode sera appelée lorsque le processus se termine
            //     UnityEngine.Debug.Log("Le processus Python s'est terminé.");
            // };


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

