using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedgehog.Tds.Build.Sim.Console
{
    public class DeleteArgs : BaseArgsProcessor
    {
        public string InstanceName
        {
            get { return GetArg("InstanceName"); }
        }

        public string InstanceDirectory
        {
            get { return GetArg("InstanceDirectory"); }
        }

        public string ConnectionString
        {
            get { return GetArg("ConnectionString"); }
        }

        public DeleteArgs(IEnumerable<string> args)
            : base(args)
        {
        }
    }
}
