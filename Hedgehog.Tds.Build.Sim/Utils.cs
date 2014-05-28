using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedgehog.Tds.Build.Sim
{
    public class Utils
    {

        public static string CleanName(string name)
        {
            if (name.StartsWith("http://"))
            {
                name = name.Substring(7);
            }
            if (name.StartsWith("https://"))
            {
                name = name.Substring(8);
            }

            return name.Trim('/');
        }

        public static string CleanPath(string path, string name)
        {
            if (path.EndsWith(string.Format("{0}\\website", name)) 
                || path.EndsWith(string.Format("{0}\\website\\", name)))
            {
                return path.Split(new []{string.Format("\\{0}\\", name)},StringSplitOptions.RemoveEmptyEntries)[0];
            }

            return path;
        }
    }
}
