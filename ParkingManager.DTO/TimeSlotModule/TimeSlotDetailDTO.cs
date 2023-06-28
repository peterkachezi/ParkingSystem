using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManager.DTO.TimeSlotModule
{
    public class TimeSlotDetailDTO
    {
        public List<TimeSlotDTO> slotList { get; set; }
        public DateDTO dateDTO { get; set; }

    }
}
