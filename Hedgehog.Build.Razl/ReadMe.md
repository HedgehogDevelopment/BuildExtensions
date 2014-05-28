<img src="http://www.hhogdev.com/Images/newsletter/logo_hedgehog.jpg" alt="Hedgehog Development" width="203" height="65" border="0">
================================

# Hedgehog.Build.Razl #

This is an MSBuild extension designed to allow you to use [Razl](http://razl.net/) as part of your build process. 

## Installation ##

Follow these instructions to install the MSBuild tasks:

1. Download the latest code from GitHub
2. Compile the solution
3. Copy the assembly **Hedgehog.Build.Razl** to **C:\Program Files (x86)\MSBuild\HedgehogDevelopment\Razl**.  

## Tasks ##


The following tasks are exposed by the extension:

* RazlScript

It can be registered in your MS Build file with the following:

	<UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Razl\Hedgehog.Build.Razl.dll" TaskName="Hedgehog.Build.Razl.RazlScript"/>

### RazlScript ###

This task allows you to run a Razl script from you MSBuild process. It has the following parameters:

* RazlPath - The path to **Razl.exe** file.
* FilePath - The path to the Razl Script file to run, see Razl help for more information on creating a script file.
* Parameters - Any additional parameters to pass to Razl that should be used as part of the script. This parameter is optional. Parameters should be passed in the following format "p1=v1;p2=v2"

The following is an example configuration that can be used with [Team Development for Sitecore](http://www.hhogdev.com/products/team-development-for-sitecore/overview.aspx).

		<RazlScript  
 			 RazlPath="C:\Program Files (x86)\Hedgehog Development\Razl\Razl.exe"
			 FilePath="$(teamcity_build_workingDir)\Hedgehog\Deploy\Razl\Staging.xml" 
			 Parameters="path=$(SitecoreDeployFolder);url=$(SitecoreWebUrl)"/>



