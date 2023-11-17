using iText.IO.Image;
using iText.Layout;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Text;
using WebApplication5.Models;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PdfGenerator _pdfGenerator;
        private readonly Context _context;

        public PdfController(PdfGenerator pdfGenerator, Context context)
        {
            _pdfGenerator = pdfGenerator;
            _context = context;
        }


        [HttpPost]
        public IActionResult GeneratePdf()
        {
            string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

            var dataForTable1 = _context.Products.ToList(); 
            decimal grandTotal = dataForTable1.Sum(p => p.Total);

            var dataForTable2 = _context.Bills.ToList();
            var paymentData = _context.Payments.FirstOrDefault();
            var companyData = _context.Compans.FirstOrDefault();
            var customerData = _context.Customers.FirstOrDefault();


            StringBuilder tableRows = new StringBuilder();// StringBuilder kullanarak HTML satırlarını oluştur
            StringBuilder tableRows2 = new StringBuilder();
            StringBuilder listItems = new StringBuilder();
            StringBuilder listItems2 = new StringBuilder();
            StringBuilder listItems3 = new StringBuilder();

            //payment
            listItems.AppendLine("<h5 style=\"color: darkblue;\">Ödeme Bilgileri</h5>");
            listItems.AppendLine("<li>Hesap Adı: " + paymentData.AccountName + "</li>");
            listItems.AppendLine("<li>Hesap No: " + paymentData.AccountNumber + "</li>");
            listItems.AppendLine("<li>Ödeme Vadesi: " + paymentData.DueDate.ToString("dd MMMM yyyy") + "</li>");


            //company
            listItems2.AppendLine("<li style=\"font-size: 18px;\"><b>Deneme Teknoloji Şirketi A.Ş.</b></li>");
            listItems2.AppendLine("<li>" + companyData.Adress + "</li>");
            listItems2.AppendLine("<li>Web Sitesi: " + companyData.WebSite + "</li>");
            listItems2.AppendLine("<li>Vergi Dairesi: " + companyData.TaxOffice + "</li>");
            listItems2.AppendLine("<li>VKN: " + companyData.TaxNu + "</li>");

            //customer
            listItems3.AppendLine("<li style=\"font-size: 18px;\"><b>Sayın</b></li>");
            listItems3.AppendLine("<li> " + customerData.CustomerName + "</li>");
            listItems3.AppendLine("<li>" + customerData.Adress+"<br> "+customerData.Adress2 + "</li><br>");
            listItems3.AppendLine("<li> " + customerData.Adress3+ "</li>");
            listItems3.AppendLine("<li>Vergi Dairesi: " + customerData.TaxOffice + "</li>");
            listItems3.AppendLine("<li>VKN: " + customerData.TaxNu + "</li>");



            foreach (var data in dataForTable2)
            {
                tableRows2.AppendLine("<tr>");
                tableRows2.AppendLine($"<th>Özelleştirme No:</th><td>{data.OzNo}</td>");
                tableRows2.AppendLine("</tr>");

                tableRows2.AppendLine("<tr>");
                tableRows2.AppendLine($"<th>Senaryo:</th><td>{data.BillName}</td>");
                tableRows2.AppendLine("</tr>");

                tableRows2.AppendLine("<tr>");
                tableRows2.AppendLine($"<th>Fatura Tipi:</th><td>{data.BillType}</td>");
                tableRows2.AppendLine("</tr>");

                tableRows2.AppendLine("<tr>");
                tableRows2.AppendLine($"<th>Fatura No:</th><td>{data.BillNu}</td>");
                tableRows2.AppendLine("</tr>");

                tableRows2.AppendLine("<tr>");
                tableRows2.AppendLine($"<th>Fatura Tarihi:</th><td>{data.Date.ToString("dd-MM-yyyy")}</td>");
                tableRows2.AppendLine("</tr>");
            }

            foreach (var data2 in dataForTable1)
            {
                tableRows.AppendLine("<tr>");
                tableRows.AppendLine($"<td>{data2.Name}</td>");
                tableRows.AppendLine($"<td>{data2.Description}</td>");
                tableRows.AppendLine($"<td>{data2.Quantity}</td>");
                tableRows.AppendLine($"<td>${data2.UnitPrice}</td>");
                tableRows.AppendLine($"<td>${data2.Total}</td>");
                tableRows.AppendLine("</tr>");
            }

            string imageFile = "https://yardim.orkestra.com.tr/usr_img/usr/contents/fatura_tasarim/gib_400px.png";
            string imageFile2 = "C:\\Users\\PC_3829\\source\\repos\\WebApplication5\\WebApplication5\\Images\\deneme.jpg";

            // <link rel=""stylesheet"" href=""C:\Users\PC_3829\Desktop\DinkToGuncel\WebApplication5\wwwroot\Css\Styles.css"">

            string customHtmlContent = $@"
            <html>
                   <html lang=""en"">
<head>
    <title>PDF Example</title>
    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"">

    <style>
        body {{
            font-family: 'Arial', sans-serif;
            background-color: #f8f9fa;
        }}

        .container {{
            background-color: #fff;
            padding: 30px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 10px;
            margin-top: 50px;
        }}

        .invoice-header {{
            align-items: center;
            margin-bottom:-30px;
        }}

        .invoice-header img {{
            width: 150px;
            height: 150px;
            display: inline-block;
            border-radius: 50%;
            margin-top: -120px;

        }}

        .invoice-details {{
            display: flex;
            justify-content: space-between;
        }}

        .invoice-table {{
            width: 100%;
            margin-top: 60px;
            border-collapse: collapse;
        }}

        .invoice-table th, .invoice-table td {{
            border: 1px solid #dee2e6;
            padding: 12px;
            text-align: left;
        }}

        .invoice-table th {{
            background-color: #f8f9fa;
        }}
        .company-table {{
             margin-left: 550px; 
             margin-top:-270px;
             width:380px;""

                }}
        .invoice-total {{
            font-weight: bold;
        }}
       
       
        .additional-info {{
            margin-top: 140px;
        }}
        .top-right {{
            position: absolute;
            top: 180px;
            right: 3%;
           font-size: 5px;  
              
           }}
        h5{{
            font-weight: bold;
        }}
        .line {{
            width: 100%;
            border-top: 2px solid rgb(3, 81, 175);
            margin-top: 10px;
            margin-bottom:5px;
            
        }}
        .line2 {{
            width: 35%;
            border-top: 2px solid rgb(3, 81, 175); 
            margin-bottom:10px;
            
        }}
        ul{{
            list-style: none;

        }}
        li{{
            margin-left: -37px;
        }}
       .img1{{
               margin-left:360px;
               margin-right: 230px;
               text-align: center;
               display: inline-block; 
                }}
        .img2{{
               text-align: center;
               display: inline-block;
               }}
     
     </style>

   </head>

  <body>

<div class=""container"">

    <div class=""invoice-header"">

        <h3>DENEME<br> TEKNOLOJİ<br> ŞİRKETİ A.Ş.</h3>

        <img src=""{imageFile}""  alt=""Resim bulunamadı"" class=""img1 ""/> 
        <img src=""{imageFile2}"" alt=""Resim bulunamadı"" class=""img2 ""/> 

    </div>

    <p style=""margin-left: 400px; margin-top:30px;"">e-Fatura</p>

    <div class=""invoice-details"">

        <div class=""top-right "">

             <h5>Tarih:<b>{currentDate}</b> </h5>

             <div class=""line""></div>

        </div>

        <div style=""margin-top:-30px;"">

             <div class=""line2""></div>

             <ul>

                {listItems2.ToString()}

             </ul>

             <div class=""line2""></div>

             <ul>

                  {listItems3.ToString()}

             </ul>

             <div class=""line2""></div>

        </div>


        <div>
        
        <table class=""table table-light company-table"" >

           <tbody>

                   {tableRows2.ToString()}

           </tbody> 

        </table>

        </div>

    </div>
     
    <table class=""table invoice-table"">

        <thead>

            <tr>

                <th>Ürün</th>
                <th>Açıklama</th>
                <th>Miktar</th>
                <th>Birim Fiyat</th>
                <th>Toplam</th>

            </tr>

        </thead>

        <tbody>

            {tableRows.ToString()}         
 
        </tbody>

        <tfoot>

            <tr>

                <td colspan=""4"" class=""invoice-total"">Toplam</td>
                <td class=""invoice-total"">${grandTotal}</td>

            </tr>

        </tfoot>

    </table>
    
    <div class=""modal-footer"">

        <ul>

            {listItems.ToString()}

        </ul>

    </div> <br>
  
    
    <div class=""card-footer"">

      <ul>
        
        <li>
           İşbu faturayı kabul etmekle faturaya konu teşil den satış işleminin Türkiye Cumhuriyeti sınırlarındaki firmanız ile yapıldığını 
            ve satın almış olduğunuz söz konusu ürünlerin satışın hiçbir koşulda Türkiye Cumhuriyeti sınırlarının dışına
            gerçekleştirmeyeceğinizi kabul beyan ve taahüt etmiş olmaktasınız.<br><br>
            Fatura 5 gün içinde itiraz edilmezse kabul edilmiş sayılır.
        </li>

      </ul>

    </div>

   </div>

 </body>

</html>
        ";
            byte[] pdfBytes = _pdfGenerator.GeneratorPdf(customHtmlContent);
            var pdfContentString = Encoding.UTF8.GetString(pdfBytes);

            return File(pdfBytes, "application/pdf", "generated. pdf");
        }

    }
}
