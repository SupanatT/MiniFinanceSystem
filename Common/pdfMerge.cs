using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MiniFinance.Common
{
    public static class PdfMerger
    {
        /// <summary>
        /// Merge pdf files.
        /// </summary>
        /// <param name="sourceFiles">PDF files being merged.</param>
        /// <returns></returns>
        public static byte[] MergeFiles(List<byte[]> sourceFiles)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            using (MemoryStream ms = new MemoryStream())
            {
                PdfCopy copy = new PdfCopy(document, ms);
                document.Open();
                int documentPageCounter = 0;

                // Iterate through all pdf documents
                for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
                {
                    // Create pdf reader
                    PdfReader reader = new PdfReader(sourceFiles[fileCounter]);
                    int numberOfPages = reader.NumberOfPages;

                    // Iterate through all pages
                    for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                    {
                        documentPageCounter++;
                        PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
                        PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);

                        //// Write header
                        //ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                        //    new Phrase("PDF Merger by Helvetic Solutions"), importedPage.Width / 2, importedPage.Height - 30,
                        //    importedPage.Width < importedPage.Height ? 0 : 1);

                        //// Write footer
                        //ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                        //    new Phrase(String.Format("Page {0}", documentPageCounter)), importedPage.Width / 2, 30,
                        //    importedPage.Width < importedPage.Height ? 0 : 1);

                        pageStamp.AlterContents();

                        copy.AddPage(importedPage);
                    }

                    copy.FreeReader(reader);
                    reader.Close();
                }

                document.Close();
                return ms.GetBuffer();
            }
        }

        public static byte[] MergeFiles(List<string> pdfFile, ref List<string> lostFile)
        {
            List<byte[]> sourceFiles = new List<byte[]>();
            lostFile = new List<string>();
            try
            {
                for (int fileCounter = 0; fileCounter < pdfFile.Count; fileCounter++)
                {
                    try
                    {
                        if (File.Exists(pdfFile[fileCounter]))
                            sourceFiles.Add(File.ReadAllBytes(pdfFile[fileCounter]));
                    }
                    catch
                    {
                        lostFile.Add(pdfFile[fileCounter]);
                    }
                }
                return MergeFiles(sourceFiles);
            }
            catch
            {
                throw;
            }
        }

        public static List<string> SplitePdf(string pdfFile)
        {
            List<byte[]> sourceFiles = new List<byte[]>();
            List<string> lsNewFilePath = new List<string>();
            byte[] btyeFile = null;

            iTextSharp.text.Document document = null;
            PdfCopy copy = null;
            try
            {
                if (File.Exists(pdfFile))
                {
                    btyeFile = File.ReadAllBytes(pdfFile);


                    int documentPageCounter = 0;

                    // Create pdf reader
                    PdfReader reader = new PdfReader(btyeFile);
                    int numberOfPages = reader.NumberOfPages;

                    // Iterate through all pages
                    string pathPDF = "";
                    for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                    {
                        pathPDF = Path.Combine(Path.GetDirectoryName(pdfFile), Path.GetFileNameWithoutExtension(pdfFile) + "_" + currentPageIndex + Path.GetExtension(pdfFile));
                        using (FileStream stream = new FileStream(pathPDF, FileMode.Create))
                        {
                            document = new iTextSharp.text.Document();
                            copy = new PdfCopy(document, stream);
                            document.Open();

                            documentPageCounter++;
                            PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
                            PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);
                            pageStamp.AlterContents();
                            copy.AddPage(importedPage);
                            copy.Close();
                        }

                        lsNewFilePath.Add(pathPDF);
                    }

                    reader.Close();
                    document.Close();
                }

                return lsNewFilePath;
            }
            catch
            {
                throw;
            }
        }
    }
}
