﻿using System;
using System.Collections.Generic;
using System.Text;
namespace ParkingManager.DTO.AppointmentModule
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Email { get; set; }
        public string FertilityEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string PaidByNumber { get; set; }
        public string CountryCode { get; set; }
        public byte Status { get; set; }
        public byte IsCompleted { get; set; }
        public byte IsApproved { get; set; }
        public byte IsBookingComplete { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ParkingSlotId { get; set; }
        public Guid OldTimeSlotId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string NewAppointmentDate { get; set; }
        public string NewAppDate { get { return AppointmentDate.ToShortDateString(); } }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string TransactionNumber { get; set; }
        public string TimeSlot { get; set; }     
        public string TransactionDate { get; set; }
        public string ReceiptURL { get; set; }
        public string VideoURL { get; set; }
        public decimal Amount { get; set; }
        public string ApprovedBy { get; set; }
        public string RescheduledBy { get; set; }
        public string ReceiptNo { get; set; }
        public int No { get; set; }
        public string NewAmount { get { return Amount.ToString("N"); } }

    }
}
