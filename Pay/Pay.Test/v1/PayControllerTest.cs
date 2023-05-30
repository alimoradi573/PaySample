using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Pay.OvetimePolicies.Api.Controllers.v1;
using Pay.OvetimePolicies.Application.DTOs;
using Pay.OvetimePolicies.Application.Services;
using Xunit.Abstractions;

namespace Pay.OvetimePolicies.Api.Tests.v1
{
    public class PayControllerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public PayControllerTest(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void AliveTest()
        {
            _testOutputHelper.WriteLine($".Net Core Web API Version 1");
        }

        [Fact]
        public async Task Calculate_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IPayService>();
            var controller = new PayController(mockService.Object);

            var datatype = "json";
            var payDTO = new PayDTO { Allowance=10,BasicSalary=100,Date=DateTime.Now,FirstName="Fnmae",LastName="Lname",Transportation=1000};

            // Act
            var result = await controller.Calculate(datatype, payDTO);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Calculate2_ReturnsOkResult()
        {
            // Arrange
            var mockBinder = new Mock<IModelBinder>();
            var mockService = new Mock<IPayService>();
            var mockFactory = new Mock<ModelBinderFactory>();
          //  mockFactory.Setup(f => f.CreateBinder(It.IsAny<ModelBinderFactoryContext>())).Returns(mockBinder.Object);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(c => c.RequestServices).Returns(Mock.Of<IServiceProvider>());

            var controllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };

            var controller = new PayController(mockService.Object)
            {
                ControllerContext = controllerContext
            };

            var payDTO = new PayDTO { Allowance = 10, BasicSalary = 100, Date = DateTime.Now, FirstName = "Fnmae", LastName = "Lname", Transportation = 1000 };

            // Act
            var result = await controller.Calculate2(payDTO);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Update_WithValidPayDTO_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IPayService>();
            var controller = new PayController(mockService.Object);

            var payDTO = new PayDTO { Allowance = 10, BasicSalary = 100, Date = DateTime.Now, FirstName = "Fnmae", LastName = "Lname", Transportation = 1000 };

            // Act
            var result =   controller.Update(payDTO) ;

            // Assert
            Assert.IsType<Task<PayDTO>>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IPayService>();
            var controller = new PayController(mockService.Object);

            var id = 3; // Specify a valid ID

            // Act
            var result =   controller.Delete(id);

            // Assert
            Assert.IsType<Task<int>>(result);
        }


        [Fact]
        public async Task GetRangeAsync_WithValidFilter_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IPayService>();
            var controller = new PayController(mockService.Object);

            var filter = new PayFilterDTO { FromDate=DateTime.Now.AddDays(-5),ToDate=DateTime.Now.AddDays(3) };

            // Act
            var result = await controller.GetRangeAsync(filter);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IPayService>();
            var controller = new PayController(mockService.Object);

            var id = 123; // Specify a valid ID

            // Act
            var result = await controller.Get(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
