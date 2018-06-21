using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace ASFund.AutoTest
{
    /// <summary>
    /// Класс содержит вспомогательные функции для работы с файлами
    /// </summary>
    public static class FileHelper
    {
        private static string downloadsDirectory = $@"C:\Users\{Environment.UserName}\Downloads\";
        /// <summary>
        /// Ожидание загрузки файла в течение 60 секунд
        /// </summary>
        /// <param name="filename"></param>
        public static void WaitForDownload(string fileName)
        {
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout download");
                if ((File.Exists(downloadsDirectory + fileName))) break;
                Thread.Sleep(1000);
            }

        }

        /// <summary>
        /// Очистка заданной директории
        /// </summary>
        public static void ClearDownloadsDirectory()
        {
            DirectoryInfo DirInfo = new DirectoryInfo(downloadsDirectory);
            foreach (FileInfo file in DirInfo.GetFiles())
                file.Delete();
        }

        /// <summary>
        /// Перемещение файла из одной директории в другую 
        /// </summary>
        public static void MoveFile(string sourceDirectoty, string destenationDirictory, string fileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(destenationDirictory);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (file.Name == fileName)
                    file.Delete();
            }

            File.Move(sourceDirectoty + fileName, destenationDirictory + fileName);
        }

        /// <summary>
        /// Перемещение файла из одной директории в другую с переименованием файла  
        /// </summary>
        public static void MoveFileWithRename(string sourceDirectoty, string destenationDirectory, string fileName, string newFileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(destenationDirectory);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (file.Name == newFileName)
                    file.Delete();
            }

            File.Move(sourceDirectoty + fileName, destenationDirectory + newFileName);
        }

        /// <summary>
        /// Сравнение двух PDF файлов по string этих файлов
        /// </summary>
        public static void ComparePDFbyString(string testDirectoty, string productionDirectory, string fileName)
        {
            Stream licenseStream = LicenseStr.LicenseStream;
            new License().SetLicense(licenseStream);
            Document pdfDocumentTest = new Document(testDirectoty + fileName);
            TextAbsorber textAbsorberTest = new TextAbsorber();
            pdfDocumentTest.Pages.Accept(textAbsorberTest);
            string extractedTextTestDocument = textAbsorberTest.Text;
            Document pdfDocumentProduction = new Document(productionDirectory + fileName);
            TextAbsorber textAbsorberProduction = new TextAbsorber();
            pdfDocumentProduction.Pages.Accept(textAbsorberProduction);
            string extractedTextProductionDocument = textAbsorberProduction.Text;
            Assert.AreEqual(extractedTextTestDocument, extractedTextProductionDocument);
        }
    }
}
