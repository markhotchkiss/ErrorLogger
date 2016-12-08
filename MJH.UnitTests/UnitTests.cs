using MJH.Interfaces;
using MJH.Models;
using NUnit.Framework;
using System;
using System.IO;

namespace MJH.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        private Logger _logger;

        [OneTimeSetUp]
        public void InitialiseTests()
        {
            _logger = new Logger
            {
                LogOutputFileLocation = "D:\\Tests\\Logger\\",
                LogOutputFileName = "Activity.log",
                LoggingLevel = LoggingLevel.Debug
            };
        }

        [TearDown]
        public void Cleanup()
        {
            //If tests have failed, cleanup residual files.
            var fileInfo = new FileInfo(_logger.LogOutputFileLocation + _logger.LogOutputFileName);

            if (fileInfo.Exists)
            {
                File.Delete(fileInfo.FullName);
            }
        }

        [Test, Order(1)]
        public void LogError()
        {
            try
            {
                throw new Exception("ErrorLogger", new Exception("This is my inner exception"));
            }
            catch (Exception exception)
            {
                _logger.LogError(LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
                Assert.IsNotEmpty(streamReader.ReadToEnd());
                streamReader.Close();

                File.Delete(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
            }
        }

        [Test, Order(2)]
        public void LogErrorWithNullInnerException()
        {
            try
            {
                throw new Exception("ErrorLogger");
            }
            catch (Exception exception)
            {
                _logger.LogError(LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
                var errorText = streamReader.ReadToEnd();
                streamReader.Close();

                Assert.IsNotEmpty(errorText);
                Assert.That(!errorText.Contains("InnerException"));

                File.Delete(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
            }
        }

        [Test, Order(3)]
        public void LogInfo()
        {
            try
            {
                throw new Exception("ErrorLogger", new Exception("This is my inner exception"));
            }
            catch (Exception exception)
            {
                _logger.LogInfo(LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
                Assert.IsNotEmpty(streamReader.ReadToEnd());
                streamReader.Close();

                File.Delete(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
            }
        }

        [Test, Order(4)]
        public void LogDebug()
        {
            try
            {
                throw new Exception("ErrorLogger", new Exception("This is my inner exception"));
            }
            catch (Exception exception)
            {
                _logger.LogDebug(LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
                Assert.IsNotEmpty(streamReader.ReadToEnd());
                streamReader.Close();

                File.Delete(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
            }
        }

        [Test, Order(5)]
        public void LogDebugWhenNotAllowed()
        {
            try
            {
                throw new Exception("ErrorLogger", new Exception("This is my inner exception"));
            }
            catch (Exception exception)
            {
                _logger.LoggingLevel = LoggingLevel.Info;
                _logger.LogDebug(LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
                Assert.False(fileInfo.Exists);

                File.Delete(_logger.LogOutputFileLocation + _logger.LogOutputFileName);
            }
        }
    }
}
