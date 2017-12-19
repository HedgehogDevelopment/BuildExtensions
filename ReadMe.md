<img src="https://www.hhog.com/-/media/PublicImages/Hedgehog/Hedgehog-logo-4color-275x46.jpg" alt="Hedgehog Development" border="0"> 


================================

# Hedgehog Development Build Extensions #

This repository contains a set of projects that are designed to help you create automated deployments. These tools are designed to work with [Team Development for Sitecore](http://www.hhogdev.com/products/team-development-for-sitecore/overview.aspx) and MS Build.

The repository contains the following tools:

* [Hedgehog.Build.Razl](https://github.com/HedgehogDevelopment/BuildExtensions/tree/master/Hedgehog.Build.Razl) - MSBuild tasks used to call [Razl](http://www.razl.net) scripts from an MS Build process.
* [Hedgehog.Build.Ship](https://github.com/HedgehogDevelopment/BuildExtensions/tree/master/Hedgehog.Build.Ship) - MSBuild tasks used to call [Sitecore Ship](https://github.com/kevinobee/Sitecore.Ship) to install Sitecore update packages and perform publishes.
* [Hedgehog.Tds.Build.Sim](https://github.com/HedgehogDevelopment/BuildExtensions/tree/master/Hedgehog.Tds.Build.Sim) - MSBuild tasks used to call [Sitecore SIM](https://marketplace.sitecore.net/en/Modules/Sitecore_Instance_Manager.aspx) commands to install and delete Sitecore instances. It is used in conjunction with [Hedgehog.Tds.Build.Sim.Console](/Todo).
* [Hedgehog.Tds.Build.Sim.Console](https://github.com/HedgehogDevelopment/BuildExtensions/tree/master/Hedgehog.Tds.Build.Sim.Console) - Console application used to run [Sitecore SIM](https://marketplace.sitecore.net/en/Modules/Sitecore_Instance_Manager.aspx) processes.


## Example Build File ##

	<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
		<UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Sim\Hedgehog.Tds.Build.Sim.dll" TaskName="Hedgehog.Tds.Build.Sim.SimInstall"/>
		<UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Sim\Hedgehog.Tds.Build.Sim.dll" TaskName="Hedgehog.Tds.Build.Sim.SimDelete"/>
		<UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Ship\Hedgehog.Build.Ship.dll" TaskName="Hedgehog.Build.Ship.Publish"/>
		<UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Ship\Hedgehog.Build.Ship.dll" TaskName="Hedgehog.Build.Ship.InstallPackage"/>
		<UsingTask AssemblyFile="$(MSBuildExtensionsPath)\HedgehogDevelopment\Razl\Hedgehog.Build.Razl.dll" TaskName="Hedgehog.Build.Razl.RazlScript"/>


		<Target Name="SitecorePreBuild"   >

			<SimDelete  Condition="Exists('$(SitecoreDeployFolder)') and '$(Configuration)' != 'Debug' and '$(Configuration)' != 'Live' "
				SimPath="C:\Tools\SIM\Hedgehog.Tds.Build.Sim.Console.exe"
				InstanceName="$(SitecoreWebUrl)"
				InstanceDirectory="$(SitecoreDeployFolder)"
				ConnectionString="Data Source=.;User ID=sitecore;Password=sitecore123"
		/>
			<SimInstall Condition="!Exists('$(SitecoreDeployFolder)') and '$(Configuration)' != 'Live' "
				SimPath="C:\Tools\SIM\Hedgehog.Tds.Build.Sim.Console.exe"
				InstanceName="$(SitecoreWebUrl)"
				InstanceDirectory="$(SitecoreDeployFolder)"
				RepoDirectory="c:\sitecorerepo"
				RepoFile="Sitecore 7.1 rev. 130926.zip"
				ConnectionString="Data Source=.;User ID=sitecore;Password=sitecore123"
				AppPoolIdentity="NetworkService"
				LicencePath="C:\Sitecore\license.xml"
		/>
		</Target>
		<Target Name="AfterGeneratePackage">
			<InstallPackage Condition="'$(Configuration)' == 'Live'"
							HostName="http://live.demo"
							FilePath="$(teamcity_build_workingDir)\Hedgehog\Hedgehog.Master\bin\Package_Live\Hedgehog.Master.update"
						/>
			<Publish Condition="'$(Configuration)' == 'Live'"
					 HostName="http://live.demo"
					 Mode="incremental"
					 Source="master"
					 Targets="web"
					 Languages="en" />

		</Target>
		<Target Name="AfterSitecoreBuild">
			<RazlScript  Condition="'$(Configuration)' != 'Debug' and '$(Configuration)' != 'Live' "
						 RazlPath="C:\Program Files (x86)\Hedgehog Development\Razl\Razl.exe"
						 FilePath="$(teamcity_build_workingDir)\Hedgehog\Deploy\Razl\Staging.xml"
						 Parameters="path=$(SitecoreDeployFolder);url=$(SitecoreWebUrl)"/>
		</Target>
	</Project>

## Resources ##

You can download the demo website that uses these tools from [BuildExtenstionDemo](https://github.com/HedgehogDevelopment/BuildExtensionsDemo).

Slides from presentation: [https://www.slideshare.net/mikeedwards83/sitecore-nirvana-continous-delivery](https://www.slideshare.net/mikeedwards83/sitecore-nirvana-continous-delivery)

Video about build extensions: [http://www.youtube.com/watch?v=MLMHMkjMZ1w](http://www.youtube.com/watch?v=MLMHMkjMZ1w)

## License ##

Distributed using the [Apache 2 Licence](https://github.com/HedgehogDevelopment/BuildExtensions/blob/master/Licence.txt)


