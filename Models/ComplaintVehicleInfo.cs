using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupCCP.Models
{
    public class ComplaintVehicleInfo
    {
        public int VehicleId { get; set; }
        public int VehicleBrandId { get; set; }
        public Brands Brands { get; set; }
        public string VehicleModel { get; set;}
        public string VehilcleVIN { get; set;}
        [DataType(DataType.Date)]
        public string VehiclePurchaseDate { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public ICollection<ComplaintLogDetail> Logs { get; set; }

    }
}
