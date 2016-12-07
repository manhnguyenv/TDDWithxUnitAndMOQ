using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDDWithxUnitAndMOQ.Models;

namespace TDDWithxUnitAndMOQ.Repository
{
    public interface ICarDealershipRespository
    {
        IEnumerable<Car> GetAllCars();
    }
}