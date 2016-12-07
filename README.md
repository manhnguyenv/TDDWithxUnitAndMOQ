# TDDWithxUnitAndMOQ
ASP.NET MVC 5 application with TDD using Unit testing framework (xUnit.NET) and Moking/Isolation framework (MOQ).

First we create a new ASP.NET MVC 5 project. Note that when you first create a new ASP.NET MVC project, you get the option to select your preferred Unit Testing framework. Since there is no xUnit.NET project template in visual studio, we will add required package form Nuget later.

Let’s say we have a requirement to display list of cars. Note that this is a simple example, but the TDD concepts you apply here remain same for larger applications.

ASP.NET MVC 5 application with TDD using Unit testing framework (xUnit.NET) and Moking/Isolation framework (MOQ).
Add, Within ASP.NET MVC project:
CarDealershipController.cs
Car.cs
ICarDealershipResposity

Add With in Unit Test project:
CarDealershipControllerTests.cs

and 
Add install xUnit.Net and asp.net mvc using nuget package manager

use: namespaces
using Xunit;
using System.Web.Mvc;


For mocking :
Add repository :
namespace TDDWithxUnitAndMOQ.Repository
{
    public interface ICarDealershipResposity
    {
        IEnumerable<Car> GetAllCars();
    }
}

Change the controller constructor :
 Add constructor in xUnit.NET test class, which can execute code that requires executing before each and every test. Equivalent to [TestInitialize] attribute in MSTest

Now, we configure/setup the CarDealershipRepository’s GetCarList method to return fake data for testing purpose using isolation framework MOQ.

This construct does not work, because we have changed the code at controller.
  CarDealershipController con = new CarDealershipController();


Click on manage nuget pacakges, and search "moq"
Moq is the most popular and friendly mocking framework for .NET. and Install it.

Add :
using Moq;


And add test code :
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

Change controller :
        private ICarDealershipRespository _carRepo;

       // constructor
        public CarDealershipController(ICarDealershipRespository _repo)
        {
            this._carRepo = _repo;
        }

        public ViewResult List()
        {
            var carList = _carRepo.GetAllCars();
            return View("List",carList);
        }


Repos has one method :
 public interface ICarDealershipRespository
    {
        IEnumerable<Car> GetAllCars();
    }

Note: It is not necessary to write the implementation, to start writing the Test. 

It is important to run all Unit Tests (at least in the same area) after every refactor. This is because we want to make sure the refactoring did not cause existing Unit Tests to fail. If any of the existing Unit Tests fails, we would refactor the production code with the smallest possible change to make the failing Test pass.

So far, what we saw is a pattern where we add a Unit Test, write enough production code to pass that Unit Test, add a new Unit Test, refactor the code to pass the new Unit Test, and re-run all Unit Tests. This method is called test driven development.


Reference  : http://www.dotnetcurry.com/aspnet-mvc/836/aspnet-mvc-app-test-driven-tdd-xunit-moq
