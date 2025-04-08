using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace CustomFile
{
    public class CustomFile
    {
        private byte[] magicHeader;
        private byte[] ver;
        private byte[] rev;
    
        public CustomFile()
        {
            magicHeader = new byte[8] { 0x12, 0x00, 0x05, 0x07, 0x12, 0xA0, 0xFF, 0x00 };
            ver = new byte[2] { 0x05, 0x00 };
            rev = new byte[4] { 0, 0, 0, 0 };
        }
    
        internal bool InternalWrite(string filePath, byte[] content)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    if (fs.Length > 0)
                    {
                        fs.SetLength(0);
                    }
    
                    fs.Write(magicHeader, 0, magicHeader.Length);
                    fs.Write(ver, 0, ver.Length);
                    fs.Write(rev, 0, rev.Length);
                    fs.Write(content, 0, content.Length);
                    fs.Flush();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    
        public bool Write(string filePath, byte[] content)
        {
            return InternalWrite(filePath, content);
        }
    
    
        public bool WriteString(string filePath, string content)
        {
            byte[] conBytes = Encoding.UTF8.GetBytes(content);
            return InternalWrite(filePath, conBytes);
        }
    
        public byte[] Read(string filePath)
        {
            try { 
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fs.Read(magicHeader, 0, magicHeader.Length);
                    fs.Read(ver, 0, ver.Length);
                    fs.Read(rev, 0, rev.Length);
                    
    
                }
    
    
            }
            catch (Exception)
            {
                return null;
            }   
        }
    
    }
}
