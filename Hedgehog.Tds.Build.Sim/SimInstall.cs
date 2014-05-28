using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Hedgehog.Tds.Build.Sim
{
    public class SimInstall : Task
    {
        [Required]
        public string SimPath { get; set; }
        [Required]
        public string InstanceName { get; set; }
        [Required]
        public string InstanceDirectory { get; set; }
        [Required]
        public string RepoDirectory { get; set; }
        [Required]
        public string RepoFile { get; set; }
        [Required]
        public string ConnectionString { get; set; }
        [Required]
        public string AppPoolIdentity { get; set; }
        [Required]
        public string LicencePath { get; set; }

        public string Modules { get; set; }

        public override bool Execute()
        {
            try
            {

                Log.LogMessage("Sitecore SIM Install Start");

                InstanceName = Utils.CleanName(InstanceName);
                InstanceDirectory = Utils.CleanPath(InstanceDirectory, InstanceName);


                FileInfo info = new FileInfo(SimPath);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = info.FullName;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.WorkingDirectory = info.DirectoryName;
                startInfo.Arguments = string.Format("install \"InstanceName:{0}\" \"InstanceDirectory:{1}\" \"RepoDirectory:{2}\" \"RepoFile:{3}\" \"ConnectionString:{4}\" \"AppPoolIdentity:{5}\" \"LicencePath:{6}\" \"Modules:{7}\" ",
                        InstanceName,
                        InstanceDirectory,
                        RepoDirectory,
                        RepoFile,
                        ConnectionString,
                        AppPoolIdentity,
                        LicencePath,
                        Modules);

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

                Log.LogMessage("Sitecore SIM Install Finished");

                return true;
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex, true);
                return false;
            }

        }
    }
}
