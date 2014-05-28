using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIM.Instances;
using SIM.Pipelines;
using SIM.Products;

namespace Hedgehog.Tds.Build.Sim.Console
{
    public class InstallProcess
    {
        public bool Execute(InstallArgs args)
        {
            try
            {
                System.Console.WriteLine("SIM: Starting Install");

                string instanceRootDirectoryPath = args.InstanceDirectory + "\\" + args.InstanceName;


                Instance instance = InstanceManager.GetInstance(args.InstanceName);
                if (instance != null)
                {
                    System.Console.WriteLine("SIM: Warning! The instance with the same ({0}) name already exists",
                        args.InstanceName);
                }

                // Next lets check the rootDirectoryPath folder for uniqueness
                if (Directory.Exists(instanceRootDirectoryPath))
                {
                    System.Console.WriteLine("SIM: Warning! The folder {0} already exists", instanceRootDirectoryPath);
                }

                Product product;
                string repoFullPath = args.RepoDirectory + "\\" + args.RepoFile;

                var result = Product.TryParse(repoFullPath, out product);
                if (result == false)
                {
                    System.Console.WriteLine("SIM: Warning! Can't detect Sitecore-based product in {0} file", args.RepoFile);
                }

                var modules = new List<Product>();

                if (!string.IsNullOrEmpty(args.Modules))
                {
                    var modulesNames = args.Modules.Split('|');
                    foreach (var moduleName in modulesNames)
                    {
                        string modulePath = args.RepoDirectory + "\\" + moduleName;
                        Product module;
                        Product.TryParse(modulePath, out module);

                        if (module != null)
                        {
                            modules.Add(module);
                        }
                    }

                }

                SIM.Pipelines.Install.InstallArgs installArgs = new SIM.Pipelines.Install.InstallArgs(args.InstanceName,
                    args.InstanceName,
                    product,
                    instanceRootDirectoryPath,
                    new SqlConnectionStringBuilder(args.ConnectionString),
                    args.AppPoolIdentity,
                    args.AppPoolIdentity,
                    new FileInfo(args.LicencePath),
                    true,
                    false,
                    false,
                    modules);

                IPipelineController controller = new ConsoleController();

                // Lets start installation
                PipelineManager.Initialize();
                PipelineManager.StartPipeline(
                    "install",
                    installArgs,
                    controller,
                    false);

                System.Console.WriteLine("SIM: Finished Install");

                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Sitecore SIM install failed {0}", ex);
                return false;
            }

        }
    }
}
