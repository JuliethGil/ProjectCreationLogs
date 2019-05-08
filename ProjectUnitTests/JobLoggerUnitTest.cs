namespace ProjectUnitTests
{
    using CrearLog.Controller;
    using NSubstitute;
    using Project.Constants;
    using Project.Interfaces;
    using Project.Models;
    using ProjectUnitTests.DataObject;
    using System;
    using System.Collections.Generic;
    using Xunit;
    using static CrearLog.Enumerators.LogEnumerators;

    public class JobLoggerUnitTest
    {
        #region Attributes
        private JobLoggerController jobLoggerController;
        private readonly ILogQuery logQuery;
        private readonly IFileAccessController fileAccessController;
        private readonly IConsoleAccessController consoleAccessController;

        #endregion

        #region Constructors
        public JobLoggerUnitTest()
        {
            logQuery = Substitute.For<ILogQuery>();
            fileAccessController = Substitute.For<IFileAccessController>();
            consoleAccessController = Substitute.For<IConsoleAccessController>();
        }
        #endregion

        #region Tests
        [Fact]
        public void SaveOnConsole()
        {
            //Arrange
            jobLoggerController = new JobLoggerController(
            false,
            true,
            false,
            TypeMessage.Error,
            fileAccessController,
            consoleAccessController,
            logQuery);
            string message = "It's a error";
            JobLogResponseModel jobLogToResponse =  JobLoggerResponseDataObject.CreateJobLogResponse(true,message, TypeLogSourse.Console);
            InitializeConsoleAccessMock(jobLogToResponse);

            //Act
            List<JobLogResponseModel> response = jobLoggerController.LogMessage(message);
            
            //Assert
            Assert.Single(response);
            Assert.Equal(message, response[0].Message);
            Assert.True(response[0].IsSuccess);
            Assert.Equal(TypeLogSourse.Console, response[0].Source);
        }

        [Fact]
        public void NotSaveOnConsole()
        {
            //Arrange
            jobLoggerController = new JobLoggerController(
            false,
            true,
            false,
            TypeMessage.Message,
            fileAccessController,
            consoleAccessController,
            logQuery);
            string message = "It's a exeption";
            JobLogResponseModel jobLogToResponse = JobLoggerResponseDataObject.CreateJobLogResponse(false, message, TypeLogSourse.Console);
            InitializeConsoleAccessMock(jobLogToResponse);

            //Act
            List<JobLogResponseModel> response = jobLoggerController.LogMessage(message);

            //Assert
            Assert.Single(response);
            Assert.Equal(message, response[0].Message);
            Assert.False(response[0].IsSuccess);
            Assert.Equal(TypeLogSourse.Console, response[0].Source);
        }

        [Fact]
        public void SaveOnDataBase()
        {
            //Arrange
            jobLoggerController = new JobLoggerController(
            false,
            false,
            true,
            TypeMessage.Warning,
            fileAccessController,
            consoleAccessController,
            logQuery);
            string message = "It's a warning";
            JobLogResponseModel jobLogToResponse = JobLoggerResponseDataObject.CreateJobLogResponse(true, message, TypeLogSourse.DataBase);
            InitializeLogQueryMock(jobLogToResponse);

            //Act
            List<JobLogResponseModel> response = jobLoggerController.LogMessage(message);

            //Assert
            Assert.Single(response);
            Assert.Equal(message, response[0].Message);
            Assert.True(response[0].IsSuccess);
            Assert.Equal(TypeLogSourse.DataBase, response[0].Source);
        }

        [Fact]
        public void DontSaveOnDataBase()
        {
            //Arrange
            jobLoggerController = new JobLoggerController(
            false,
            false,
            true,
            TypeMessage.Error,
            fileAccessController,
            consoleAccessController,
            logQuery);
            string message = "It's a exeption";
            JobLogResponseModel jobLogToResponse = JobLoggerResponseDataObject.CreateJobLogResponse(false, message, TypeLogSourse.DataBase);
            InitializeLogQueryMock(jobLogToResponse);

            //Act
            List<JobLogResponseModel> response = jobLoggerController.LogMessage(message);

            //Assert
            Assert.Single(response);
            Assert.Equal(message, response[0].Message);
            Assert.False(response[0].IsSuccess);
            Assert.Equal(TypeLogSourse.DataBase, response[0].Source);
        }

        [Fact]
        public void SaveOnFile()
        {
            //Arrange
            jobLoggerController = new JobLoggerController(
            true,
            false,
            false,
            TypeMessage.Warning,
            fileAccessController,
            consoleAccessController,
            logQuery);
            string message = "It's a warning";
            JobLogResponseModel jobLogToResponse = JobLoggerResponseDataObject.CreateJobLogResponse(true, message, TypeLogSourse.File);
            InitializeFileAccessMock(jobLogToResponse);

            //Act
            List<JobLogResponseModel> response = jobLoggerController.LogMessage(message);

            //Assert
            Assert.Single(response);
            Assert.Equal(message, response[0].Message);
            Assert.True(response[0].IsSuccess);
            Assert.Equal(TypeLogSourse.File, response[0].Source);
        }

        [Fact]
        public void NotSaveOnFile()
        {
            //Arrange
            jobLoggerController = new JobLoggerController(
            true,
            false,
            false,
            TypeMessage.Error,
            fileAccessController,
            consoleAccessController,
            logQuery);
            string message = "It's a exeption";
            JobLogResponseModel jobLogToResponse = JobLoggerResponseDataObject.CreateJobLogResponse(false, message, TypeLogSourse.File);
            InitializeFileAccessMock(jobLogToResponse);

            //Act
            List<JobLogResponseModel> response = jobLoggerController.LogMessage(message);

            //Assert
            Assert.Single(response);
            Assert.Equal(message, response[0].Message);
            Assert.False(response[0].IsSuccess);
            Assert.Equal(TypeLogSourse.File, response[0].Source);
        }

        [Fact]
        public void DontSelectSourse()
        {
            //Arrange
            jobLoggerController = new JobLoggerController(
            false,
            false,
            false,
            TypeMessage.Warning,
            fileAccessController,
            consoleAccessController,
            logQuery);

            string message = "It's a warning";
            string ErrorTypeStorage = string.Empty;

            try
            {
                //Act
                List<JobLogResponseModel> response = jobLoggerController.LogMessage(message);
            }
            catch (Exception)
            {
                ErrorTypeStorage = "Invalid configuration";
            }
            //asert

            Assert.Equal(ErrorTypeStorage, ErrorMessages.ErrorTypeStorage);
        }

        [Fact]
        public void DontMessage()
        {
            //Arrange
            jobLoggerController = new JobLoggerController(
            false,
            true,
            false,
            TypeMessage.Warning,
            fileAccessController,
            consoleAccessController,
            logQuery);

            string message = string.Empty;
            string ErrorTypeMessage = string.Empty;

            try
            {
                //Act
                List<JobLogResponseModel> response = jobLoggerController.LogMessage(message);
            }
            catch (Exception)
            {
                ErrorTypeMessage = "Message is null or empy";
            }
            //asert

            Assert.Equal(ErrorTypeMessage, ErrorMessages.ErrorTypeMessage);
        }

        [Fact]
        public void SelectAllOptions()
        {
            //Arrange
            jobLoggerController = new JobLoggerController(
            true,
            true,
            true,
            TypeMessage.Warning,
            fileAccessController,
            consoleAccessController,
            logQuery);
            string message = "It's all options";
            JobLogResponseModel jobLogToResponseDataBase = JobLoggerResponseDataObject.CreateJobLogResponse(true, message, TypeLogSourse.DataBase);
            InitializeLogQueryMock(jobLogToResponseDataBase);

            JobLogResponseModel jobLogToResponseFile = JobLoggerResponseDataObject.CreateJobLogResponse(true, message, TypeLogSourse.File);
            InitializeFileAccessMock(jobLogToResponseFile);

            JobLogResponseModel jobLogToResponseConsole = JobLoggerResponseDataObject.CreateJobLogResponse(true, message, TypeLogSourse.Console);
            InitializeConsoleAccessMock(jobLogToResponseConsole);

            //Act
            List<JobLogResponseModel> response = jobLoggerController.LogMessage(message);

            //Assert
            Assert.Equal(3,response.Count);
            Assert.Equal(message, response[0].Message);
            Assert.True(response[2].IsSuccess);
            Assert.Equal(TypeLogSourse.File, response[1].Source);
        }
        #endregion

        #region Private Methods
        private void InitializeFileAccessMock(JobLogResponseModel jobLogResponse)
        {
            fileAccessController.InsertFile(Arg.Any<string>(), Arg.Any<TypeMessage>()).Returns(jobLogResponse);
        }

        private void InitializeLogQueryMock(JobLogResponseModel jobLogResponse)
        {
            logQuery.DataBaseInsert(Arg.Any<LogDataBaseModel>()).Returns(jobLogResponse);
        }

        private void InitializeConsoleAccessMock(JobLogResponseModel jobLogResponse)
        {
            consoleAccessController.ViewConsole(Arg.Any<string>(), Arg.Any<TypeMessage>()).Returns(jobLogResponse);
        }
        #endregion
    }
}
