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
        private TextLogger _textLogger;

        [OneTimeSetUp]
        public void InitialiseTests()
        {
            _textLogger = new TextLogger
            {
                LogOutputFileLocation = "C:\\Tests\\TextLogger\\",
                LogOutputFileName = "Activity.log",
                LoggingLevel = LoggingLevel.Debug
            };
        }

        [TearDown]
        public void Cleanup()
        {
            //If tests have failed, cleanup residual files.
            var fileInfo = new FileInfo(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);

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
                _textLogger.LogError(LoggingTypeModel.LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
                Assert.IsNotEmpty(streamReader.ReadToEnd());
                streamReader.Close();

                File.Delete(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
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
                _textLogger.LogError(LoggingTypeModel.LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
                var errorText = streamReader.ReadToEnd();
                streamReader.Close();

                Assert.IsNotEmpty(errorText);
                Assert.That(!errorText.Contains("InnerException"));

                File.Delete(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
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
                _textLogger.LogInfo(LoggingTypeModel.LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
                Assert.IsNotEmpty(streamReader.ReadToEnd());
                streamReader.Close();

                File.Delete(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
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
                _textLogger.LogDebug(LoggingTypeModel.LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
                Assert.IsNotEmpty(streamReader.ReadToEnd());
                streamReader.Close();

                File.Delete(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
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
                _textLogger.LoggingLevel = LoggingLevel.Info;
                _textLogger.LogDebug(LoggingTypeModel.LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
                Assert.False(fileInfo.Exists);

                File.Delete(_textLogger.LogOutputFileLocation + _textLogger.LogOutputFileName);
            }
        }
    }
}
