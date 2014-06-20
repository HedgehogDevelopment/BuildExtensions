using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace Hedgehog.Build.Ship
{
    public class InstallPackage : Task
    {
        private const string Path = "/services/package/install/fileupload";

        [Required]
        public string HostName { get; set; }

        [Required]
        public string FilePath { get; set; }

        public int Timeout { get; set; }

        public int PackageId { get; set;}

        public string Description { get; set; }


        public override bool Execute()
        {
            var result = false;

            Log.LogMessage("Sitecore Ship Upload Start: {0}", FilePath);

            if (File.Exists(FilePath))
            {
                try
                {
                   
                    HostName = HostName.TrimEnd('/');
                    string absolutUrl = string.Format("{0}{1}", HostName, Path);

                    Log.LogMessage("Sitecore Ship Upload: Pushing to {0}", absolutUrl);


                    if (PackageId > 0 && !string.IsNullOrEmpty(Description))
                    {
                        //this is hack for now
                        absolutUrl = absolutUrl +
                                     string.Format("?packageId={0}&description={1}", PackageId, Description);
                    }

                    Log.LogWarning("Sitecore Ship Upload Finished to {0}, timeout {1}", absolutUrl, Timeout == 0 ? 100 : Timeout);

                    WebClientEx client = new WebClientEx();
                    client.Timeout = Timeout == 0 ? 100 : Timeout;
                    client.UploadFile(absolutUrl, "POST", FilePath);
                    result = true;

                    Log.LogMessage("Sitecore Ship Upload Finished: {0}", FilePath);

                }
                catch (Exception ex)
                {
                    Log.LogErrorFromException(ex, true);
                }
            }
            else
            {
                Log.LogError(string.Format("Sitecore Ship Upload Failed: Failed to find file {0}", FilePath));
               
            }

            return result;


        }
    }
}
