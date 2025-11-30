using Business.Abstract;
using Business.Utils;
using DataAccess.Abstract;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReportManager : IReportService
    {
         IMemberAPIDal _memberAPIDal;
        public ReportManager(IMemberAPIDal memberAPIDal) 
        {
            _memberAPIDal = memberAPIDal;
        }


        public async Task<byte[]> CreateMemberReportAsync()
        {
            // 1. Üye listesini API'den asenkron olarak alın.
            var members = await _memberAPIDal.GetAllMembers();

            if (members == null || members.Count == 0)
            {
                // Üye listesi boşsa null veya boş bir dizi döndürün.
                System.Console.WriteLine("Üye bulunamadı. Boş bir rapor oluşturuldu.");
                return System.Array.Empty<byte>();
            }

            // 2. QuestPDF belgesini oluşturun ve verileri içine yerleştirin.
            var document = new MembersReportDocument(members);

            // 3. Belgeyi PDF formatında byte dizisi olarak oluşturun ve döndürün.
            var pdfBytes = document.GeneratePdf();

            System.Console.WriteLine("Üye raporu byte dizisi olarak başarıyla oluşturuldu.");

            return pdfBytes;
        }
    }
}
