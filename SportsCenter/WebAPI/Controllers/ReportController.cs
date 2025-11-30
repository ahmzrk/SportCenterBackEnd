using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public ReportController(IHttpClientFactory httpClientFactory)
        {
            // fabrika ile bir HttpClient nesnesi oluştururuz
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpGet("download-report")]
        public async Task<IActionResult> DownloadMemberReport()
        {
            // Raporlama mikroservisinin adresini ve endpoint'ini belirtin
            var reportServiceUrl = "https://localhost:44390/api/Reports/generate";

            // HTTP isteği gönderin
            var response = await _httpClient.GetAsync(reportServiceUrl);

            // Cevabı kontrol edin
            if (response.IsSuccessStatusCode)
            {
                // Başarılıysa, gelen byte dizisini okuyun
                var pdfBytes = await response.Content.ReadAsByteArrayAsync();

                // PDF dosyasını kullanıcıya döndürün
                return File(pdfBytes, "application/pdf", "MemberReport.pdf");
            }

            // Başarısız olursa bir hata döndürün
            return BadRequest("Rapor oluşturulurken bir hata oluştu.");
        }
    }
}
