using MJH.Models;
using NUnit.Framework;
using System;
using System.IO;

namespace MJH.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        [OneTimeSetUp]
        public void InitialiseTests()
        {

        }

        [TearDown]
        public void Cleanup()
        {
            //If tests have failed, cleanup residual files.
            var fileInfo = new FileInfo("D:\\Tests\\Logger\\Activity.log");
            fileInfo.Delete();
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
                Logger.LogError(LoggingTypeModel.LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo("D:\\Tests\\Logger\\Activity.log");
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(fileInfo.FullName);
                Assert.IsNotEmpty(streamReader.ReadToEnd());
                streamReader.Close();

                File.Delete(fileInfo.FullName);
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
                Logger.LogError(LoggingTypeModel.LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo("D:\\Tests\\Logger\\Activity.log");

                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(fileInfo.FullName);
                var errorText = streamReader.ReadToEnd();
                streamReader.Close();

                Assert.IsNotEmpty(errorText);
                Assert.That(!errorText.Contains("InnerException"));

                File.Delete(fileInfo.FullName);
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
                Logger.LogInfo(LoggingTypeModel.LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo("D:\\Tests\\Logger\\Activity.log");
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(fileInfo.FullName);
                Assert.IsNotEmpty(streamReader.ReadToEnd());
                streamReader.Close();

                File.Delete(fileInfo.FullName);
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
                Logger.LogDebug(LoggingTypeModel.LogCategory.Process, exception);

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo("D:\\Tests\\Logger\\Activity.log");
                Assert.True(fileInfo.Exists);

                var streamReader = new StreamReader(fileInfo.FullName);
                Assert.IsNotEmpty(streamReader.ReadToEnd());
                streamReader.Close();

                File.Delete(fileInfo.FullName);
            }
        }
    }
}