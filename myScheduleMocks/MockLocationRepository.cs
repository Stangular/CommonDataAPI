using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myScheduleModels.Models;
using myScheduleModels.Models.Interfaces;
using DataRecord;

namespace mySchedule.Mocks.Repository
{
    public class MockLocationRepository : ILocationRepository
    {

        public Form UserLocations(string userID)
        {
            return null;
        }

        //public IEnumerable<LocationDTO> Places
        //{
        //    get
        //    {

        //        return new List<LocationDTO>{
        //            new LocationDTO{
        //                Address ="7683 North County Road 800 West ::",
        //                Path ="Earth,USA,Indiana,Henry,Middletown",
        //                Lat = 40.057094M,
        //                Lon = -85.541098M,
        //                Description = "My House"
        //            },
        //            new LocationDTO {
        //                Address = "Ball State University, 2000 W University Ave",
        //                Path = "Indiana,Delaware,Muncie",
        //                Lat = 40.201169M,
        //                Lon = -85.409549M,
        //                Description = "Where Jill works"
        //            }
        //        };

        //    }
        //}

        //public Location GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
