﻿using System;
using System.Collections.Generic;
using System.Text;
namespace ParkingManager.DTO.ApplicationUserModule
{
    public class UpdatePasswordDTO
    {
        public string CurrentPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
