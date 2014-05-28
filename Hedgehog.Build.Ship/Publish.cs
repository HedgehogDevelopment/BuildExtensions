using System;
using System.Net;
using System.Web;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Hedgehog.Build.Ship
{
    public class Publish : Task
    {

        private const string Path = "/services/publish/{0}";

        [Required]
        public string HostName { get; set; }

        [Required]
        public string Mode { get; set; }

        [Required]
        public string Languages { get; set; }

        [Required]
        public string Source { get; set; }

        [Required]
        public string Targets { get; set; }



        public override bool Execute()
        {
            var result = false;

            try
            {
                Log.LogMessage("Sitecore Ship Publish Start: {0} | {1} | {2} | {3} | {4}", HostName, Mode, Source, Targets, Languages);

                 HostName = HostName.TrimEnd('/');
                string absolutUrl = string.Format("{0}{1}", HostName, string.Format(Path, Mode));

                string formData = string.Format("source={0}&targets={1}&languages={2}",
                    HttpUtility.UrlEncode(Source),
                    HttpUtility.UrlEncode(Targets),
                    HttpUtility.UrlEncode(Languages)
                    );

                WebClient client = new WebClient();
                client.UploadString(absolutUrl, "POST", formData);

                result = true;

                Log.LogMessage("Sitecore Ship Publish Finished: {0} | {1} | {2} | {3} | {4}", HostName, Mode, Source, Targets, Languages);

            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex, true);
            }

            return result;
        }
    }
}
