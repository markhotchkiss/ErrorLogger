# Error Logger
This is an Error Logger for C# Applications.  It uses a standard class library to allow you to easily log all errors to a single Text File, SQLite or SQL.  You can pass in parameters to allow the text file to be created in a specific location with any given name.  It also takes care of Archiving where appropriate and even gives you options for reading your log file back in one easy, simple command.

The library has now been updated to include more "Read" options, including reading the last X records, Logs by Category and returning all logs between two dates.

Throw this in your Exceptions for easy exception logging or pass in a string for a static error message.

Check the [Usage](https://github.com/markhotchkiss/ErrorLogger/wiki#usage) section below for more info...

## Setup

Either clone this repository using your local tools, or download from NuGet.

PM> Install-Package MJH.ErrorLogger
-----------------------------------

## Usage

# XML Setup

When you download the NuGet package to your project, you should receive an XML file called "LoggerConfig.xml".  All the configuration required to use this package is contained within this file.

Below is an annotated version to get you started!

<?xml version="1.0" encoding="utf-8" ?>
<LoggerConfig>
  <LoggerType>TextFile</LoggerType> <!--TextFile, SQLite, SQL-->
  <LoggingLevel>Debug</LoggingLevel> <!--Debug, Info, Error - NOTE, this is case sensitive-->
  <Sql>
    <ServerInformation>
      <Server>(local)\MSSQL2012</Server>
      <Database>ErrorLogger</Database>
      <Username>sa</Username>
      <Password>5chne1deR</Password>
    </ServerInformation>
    <LoggerInformation>
      <HistoryToKeep>0</HistoryToKeep> <!--Use 0 for no truncation-->
    </LoggerInformation>
  </Sql>
  <SQLite>
    <ServerInformation>
      <LogFileName>Activity.db</LogFileName>
      <LogFileLocation>D:\Tests\Logger</LogFileLocation>
    </ServerInformation>
    <LoggerInformation>
      <HistoryToKeep>0</HistoryToKeep> <!--Use 0 for no truncation-->
    </LoggerInformation>
  </SQLite>
  <Text>
    <FileInformation>
      <LogFileName>Activity.log</LogFileName>
      <LogFileLocation>D:\Tests\Logger</LogFileLocation>
      <ArchiveDirectory>D:\Tests\Logger\Archive</ArchiveDirectory>
    </FileInformation>
    <LoggerInformation>
      <FileHistoryToKeep>2</FileHistoryToKeep> <!--Use 0 for no truncation-->
      <MaxFileSize>1</MaxFileSize>
    </LoggerInformation>
  </Text>
</LoggerConfig>

# Library Usage

The library uses static types therefore there is no need to instantiate the library in your code.  You can use a single line, as below, to log an error to either a TextFile, SQLite or SQL.

```javascript
using MJH.ErrorLogger;
using MJH.ErrorLogger.Models;

    public static class InternalLogger
    {
        public void LogError(Exception exception)
        {
            Logger.LogInfo(LogCategory.Service, exception)
        }
    }
```

Call the Static Class where required and pass the arguments.
```javascript  
   try
   {
       Process();
   }
   catch (Exception exception)
   {
       InternalLogger.Create().LogError(LogCategory.File, exception);
   }
```
As well as writing to your destination as required, you can also read the Error file using the following...

```javascript  
   var logs = Logger.Read();
   foreach(var log in logs)
   {
      //Do Something.
   }
```

The Read() method will return your TextFile, SQL Table or SQLite Table as an object of type IReadOnlyCollection<T> which means that you can Iterate the logs and filter/order as needed.  The CSV TextFile also gets returned as an Object for ease :-)

# Programmatically Change the Configuration

In order to allow you to change the config of the ErrorLogger directly from code, you can do so by Instantiating the ConfigurationHandler() to override any aspect of the configuration.  Once you Load it in, you can make your changes and write it back.

## Example
### Reading the Configuration

```javascript  
using MJH.BusinessLogic.Configuration;

        public void Read()
        {
            var config = new ConfigurationHandler().Read();
            config.LoggingLevel = LoggingLevel.Debug;
            config.LoggerType = LogOutputType.Sql;
        }
```

### Writing the Configuration

```javascript  
using MJH.BusinessLogic.Configuration;

        public void Write(LoggerConfig config)
        {
            var writer = new ConfigurationHandler().Write(config); //Config from the Read() method above after changes.
        }
```

## Other
Feel free to Fork/Download the project and take a look at the UnitTests.  These will give you a good idea of the accepted usage of all available methods.  If there are any issues/bugs, feel free to log these on GitHub and I will look at them as soon as I can!

Thanks :-)
