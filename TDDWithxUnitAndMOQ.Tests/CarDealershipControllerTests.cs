using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TDDWithxUnitAndMOQ.Controllers;
using TDDWithxUnitAndMOQ.Models;
using TDDWithxUnitAndMOQ.Repository;
using Xunit;

namespace TDDWithxUnitAndMOQ.Tests
{
    public class CarDealershipControllerTests
    {
        private Mock<ICarDealershipRespository> reposStub;
        private CarDealershipController controller;

        public CarDealershipControllerTests()
        {
            reposStub = new Mock<ICarDealershipRespository>();
            controller = new CarDealershipController(reposStub.Object);
        }

        [Fact]
        public void List_WhenActionExecute_ReturnsViewNameList()
        {
            //act
            var result = controller.List() as ViewResult;
           
            //assert
            Assert.Equal<string>(result.ViewName, "List");
        }

        [Fact]
        public void List_WhenActionExecute_ReturnsContainsListOfCars()
        {

            //arrange to return fake data
            reposStub.Setup(x => x.GetAllCars()).Returns(() => new List<Car> { new Car() });

            //act
            var result = controller.List() as ViewResult;

            //assert
            Assert.True(((IEnumerable<Car>)result.Model).Any());
        }
    }
}
