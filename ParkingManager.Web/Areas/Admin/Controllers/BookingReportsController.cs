using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ParkingManager.BLL.Repositories.BookingModule;
using System.Drawing;

namespace ParkingManager.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingReportsController : Controller
    {
        private readonly IBookingRepository bookingRepository;

        public BookingReportsController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return null;
            }
        }

        public async Task<IActionResult> DownloadAllAppointment()
        {
            //var user1 = await userManager.FindByEmailAsync(User.Identity.Name);
            // Get the user list 
            try
            {

                var users = await bookingRepository.GetAll();

                if (users.Count == 0)
                {
                    TempData["Error"] = "There are no reports to display";

                    return RedirectToAction("Login", "Account");
                }

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("Users");

                    var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");

                    namedStyle.Style.Font.UnderLine = true;

                    namedStyle.Style.Font.Color.SetColor(Color.Blue);

                    const int startRow = 5;

                    var row = startRow;

                    //Create Headers and format them
                    worksheet.Cells["A1,B1,C1,D1,E1"].Value = "Booking Reports";

                    using (var r = worksheet.Cells["A1:E1"])
                    {
                        r.Merge = true;

                        r.Style.Font.Color.SetColor(Color.White);

                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;

                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;

                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));

                    }

                    worksheet.Cells["A2"].Value = "FullName";

                    worksheet.Cells["B2"].Value = "PhoneNumber";

                    worksheet.Cells["C2"].Value = "SlotName";

                    worksheet.Cells["D2"].Value = "Email";

                    worksheet.Cells["E2"].Value = "CreateDate";

                    worksheet.Cells["A2:E2"].Style.Fill.PatternType = ExcelFillStyle.Solid;

                    worksheet.Cells["A2:E2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));

                    worksheet.Cells["A2:E2"].Style.Font.Bold = true;

                    row = 3;

                    foreach (var user in users)
                    {
                        worksheet.Cells[row, 1].Value = user.FullName;

                        worksheet.Cells[row, 2].Value = user.PhoneNumber;

                        worksheet.Cells[row, 3].Value = user.SlotName;

                        worksheet.Cells[row, 4].Value = user.Email;

                        worksheet.Cells[row, 5].Value = user.CreateDate.ToShortDateString();

                        row++;
                    }

                    // set some core property values
                    xlPackage.Workbook.Properties.Title = "FP";

                    xlPackage.Workbook.Properties.Author = "FP";

                    xlPackage.Workbook.Properties.Subject = "FP";
                    // save the new spreadsheet
                    xlPackage.Save();
                    // Response.Clear();
                }
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BookingReports.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                TempData["Error"] = "Something went wrong";

                return RedirectToAction("Login", "Account", new { area = "" });
            }
        }

    }
}
