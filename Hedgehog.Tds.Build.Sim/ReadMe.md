<img src="http://www.hhogdev.com/Images/newsletter/logo_hedgehog.jpg" alt="Hedgehog Development" width="203" height="65" border="0">
================================

# Hedgehog.Tds.Build.Sim #

This is an MSBuild extension designed to allow you to use [Sitecore SIM](https://marketplace.sitecore.net/en/Modules/Sitecore_Instance_Manager.aspx) as part of your build process. It must be used in conjunction with the **Hedgehog.Tds.Build.Sim.Console** project.

## Installation ##

Follow these instructions to install the MSBuild tasks:

1. Download the latest code from GitHub
2. Compile the solution
3. Copy the assembly **Hedgehog.Tds.Build.Sim** to **C:\Program Files (x86)\MSBuild\HedgehogDevelopment\Sim**.  

## Tasks ##

The following tasks are exposed by the extension:

* SimInstall
* SimDelete

They can be registered in your MS Build file with the following:
    
     <UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Sim\Hedgehog.Tds.Build.Sim.dll" TaskName="Hedgehog.Tds.Build.Sim.SimInstall"/>
	 <UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Sim\Hedgehog.Tds.Build.Sim.dll" TaskName="Hedgehog.Tds.Build.Sim.SimDelete"/>

### SimInstall ##

This task will install a Sitecore instance using **Sitecore SIM** and has the following parameters:

* SimPath - The path to the file **Hedgehog.Tds.Build.Sim.Console.exe**, this file is used to call **Sitecore SIM** itself.
* InstanceName - The name of the Sitecore instance to create, this can either be "mydomain.com" or "http://mydomain.com"
* InstanceDirectory - The path to the location where the Sitecore instance should be installed
* RepoDirectory - The path to the folder that contains the Sitecore CMS zip files
* RepoFile - The name of the Sitecore CMS zip file to install, e.g. "Sitecore 7.1 rev. 130926.zip"
* ConnectionString - The connection string to use for installing the Sitecore databases
* AppPoolIdentity - The identity to give the AppPool for example "Network Service"
* LicensePath - The path to the Sitecore license file, e.g. "c:\sitecore\license.xml"


The following is an example configuration that can be used with [Team Development for Sitecore](http://www.hhogdev.com/products/team-development-for-sitecore/overview.aspx).

		<SimInstall 
			SimPath="C:\Tools\SIM\Hedgehog.Tds.Build.Sim.Console.exe"
			InstanceName="$(SitecoreWebUrl)"
			InstanceDirectory="$(SitecoreDeployFolder)"
			RepoDirectory="c:\sitecorerepo"
			RepoFile="Sitecore 7.1 rev. 130926.zip"
			ConnectionString="Data Source=.;User ID=sitecore;Password=sitecore123"
			AppPoolIdentity="NetworkService"
			LicencePath="C:\Sitecore\license.xml"
		/>

### SimDelete ###

This task will delete a Sitecore instance using **Sitecore SIM** and has the following parameters:

* SimPath - The path to the file **Hedgehog.Tds.Build.Sim.Console.exe**, this file is used to call **Sitecore SIM** itself.
* InstanceName - The name of the Sitecore instance to remove, this can either be "mydomain.com" or "http://mydomain.com"
* InstanceDirectory - The path to the location where the Sitecore instance exists
* ConnectionString - The connection string to used to install the Sitecore databases

The following is an example configuration that can be used with [Team Development for Sitecore](http://www.hhogdev.com/products/team-development-for-sitecore/overview.aspx).

		<SimDelete  
			SimPath="C:\Tools\SIM\Hedgehog.Tds.Build.Sim.Console.exe"
			InstanceName="$(SitecoreWebUrl)"
			InstanceDirectory="$(SitecoreDeployFolder)"
			ConnectionString="Data Source=.;User ID=sitecore;Password=sitecore123"
		/>
		
## License ##

Distributed using the [Apache 2 Licence](https://github.com/HedgehogDevelopment/BuildExtensions/blob/master/Licence.txt)
