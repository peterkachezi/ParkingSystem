﻿//using ParkingManager.BLL.Repositories.AppointmentModule;
//using Microsoft.AspNetCore.Mvc;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using System.Drawing;

//namespace ParkingManager.Web.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class AppointmentReportsController : Controller
//    {
//        private readonly IAppointmentRepository appointmentRepository;

//        public AppointmentReportsController(IAppointmentRepository appointmentRepository)
//        {
//            this.appointmentRepository = appointmentRepository;
//        }
//        public IActionResult Index()
//        {
//            try
//            {
//                return View();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex);

//                return null;
//            }
//        }

//        public async Task<IActionResult> DownloadAllAppointment()
//        {
//            //var user1 = await userManager.FindByEmailAsync(User.Identity.Name);
//            // Get the user list 

//            var users = await appointmentRepository.GetAll();

//            var stream = new MemoryStream();

//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//            using (var xlPackage = new ExcelPackage(stream))
//            {
//                var worksheet = xlPackage.Workbook.Worksheets.Add("Users");

//                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");

//                namedStyle.Style.Font.UnderLine = true;

//                namedStyle.Style.Font.Color.SetColor(Color.Blue);

//                const int startRow = 5;

//                var row = startRow;

//                //Create Headers and format them
//                worksheet.Cells["A1,B1,C1,D1"].Value = "Appointment Report";

//                using (var r = worksheet.Cells["A1:D1"])
//                {
//                    r.Merge = true;

//                    r.Style.Font.Color.SetColor(Color.White);

//                    r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;

//                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;

//                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));

//                }

//                worksheet.Cells["A2"].Value = "FullName";

//                worksheet.Cells["B2"].Value = "PhoneNumber";

//                worksheet.Cells["C2"].Value = "Email";  

//                worksheet.Cells["D2"].Value = "CreateDate";            

//                worksheet.Cells["A2:D2"].Style.Fill.PatternType = ExcelFillStyle.Solid;

//                worksheet.Cells["A2:D2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));

//                worksheet.Cells["A2:D2"].Style.Font.Bold = true;

//                row = 3;

//                foreach (var user in users)
//                {
//                    worksheet.Cells[row, 1].Value = user.FullName;

//                    worksheet.Cells[row, 2].Value = user.PhoneNumber;

//                    worksheet.Cells[row, 3].Value = user.Email;

//                    worksheet.Cells[row, 4].Value = user.CreateDate.ToShortDateString();

//                    row++;
//                }

//                // set some core property values
//                xlPackage.Workbook.Properties.Title = "FP";

//                xlPackage.Workbook.Properties.Author = "FP";

//                xlPackage.Workbook.Properties.Subject = "FP";
//                // save the new spreadsheet
//                xlPackage.Save();
//                // Response.Clear();
//            }
//            stream.Position = 0;
            
//            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AppointmentReport.xlsx");
//        }

//    }
//}
