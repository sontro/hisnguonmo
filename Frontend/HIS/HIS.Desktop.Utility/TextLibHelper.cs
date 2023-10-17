using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Utility
{
    public class TextLibHelper
    {
        public static string BytesToStringConverted(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        static Telerik.WinForms.Documents.Model.RadDocument DocImport(byte[] bytes)
        {
            Telerik.WinForms.Documents.FormatProviders.Rtf.RtfFormatProvider provider = new Telerik.WinForms.Documents.FormatProviders.Rtf.RtfFormatProvider();
            return provider.Import(bytes);
        }

        public static string BytesToStringTelerik(byte[] bytes)
        {
            string data = "";
            try
            {
                var doc = DocImport(bytes);
                Telerik.WinForms.Documents.FormatProviders.Txt.TxtFormatProvider providerExport = new Telerik.WinForms.Documents.FormatProviders.Txt.TxtFormatProvider();
                data = providerExport.Export(doc);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return data;
        }

        public static string BytesToRtfTextTelerik(byte[] bytes)
        {
            string data = "";
            try
            {
                var doc = DocImport(bytes);
                Telerik.WinForms.Documents.FormatProviders.Rtf.RtfFormatProvider provider = new Telerik.WinForms.Documents.FormatProviders.Rtf.RtfFormatProvider();
                data = provider.Export(doc);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return data;
        }

        public static string BytesToHtmlTextTelerik(byte[] bytes)
        {
            string data = "";
            try
            {
                var doc = DocImport(bytes);
                Telerik.WinForms.Documents.FormatProviders.Html.HtmlFormatProvider provider = new Telerik.WinForms.Documents.FormatProviders.Html.HtmlFormatProvider();
                data = provider.Export(doc);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return data;
        }

        public static string BytesToString(byte[] bytes)
        {
            string data = "";
            using (var stream = new MemoryStream(bytes))
            {
                stream.Position = 0;
                DevExpress.XtraRichEdit.RichEditControl editControl = new DevExpress.XtraRichEdit.RichEditControl();
                editControl.DragDropMode = DevExpress.XtraRichEdit.DragDropMode.Manual;
                editControl.LoadDocument(stream, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                data = editControl.Text;
            }

            return data;
        }

        public static string BytesToRtfText(byte[] bytes)
        {
            string data = "";
            using (var stream = new MemoryStream(bytes))
            {
                stream.Position = 0;
                DevExpress.XtraRichEdit.RichEditControl editControl = new DevExpress.XtraRichEdit.RichEditControl();
                editControl.DragDropMode = DevExpress.XtraRichEdit.DragDropMode.Manual;
                editControl.LoadDocument(stream, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                data = editControl.RtfText;
            }

            return data;
        }

        public static string BytesToHtmlText(byte[] bytes)
        {
            string data = "";
            using (var stream = new MemoryStream(bytes))
            {
                stream.Position = 0;
                DevExpress.XtraRichEdit.RichEditControl editControl = new DevExpress.XtraRichEdit.RichEditControl();
                editControl.DragDropMode = DevExpress.XtraRichEdit.DragDropMode.Manual;
                editControl.LoadDocument(stream, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                data = editControl.HtmlText;
            }

            return data;
        }

        public static string BytesToStringGeneral(byte[] bytes, int libType)
        {
            string data = "";
            try
            {
                if (libType == ShortcutReplace.LIB_TYPE_DEV)
                {
                    data = BytesToString(bytes);
                }
                else if (libType == ShortcutReplace.LIB_TYPE_TELERIK)
                {
                    data = BytesToStringTelerik(bytes);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return data;
        }

        public static string BytesToRtfTextGeneral(byte[] bytes, int libType)
        {
            string data = "";
            try
            {
                if (libType == ShortcutReplace.LIB_TYPE_DEV)
                {
                    data = BytesToRtfText(bytes);
                }
                else if (libType == ShortcutReplace.LIB_TYPE_TELERIK)
                {
                    data = BytesToRtfTextTelerik(bytes);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return data;
        }

        public static string BytesToHtmlTextGeneral(byte[] bytes, int libType)
        {
            string data = "";
            try
            {
                if (libType == ShortcutReplace.LIB_TYPE_DEV)
                {
                    data = BytesToHtmlText(bytes);
                }
                else if (libType == ShortcutReplace.LIB_TYPE_TELERIK)
                {
                    data = BytesToHtmlTextTelerik(bytes);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

            return data;
        }
    }
}
