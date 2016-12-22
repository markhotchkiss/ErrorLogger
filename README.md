#Error Logger
This is an Error Logger for C# Applications.  It uses a standard class library to allow you to easily log all errors to a single Text File, SQLite or SQL.  You can pass in parameters to allow the text file to be created in a specific location with any given name.

Check the [Usage](https://github.com/markhotchkiss/ErrorLogger/wiki#usage) section below for more info...

##Setup

Either clone this repository using your local tools, or download from NuGet.

PM> Install-Package MJH.ErrorLogger
-----------------------------------

##Usage

#XML Setup

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

#Library Usage

The library uses static types therefore there is no need to instantiate the library in your code.  You can use a single line, as below, to log an error to either a TextFile, SQLite or SQL.

```javascript
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
