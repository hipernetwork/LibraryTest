using System;
using CustomFile;

class Program
{
    static void Main(string[] args)
    {
        // CustomFileHandler sınıfını başlat
        var fileHandler = new CustomFileHandler();

        // Örnek bir XML dosyası oluştur ve işlemleri gerçekleştir
        string xmlFilePath = "example.xml";
        string content = "Bu bir XML örnek içeriktir.";

        // XML dosyasına yaz
        if (fileHandler.WriteXml(xmlFilePath, content))
        {
            Console.WriteLine("XML dosyasına yazma işlemi başarılı.");
        }
        else
        {
            Console.WriteLine("XML dosyasına yazma işlemi başarısız.");
        }

        // XML dosyasından oku
        try
        {
            string readContent = fileHandler.ReadXml(xmlFilePath);
            Console.WriteLine("XML dosyasından okunan içerik: " + readContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }
}
