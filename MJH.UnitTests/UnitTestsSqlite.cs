using MJH.BusinessLogic.Configuration;
using MJH.Models;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace MJH.UnitTests
{
    [TestFixture]
    public class UnitTestsSqlite
    {
        private LoggerConfig _config;

        [OneTimeSetUp]
        public void InitialiseTests()
        {
            _config = new ConfigurationHandler().Read();

            _config.LoggerType = LoggingTypeModel.LogOutputType.SQLite;

            new ConfigurationHandler().Write(_config);

            if (_config.LoggerType != LoggingTypeModel.LogOutputType.SQLite)
            {
                throw new Exception("Set the logger type to SQLite in the Config file before running these tests.");
            }
        }

        [Test, Order(1)]
        public void WriteSqliteError()
        {
            for (int i = 0; i < 10; i++)
            {
                Logger.LogError(LoggingTypeModel.LogCategory.Process, new Exception("ErrorLogger", new Exception("This is my inner exception")));
            }

            //Check that the log file exists with text inside
            var fileInfo = new FileInfo(_config.SQLite.ServerInformation.LogFileLocation + "\\" + _config.SQLite.ServerInformation.LogFileName);
            Assert.True(fileInfo.Exists);
        }

        [Test, Order(2)]
        public void WriteSqliteInfo()
        {
            for (int i = 0; i < 10; i++)
            {
                Logger.LogInfo(LoggingTypeModel.LogCategory.Process, new Exception("ErrorLogger", new Exception("This is my inner exception")));
            }


            //Check that the log file exists with text inside
            var fileInfo = new FileInfo(_config.SQLite.ServerInformation.LogFileLocation + "\\" + _config.SQLite.ServerInformation.LogFileName);
            Assert.True(fileInfo.Exists);
        }

        [Test, Order(3)]
        public void WriteSqliteDebug()
        {
            for (int i = 0; i < 100; i++)
            {
                Logger.LogDebug(LoggingTypeModel.LogCategory.Process, new Exception("ErrorLogger", new Exception("This is my inner exception")));
            }

            //Check that the log file exists with text inside
            var fileInfo = new FileInfo(_config.SQLite.ServerInformation.LogFileLocation + "\\" + _config.SQLite.ServerInformation.LogFileName);
            Assert.True(fileInfo.Exists);
        }

        [Test, Order(3)]
        public void WriteSqliteDebug_CheckConcurrency()
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    Logger.LogDebug(LoggingTypeModel.LogCategory.Process, new Exception("ErrorLogger", new Exception("This is my inner exception")));
                }

                //Check that the log file exists with text inside
                var fileInfo = new FileInfo(_config.SQLite.ServerInformation.LogFileLocation + "\\" + _config.SQLite.ServerInformation.LogFileName);
                Assert.True(fileInfo.Exists);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Test, Order(5)]
        public void WriteSqliteLongMessage()
        {
            var message =
                "The HTTP request is unauthorized with client authentication scheme 'Digest'.The authentication header received from the server was 'Digest realm=\"ews@SxWBM\",qop=\"auth\",nonce=\"A2591E18D625F22A1EC0CB914E8781FF\",opaque=\"C117F5\"'.The remote server returned an error: (401) Unauthorized.Server stack trace: at System.ServiceModel.Channels.HttpChannelUtilities.ValidateAuthentication(HttpWebRequest request, HttpWebResponse response, WebException responseException, HttpChannelFactory`1 factory)    at System.ServiceModel.Channels.HttpChannelUtilities.ValidateRequestReplyResponse(HttpWebRequest request, HttpWebResponse response, HttpChannelFactory`1 factory, WebException responseException, ChannelBinding channelBinding)    at System.ServiceModel.Channels.HttpChannelFactory`1.HttpRequestChannel.HttpChannelRequest.WaitForReply(TimeSpan timeout)    at System.ServiceModel.Channels.RequestChannel.Request(Message message, TimeSpan timeout)    at System.ServiceModel.Dispatcher.RequestChannelBinder.Request(Message message, TimeSpan timeout)    at System.ServiceModel.Channels.ServiceChannel.Call(String action, Boolean oneway, ProxyOperationRuntime operation, Object[] ins, Object[] outs, TimeSpan timeout)    at System.ServiceModel.Channels.ServiceChannelProxy.InvokeService(IMethodCallMessage methodCall, ProxyOperationRuntime operation)    at System.ServiceModel.Channels.ServiceChannelProxy.Invoke(IMessage message)  Exception rethrown at[0]:     at System.Runtime.Remoting.Proxies.RealProxy.HandleReturnMessage(IMessage reqMsg, IMessage retMsg)    at System.Runtime.Remoting.Proxies.RealProxy.PrivateInvoke(MessageData & msgData, Int32 type)    at Ews.Client.IDataExchange.GetWebServiceInformation(GetWebServiceInformationRequest1 request)    at Ews.Client.DataExchangeClient.Ews.Client.IDataExchange.GetWebServiceInformation(GetWebServiceInformationRequest1 request)    at Ews.Client.DataExchangeClient.GetWebServiceInformation(GetWebServiceInformationRequest request)    at Ews.Client.EwsClient.ExecuteAndLogCall[TRequest, TResponse](String methodName, TRequest request, Func`2 methodToLog)    at Ews.Client.EwsClient.get_EwsVersionImplemented()    at Ews.Client.EwsClient..ctor(EwsSecurity credentials, String address, EwsBindingConfig bindingConfig, Nullable`1 compatibilityVersion)    at Mongoose.Process.ExtensionMethods.CreateClientConnection(IEwsEndpoint ewsEndpoint)    at Mongoose.Process.Ews.AlarmItemReader.ReadData()";

            Logger.LogError(LoggingTypeModel.LogCategory.Process, message);
        }

        [Test, Order(4)]
        public void Read()
        {
            var log = Logger.Read();
            Assert.IsNotNull(log);
        }

        [Test, Order(5)]
        public void Read_MaxRecords5()
        {
            var log = Logger.Read(5);

            Assert.AreEqual(5, log.Count);
        }

        [Test, Order(6)]
        public void Read_BetweenDates()
        {
            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now;

            var log = Logger.Read(startDate, endDate);

            Assert.GreaterOrEqual(log.Count, 1);
            Assert.That(log.Count(dt => dt.DateTimeUTC < startDate) == 0);
            Assert.That(log.Count(dt => dt.DateTimeUTC > endDate) == 0);
        }

        [Test, Order(7)]
        public void Read_SpecificLoggingLevel()
        {
            var log = Logger.Read(LoggingTypeModel.LogCategory.Process);

            Assert.That(log.Count > 0);
            Assert.That(log.Count(l => l.ErrorType != "Process") == 0);
        }

        [Test, Order(8)]
        public void WriteTransactionLog()
        {
            Logger.LogTransaction("MY SOURCE", "My Message");
        }
    }
}
