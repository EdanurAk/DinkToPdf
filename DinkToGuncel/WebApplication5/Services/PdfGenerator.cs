using DinkToPdf.Contracts;
using DinkToPdf;

namespace WebApplication5.Services
{
    public class PdfGenerator
    {
        private readonly IConverter _converter;
        public PdfGenerator(IConverter converter)
        {
            _converter = converter;
        }
        public byte[] GeneratorPdf(string htmlContent)
        {
            var globalSettings = new GlobalSettings//PDF belgesinin genel ayarlarını içerir. Bu ayarlar belge boyutu, renk modu, kenar boşlukları 
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 5, Right = 5 },
                DocumentTitle = "Generated PDF"
            };
            var objectSettings = new ObjectSettings()//dönüştürülecek HTML içeriği ve belgenin diğer özelliklerini içerir. Örneğin, sayfa sayısı, sayfa başlığı
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontSize = 10, Right = "Sayfa [page] / [toPage]", Line = true, Spacing = 2.812 },
                FooterSettings = { FontSize = 10, Line = true, Right = ""+ DateTime.Now.Year }
            };
            var document = new HtmlToPdfDocument()//genel ve nesne ayarlarını bir araya getirir ve PDF belgesini oluşturmak için kullanılır.
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }

            };

            return _converter.Convert(document);// belgeyi PDF formatına dönüştürmek için kullanılır ve dönüştürülen PDF verilerini byte dizisi olarak döndürür

        }
    }
}
