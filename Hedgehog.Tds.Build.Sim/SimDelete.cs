using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;


namespace Hedgehog.Tds.Build.Sim
{
    public class SimDelete : Task
    {
        [Required]
        public string SimPath { get; set; }
        [Required]
        public string InstanceName { get; set; }
        [Required]
        public string InstanceDirectory { get; set; }
        [Required]
        public string ConnectionString { get; set; }

        public override bool Execute()
        {
            try
            {
                
                Log.LogMessage("Sitecore SIM Delete Start");

                InstanceName = Utils.CleanName(InstanceName);
                InstanceDirectory = Utils.CleanPath(InstanceDirectory, InstanceName);


                FileInfo info = new FileInfo(SimPath);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = info.FullName;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.WorkingDirectory = info.DirectoryName;
                startInfo.Arguments = string.Format("delete \"InstanceName:{0}\" \"InstanceDirectory:{1}\" \"ConnectionString:{2}\" ",
                        InstanceName,
                        InstanceDirectory,
                        ConnectionString);

                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
              


                Log.LogMessage("{0} {1}", startInfo.FileName, startInfo.Arguments);

                using (Process exeProcess = Process.Start(startInfo))
                {
                    Log.LogMessage(exeProcess.StandardOutput.ReadToEnd());
                    Log.LogMessage(exeProcess.StandardError.ReadToEnd());
                    exeProcess.WaitForExit();
                }

                Log.LogMessage("Sitecore SIM Delete Finished");

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
