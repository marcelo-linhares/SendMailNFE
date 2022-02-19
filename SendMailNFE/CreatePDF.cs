using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Office.Interop.Word;

namespace SendMailNFE
{
    class CreatePDF
    {

        public void GenerateReport(string FileNameXML, Dictionary<string, string> dicConfigXMLNFE, string MainPathXML, string FileNameDest, string FileNameTemplate)
        {
            StreamReader oSR = new StreamReader(FileNameXML);
            String TextXML = oSR.ReadToEnd().Replace("xmlns=\"http://www.portalfiscal.inf.br/nfe\"", "");
            // Utilizando o xml para preecher o objeto de dados
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(TextXML);
            Dictionary<string, string> dicFinal = new Dictionary<string, string>();

            foreach (KeyValuePair<string,string> item in dicConfigXMLNFE)
            {
                //Incluir tratamento para tamanho, mascara, entre outros, neste campo.
                if(xmlDoc.SelectSingleNode(MainPathXML + item.Value)!=null)
                    dicFinal.Add(item.Key, xmlDoc.SelectSingleNode(MainPathXML + item.Value).InnerText);
                else
                    dicFinal.Add(item.Key, String.Empty);
            }

            oSR.Close();
            
            if (TransformNFEDOC(FileNameTemplate, FileNameDest.Insert(FileNameDest.Length, ".docx"), dicFinal, FileNameXML))
            {
                return;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateDoc"></param>
        /// <param name="FileName"></param>
        /// <param name="Dic"></param>
        /// <returns></returns>
        /// <refCopyRight>https://stackoverflow.com/questions/50117531/generate-a-word-document-docx-using-data-from-an-xml-file-convert-xml-to-a-w</refCopyRight>
        private Boolean TransformNFEDOC(string templateDoc, string FileName, Dictionary<string, string> Dic, string xmlDataFile)
        {
            File.Copy(templateDoc, FileName, true);

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(FileName, true))
            {
                //get the main part of the document which contains CustomXMLParts
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;

                //delete all CustomXMLParts in the document. If needed only specific CustomXMLParts can be deleted using the CustomXmlParts IEnumerable
                mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

                //add new CustomXMLPart with data from new XML file
                CustomXmlPart myXmlPart = mainPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);
                using (FileStream stream = new FileStream(xmlDataFile, FileMode.Open))
                {
                    myXmlPart.FeedData(stream);
                }

                /*string docText = null;

                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                foreach (KeyValuePair<string, string> item in Dic)
                {
                    Regex regexText = new Regex(item.Key);
                    docText = regexText.Replace(docText, item.Value);
                }

                using (StreamWriter sw = new StreamWriter(
                            wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }*/
                
            }

            GeneratePDF(FileName);
            return true;
        }

        private static void GeneratePDF(string FileNameDOCX)
        {


            // Create a new Microsoft Word application object
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

            // C# doesn't have optional arguments so we'll need a dummy value
            object oMissing = System.Reflection.Missing.Value;

            // Get list of Word files in specified directory
            //DirectoryInfo dirInfo = new DirectoryInfo(@"C:\DANFE");
            //FileInfo[] wordFiles = dirInfo.GetFiles(FileNameDOCX);

            //FileInfo wordFile = new FileInfo(FileNameDOCX);

            word.Visible = false;
            word.ScreenUpdating = false;

            Object filename = (Object)FileNameDOCX;

            Document doc = word.Documents.Open(ref filename);
            doc.Activate();

            object outputFileName = FileNameDOCX.Replace(".docx", ".pdf"); //wordFile.FullName.Replace(".docx", ".pdf");
            object fileFormat = WdSaveFormat.wdFormatPDF;

            // Save document into PDF Format
            doc.SaveAs(ref outputFileName,
                ref fileFormat, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            // Close the Word document, but leave the Word application open.
            // doc has to be cast to type _Document so that it will find the
            // correct Close method.                
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
            doc = null;


            //foreach (FileInfo wordFile in wordFiles)
            //{
            //    // Cast as Object for word Open method
            //    Object filename = (Object)wordFile.FullName;

            //    // Use the dummy value as a placeholder for optional arguments
            //    Document doc = word.Documents.Open(ref filename);
            //    doc.Activate();

            //    object outputFileName = wordFile.FullName.Replace(".doc", ".pdf");
            //    object fileFormat = WdSaveFormat.wdFormatPDF;

            //    // Save document into PDF Format
            //    doc.SaveAs(ref outputFileName,
            //        ref fileFormat, ref oMissing, ref oMissing,
            //        ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //        ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //        ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            //    // Close the Word document, but leave the Word application open.
            //    // doc has to be cast to type _Document so that it will find the
            //    // correct Close method.                
            //    object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            //    ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
            //    doc = null;
            //}

            // word has to be cast to type _Application so that it will find
            // the correct Quit method.
            ((_Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
            word = null;

        }

    }
}
