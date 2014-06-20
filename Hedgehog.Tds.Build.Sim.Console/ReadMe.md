<img src="http://www.hhogdev.com/Images/newsletter/logo_hedgehog.jpg" alt="Hedgehog Development" width="203" height="65" border="0">
================================

# Hedgehog.Tds.Build.Sim.Console #

This is a console application that allows you to use [Sitecore SIM](https://marketplace.sitecore.net/en/Modules/Sitecore_Instance_Manager.aspx) from the command line.

## Installation ##

Follow these instructions to install the console application:

1. Download the latest code from GitHub
2. Compile the solution
3. Copy the assembly **Hedgehog.Tds.Build.Sim.Console** to the folder that contains **SIM.Tool.exe**.  


## Running the Application ##

The application can be run from the command line by passing different parameters depending on the task that needs to be performed.

### Install ###

A Sitecore instance can be installed using the **/install** switch. The task takes the following parameters:

* InstanceName - The name of the Sitecore instance to create, this can either be "mydomain.com" or "http://mydomain.com"
* InstanceDirectory - The path to the location where the Sitecore instance should be installed
* RepoDirectory - The path to the folder that contains the Sitecore CMS zip files
* RepoFile - The name of the Sitecore CMS zip file to install, e.g. "Sitecore 7.1 rev. 130926.zip"
* ConnectionString - The connection string to use for installing the Sitecore databases
* AppPoolIdentity - The identity to give the AppPool for example "Network Service"
* LicensePath - The path to the Sitecore license file, e.g. "c:\sitecore\license.xml"

The following is an example of calling install:

    Hedgehog.Tds.Build.Sim.Console.exe install "InstanceName:demo.tds" "InstanceDirectory:c:\sitecore" "RepoDirectory:c:\sitecorerepo" "RepoFile:Sitecore 7.1 rev. 130926.zip" "ConnectionString:Data Source=.;User ID=sitecore;Password=sitecore123" "AppPoolIdentity:NetworkService" "LicencePath:C:\Sitecore\license.xml" 

### Delete ###

A Sitecore instance can be deeleted using the **/delete** switch. The task takes the following parameters:

* InstanceName - The name of the Sitecore instance to remove, this can either be "mydomain.com" or "http://mydomain.com"
* InstanceDirectory - The path to the location where the Sitecore instance exists
* ConnectionString - The connection string to used to install the Sitecore databases

The following is an example of calling delete:

    Hedgehog.Tds.Build.Sim.Console.exe delete "InstanceName:demo.tds" "InstanceDirectory:c:\sitecore" "ConnectionString:Data Source=.;User ID=sitecore;Password=sitecore123" 


## License ##

Distributed using the [Apache 2 Licence](https://github.com/HedgehogDevelopment/BuildExtensions/blob/master/Licence.txt)
