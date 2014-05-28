using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedgehog.Tds.Build.Sim.Console
{
    public class BaseArgsProcessor
    {
        private readonly IEnumerable<string> _args;

        public BaseArgsProcessor(IEnumerable<string> args )
        {
            _args = args;
        }

        protected string GetArg(string key)
        {
            string arg = _args.FirstOrDefault(x => x.ToLowerInvariant().StartsWith(key.ToLowerInvariant()));
            if (arg == null)
            {
                return string.Empty;
            }

            return arg.Substring(key.Length + 1);
        }
    }
}
