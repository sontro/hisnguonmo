using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraRichEdit.API.Native;
using System.Resources;

namespace HIS.Desktop.Plugins.ServiceExecute
{
    public partial class UcWordFull : UserControl
    {
        DocumentRange rangeAdmins = null;
        Action<decimal> actChangeZoom;
        public UcWordFull(Action<decimal> actChangeZoom)
        {
            InitializeComponent();
            this.actChangeZoom = actChangeZoom;
            this.txtDescription.Options.CopyPaste.InsertOptions = InsertOptions.KeepSourceFormatting;
            this.SetCaptionByLanguageKey();
        }

        private void txtDescription_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //WordProcess.zoomFactor(txtDescription);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtDescription_ZoomChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.actChangeZoom != null)
                {
                    this.actChangeZoom((decimal)txtDescription.ActiveView.ZoomFactor);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void CreateRange(string rangeOld)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(rangeOld))
                {
                    var ps = rangeOld.Split(':');
                    if (ps.Length == 2)
                    {
                        int start = Inventec.Common.TypeConvert.Parse.ToInt32(ps[0]);
                        int length = Inventec.Common.TypeConvert.Parse.ToInt32(ps[1]);
                        if (start > 0 && length > 0)
                        {
                            rangeAdmins = txtDescription.Document.CreateRange(start, length);
                        }
                    }
                }
                else if (rangeAdmins == null)
                {
                    DevExpress.XtraRichEdit.API.Native.DocumentRange[] ranges = txtDescription.Document.FindAll(ServiceExecuteCFG.keyXml, SearchOptions.None);
                    if (ranges != null && ranges.Length > 1)
                    {
                        //từ vị trí 0 đến vị trí key begin đầu tiên sẽ cho phép nhập.
                        int start0 = ranges.First().Start.ToInt();
                        int length0 = ranges.Last().Start.ToInt() - start0;
                        rangeAdmins = txtDescription.Document.CreateRange(start0, length0);
                    }

                    txtDescription.Document.ReplaceAll(ServiceExecuteCFG.keyXml, " ", SearchOptions.CaseSensitive);
                }
                else// clear nếu không có dữ liệu cũ
                {
                    rangeAdmins = null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal string GetRange()
        {
            string result = "";
            try
            {
                if (rangeAdmins != null)
                {
                    result = string.Format("{0}:{1}", rangeAdmins.Start.ToInt(), rangeAdmins.Length);
                }
            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        internal string GetDataRange()
        {
            string result = null;
            try
            {
                if (rangeAdmins != null)
                {
                    result = this.txtDescription.Document.GetText(rangeAdmins);
                }
                else
                {
                    result = this.txtDescription.Text;
                }
            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        ///Hàm xét ngôn ngữ cho giao diện UcWordFull
        /// </summary>
        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResourceUcWordFull = new ResourceManager("HIS.Desktop.Plugins.ServiceExecute.Resources.Lang", typeof(UcWordFull).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
