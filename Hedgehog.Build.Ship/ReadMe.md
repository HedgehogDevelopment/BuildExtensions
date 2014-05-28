<img src="http://www.hhogdev.com/Images/newsletter/logo_hedgehog.jpg" alt="Hedgehog Development" width="203" height="65" border="0">
================================

# Hedgehog.Build.Ship #

This is an MSBuild extension designed to allow you to use [Sitecore Ship](https://github.com/kevinobee/Sitecore.Ship) as part of your build process. 

## Installation ##

Follow these instructions to install the MSBuild tasks:

1. Download the latest code from GitHub
2. Compile the solution
3. Copy the assembly **Hedgehog.Build.Ship** to **C:\Program Files (x86)\MSBuild\HedgehogDevelopment\Ship**.  

## Tasks ##

The following tasks are exposed by the extension:

* InstallPackage
* Publish

They can be registered in your MS Build file with the following:
    
	<UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Ship\Hedgehog.Build.Ship.dll" TaskName="Hedgehog.Build.Ship.Publish"/>
	<UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Ship\Hedgehog.Build.Ship.dll" TaskName="Hedgehog.Build.Ship.InstallPackage"/>

### InstallPackage ###

This task will send a Sitecore update package to a Sitecore Ship endpoint for installation. It has the following parameters:

* HostName - The host name of the target Sitecore instance, e.g. "http://live.sitecore.com"
* FilePath - The path to the Sitecore package  **.update** file to install.

The following is an example configuration that can be used with [Team Development for Sitecore](http://www.hhogdev.com/products/team-development-for-sitecore/overview.aspx).

    <InstallPackage 
		HostName="http://live.demo"
		FilePath="$(workingDir)\Hedgehog\Hedgehog.Master\bin\Package_Live\Hedgehog.Master.update" />

### Publish ###

This task will call a publish on the target Sitecore instance. It has the following parameters:

* HostName - The host name of the target Sitecore instance, e.g. "http://live.sitecore.com"
* Mode - The publishing mode, e.g. full. See Sitecore Ship documentation for possible values.
* Source - The name of the database to publish from, e.g. master
* Targets - The name of the database to publish to, e.g. web. Mulitple target databases can be specified as follows "web1,web2,web3".
* Languages - The name of the languages to publish, e.g. en. Multiple languages can be specified as follows: "en,fr,jp".

The following is an example configuration:

		<Publish 
			HostName="http://dev.demo"
			Mode="incremental"
			Source="master"
			Targets="web"
			Languages="en" />
			
## License ##

Distributed using the [Apache 2 Licence](https://github.com/HedgehogDevelopment/BuildExtensions/blob/master/Licence.txt)
