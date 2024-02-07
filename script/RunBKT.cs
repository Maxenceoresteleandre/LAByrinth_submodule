
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class RunBKT : MonoBehaviour
{
    public string pythonPath = "cf WherePython.cs dans scripts";

    /// Exécute le script Python bkt.py et retourne un float représentant la probabilité de réussir un niveau avec skill_id.
    /// </summary>
    /// <param name="user_id">L'ID de l'utilisateur.</param>
    /// <param name="skill_id">L'ID de la compétence.</param>
    /// <param name="correct">Indique si le niveau est reussi (1) ou non (0).</param>
    /// <returns>La probabilité de success, ou 0 si une erreur se produit.</returns>
    public void Start()
    {
        pythonPath = WherePython.pythonPath;
        // runBKT_partial_fit(1, 1, 1);
        // runBKT_p_success(1, 1, 1);
    }

    public float runBKT_p_success(int user_id, int skill_id, int correct)
    {
        // Chemin vers le script Python à exécuter
        // string scriptPath = @"chemin\vers\votre_script.py";
        string scriptPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Assets/demoScene/LAByrinth/script/CoPillars-model/bkt.py");
        string fileToReadPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Assets/demoScene/LAByrinth/script/CoPillars-model/all_data.csv");
        string fileToLoadPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Assets/demoScene/LAByrinth/script/CoPillars-model/model.pkl");

        // UnityEngine.Debug.Log("LAAAAAAAAAAAAAAAAAAAAAAAAAAA" + scriptPath);

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
            UnityEngine.Debug.Log("Sortie de Python p_success : " + output);
            
            return float.Parse(RemplacerPointParVirgule(output));
        }
        else
        {
            UnityEngine.Debug.LogError("Les fichiers spécifiés n'existent pas.");
        }
        return 0f;
    }

    /// <summary>
    /// partial fit le model avec un nouveau vecteur (user_id, skill_id, correct)
    /// </summary>
    /// <param name="user_id">L'ID de l'utilisateur.</param>
    /// <param name="skill_id">L'ID de la compétence.</param>
    /// <param name="correct">Indique si le niveau est reussi (1) ou non (0).</param>
    public void runBKT_partial_fit(int user_id, int skill_id, int correct)
    {
        
        // Chemin vers le script Python à exécuter
        // string scriptPath = @"chemin\vers\votre_script.py";
        string scriptPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Assets/demoScene/LAByrinth/script/CoPillars-model/partial_fit.py");
        // string fileToReadPath = System.IO.Path.Combine(Environment.CurrentDirectory, "../model/CoPillars-model/data/all_data.csv");
        string fileToLoadPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Assets/demoScene/LAByrinth/script/CoPillars-model/model.pkl");

        // UnityEngine.Debug.Log("LAAAAAAAAAAAAAAAAAAAAAAAAAAA" + scriptPath);

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

            process.Exited += (sender, e) =>
            {
                // Cette méthode sera appelée lorsque le processus se termine
                UnityEngine.Debug.Log("Le processus Python s'est terminé.");
            };
            
            process.Start();


            // Lire la sortie standard du processus (si nécessaire)
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(); // Attendre la fin de l'exécution du processus
            UnityEngine.Debug.Log("Sortie de Partial Fit : " + output);
        }
        else
        {
            UnityEngine.Debug.LogError("Les fichiers spécifiés n'existent pas.");
        }
    }

    //pour remplacer le point du output en virgule
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
}

