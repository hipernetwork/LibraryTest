using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace CustomFile
{
    public class CustomFileHandler
    {
        // Magic Header sabiti
        private static readonly byte[] MagicHeader = { 0x12, 0x00, 0x05, 0x07, 0x12, 0xA0, 0xFF, 0x00 };

        // Mevcut metotlar burada...

        /// <summary>
        /// String içeriği XML formatında dosyaya yazar.
        /// </summary>
        public bool WriteXml(string filePath, string content)
        {
            try
            {
                XDocument xmlDocument;

                // Eğer dosya mevcutsa yükle, değilse yeni bir XML belgesi oluştur
                if (File.Exists(filePath))
                {
                    xmlDocument = XDocument.Load(filePath);
                }
                else
                {
                    xmlDocument = new XDocument(
                        new XElement("CustomFile",
                        new XElement("Header", Convert.ToBase64String(MagicHeader)),
                        new XElement("Version", "1.0"),
                        new XElement("Content", content)
                    )
                );

                    // XML belgesinin kök elemanını oluştur
                    //xmlDocument = new XDocument(new XElement("CustomFile"));
                }

                // Yeni içerik ekle

                xmlDocument.Root?.Add(
                        new XElement("CustomFile",
                        new XElement("Header", Convert.ToBase64String(MagicHeader)),
                        new XElement("Version", "1.0"),
                        new XElement("Content", content)
                    )
                );


                //xmlDocument.Root?.Add(new XElement("Entry",
                //    new XElement("Content", content)
                //    ));


                //xmlDocument.Root?.Add(new XElement("Entry",
                //    new XElement("Content", content)
                //));

                // XML'i dosyaya kaydet
                xmlDocument.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// XML formatındaki dosyadan string içeriği okur.
        /// </summary>
        public string ReadXml(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Dosya bulunamadı.", filePath);

            try
            {
                // XML belgesini yükle
                var xmlDocument = XDocument.Load(filePath);

                // İçeriği al
                var contentElement = xmlDocument.Root?.Element("Content");
                return contentElement?.Value ?? string.Empty;
            }
            catch
            {
                throw new InvalidOperationException("XML dosyası okunamadı.");
            }
        }
    }
}
