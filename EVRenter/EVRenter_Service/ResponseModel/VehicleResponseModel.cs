using EVRenter_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRenter_Service.ResponseModel
{
    public class VehicleResponseModel
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public int ModelID { get; set; }
        //public string ModelName { get; set; }
        //public virtual Model Model { get; set; }
        public int StationID { get; set; }
        //public int StationName { get; set; }
        //public virtual Station Station { get; set; }
        public int Status { get; set; }
    }
}
