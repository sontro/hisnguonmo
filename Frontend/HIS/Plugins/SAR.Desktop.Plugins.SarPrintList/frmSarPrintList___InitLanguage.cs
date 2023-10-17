using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using SAR.Desktop.Plugins.SarPrintList.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAR.Desktop.Plugins.SarPrintList
{
    public partial class frmSarPrintList : HIS.Desktop.Utility.FormBase
    {
        public void InitLanguage()
        {
            try
            {
                ////Khoi tao doi tuong resource
                ResourceLangManager.InitResourceLanguageManager();

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmSarPrintList.layoutControl1.Text", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
                this.gridColEdit.Caption = Inventec.Common.Resource.Get.Value("frmSarPrintList.gridColEdit.Caption", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
                this.gridColDelete.Caption = Inventec.Common.Resource.Get.Value("frmSarPrintList.gridColDelete.Caption", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
                this.gridColPrint.Caption = Inventec.Common.Resource.Get.Value("frmSarPrintList.gridColPrint.Caption", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
                this.gridColTitle.Caption = Inventec.Common.Resource.Get.Value("frmSarPrintList.gridColTitle.Caption", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
                this.gridColCreateTime.Caption = Inventec.Common.Resource.Get.Value("frmSarPrintList.gridColCreateTime.Caption", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
                this.gridColCreator.Caption = Inventec.Common.Resource.Get.Value("frmSarPrintList.gridColCreator.Caption", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
                this.gridColModifyTime.Caption = Inventec.Common.Resource.Get.Value("frmSarPrintList.gridColModifyTime.Caption", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
                this.gridColModifier.Caption = Inventec.Common.Resource.Get.Value("frmSarPrintList.gridColModifier.Caption", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
                this.Text = Inventec.Common.Resource.Get.Value("frmSarPrintList.Text", ResourceLangManager.LanguageFrmPrintList, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
