using Moq;
using verbum_service_domain.DTO.Response;
using verbum_service_application.Service;
using verbum_service.Controllers;
using verbum_service_domain.DTO.Request;
using verbum_service_infrastructure.Impl.Workflow;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using verbum_service_application.Workflow;
using AutoMapper;
using verbum_service_infrastructure.Impl.Validation;
using verbum_service_infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using verbum_service_domain.Models;
using System.ComponentModel.Design;

namespace verbum_service_test
{
    public class UnitTest1
    {
        [Fact]
        public async void UserController_GetAllUserInCompany_ReturnData()
        {
            var userServiceMock = new Mock<UserService>();
            var companyId = Guid.NewGuid();
            var Fix = new Fixture();
            var userInfo = Fix.Create<UserInfo>();

            userServiceMock
                .Setup(us => us.GetAllUserInCompany(companyId))
                .ReturnsAsync(new List<UserInfo> { userInfo });

            var controller = new UserController(userServiceMock.Object, null);

            // Act
            var result = await controller.GetAllUserInCompany(companyId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserInfo>>(result);
            Assert.Contains(result, u => u == userInfo);
            userServiceMock.Verify(u => u.GetAllUserInCompany(companyId), Times.Once);
        }

        [Fact]
        public async Task UserController_UpdateUserCompanyStatus_TaskComplete()
        {
            // Arrange
            var userServiceMock = new Mock<UserService>();
            userServiceMock.Setup(us => us.UpdateUserCompanyStatus(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            var controller = new UserController(userServiceMock.Object, null);

            // Act
            await controller.UpdateUserCompanyStatus(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            userServiceMock.Verify(us => us.UpdateUserCompanyStatus(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task UserController_UpdateUser_TaskComplete()
        {
            
        }

        [Fact]
        public async Task GetUserInCompanyById_UserServiceGetUserInCompanyByIdCalled()
        {
            // Arrange
            var userServiceMock = new Mock<UserService>();
            var userId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var Fix = new Fixture();
            var userInfo = Fix.Create<UserInfo>();

            userServiceMock
                .Setup(u => u.GetUserInCompanyById(userId, companyId))
                .ReturnsAsync(userInfo);

            var controller = new UserController(userServiceMock.Object,null);

            // Act
            var result = await controller.GetUserInCompanyById(userId, companyId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserInfo>(result);
            userServiceMock.Verify(u => u.GetUserInCompanyById(userId, companyId), Times.Once);
        }
    }
}