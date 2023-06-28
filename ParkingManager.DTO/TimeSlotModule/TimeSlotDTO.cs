﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManager.DTO.TimeSlotModule
{
    public class TimeSlotDTO
    {
        public Guid Id { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string TimeSlot => FromTime.ToString("h:mm tt") + " - " + ToTime.ToString("h:mm tt");
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedBy { get; set; }
        public byte IsBooked { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string NewAppointmentDate { get { return AppointmentDate.ToShortDateString(); } }
        public string NewFromTime { get { return FromTime.ToString("h:mm tt"); } }
        public string NewToTime { get { return ToTime.ToString("h:mm tt"); } }

    }

}
