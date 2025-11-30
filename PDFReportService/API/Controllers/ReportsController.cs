using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GeneratePdfReport()
        {
            // Business katmanındaki servisi çağırarak PDF'i byte dizisi olarak al
            var pdfBytes = await _reportService.CreateMemberReportAsync();

            // PDF dosyasını HTTP cevabı olarak döndür
            return File(pdfBytes, "application/pdf", "MemberReport.pdf");
        }
    }
}

