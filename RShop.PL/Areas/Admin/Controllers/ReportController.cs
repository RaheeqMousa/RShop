using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.BLL.Services.Classes;

namespace RShop.PL.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin, SuperAdmin")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService) {
            _reportService = reportService;
        }

        [HttpGet("PdfProduct")]
        public IActionResult GetPdfProductReport()
        {
            var pdf = _reportService.GenerateProductReports();
            return File(pdf, "application/pdf", "RShop.pdf");
        }

    }
}
