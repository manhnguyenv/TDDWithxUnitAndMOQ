using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDDWithxUnitAndMOQ.Repository;

namespace TDDWithxUnitAndMOQ.Controllers
{
    public class CarDealershipController : Controller
    {
        private ICarDealershipRespository _carRepo;

        public CarDealershipController(ICarDealershipRespository _repo)
        {
            this._carRepo = _repo;
        }
        // GET: CarDealership
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List()
        {
            var carList = _carRepo.GetAllCars();
            return View("List",carList);
        }
    }
}