using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Hedgehog.Build.Razl
{
    public class RazlScript : Task
    {
        [Required]
        public string RazlPath { get; set; }

        [Required]
        public string FilePath { get; set; }

        public string Parameters { get; set; }

        public override bool Execute()
        {
            var result = false;

            if (File.Exists(FilePath))
            {
                try
                {
                    Log.LogMessage("Razl Script Started: {0}", FilePath);

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = RazlPath;
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.Arguments = string.Format("/script:{0}",FilePath);

                    if (!string.IsNullOrEmpty(Parameters))
                    {
                        startInfo.Arguments += string.Format(" /p:{0}", Parameters);
                    }

                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;

                    Log.LogMessage("{0} {1}", startInfo.FileName, startInfo.Arguments);

                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        Log.LogMessage(exeProcess.StandardOutput.ReadToEnd());
                        Log.LogMessage(exeProcess.StandardError.ReadToEnd());

                        exeProcess.OutputDataReceived += (sender, args) => Log.LogMessage(args.Data);
                        exeProcess.WaitForExit();
                    }

                    result = true;

                    Log.LogMessage("Razl Script Finished: {0}", FilePath);
                }
                catch (Exception ex)
                {
                    Log.LogErrorFromException(ex, true);

                }

            }
            else
            {
                Log.LogError(string.Format("Razl Script Failed: Failed to find file {0}", FilePath));

            }

            return result;
        }
    }
}
