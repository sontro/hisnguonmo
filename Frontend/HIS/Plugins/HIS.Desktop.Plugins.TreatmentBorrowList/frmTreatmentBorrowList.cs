using Inventec.Desktop.Common.LanguageManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentBorrowList
{
    public partial class frmTreatmentBorrowList : HIS.Desktop.Utility.FormBase
    {

        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentBorrowTypeId;


        public frmTreatmentBorrowList()
        {
            InitializeComponent();
        }

        public frmTreatmentBorrowList(Inventec.Desktop.Common.Modules.Module _module, long _treatmentBorrowTypeId)
            : base(_module)
        {
            InitializeComponent();
            try
            {

                this.treatmentBorrowTypeId = _treatmentBorrowTypeId;
                this.currentModule = _module;
                SetIcon();
                this.Text = this.currentModule.text;

                if (treatmentBorrowTypeId != null && treatmentBorrowTypeId > 0)
                {
                    UCTreatmentBorrowList uc = new UCTreatmentBorrowList(this.currentModule);
                    if (uc != null)
                    {
                        this.panelControlTreatmentBorrowList.Controls.Add(uc);
                        uc.Dock = DockStyle.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmTreatmentBorrowList_Load(object sender, EventArgs e)
        {
            try
            {
                SetCaptionByLanguageKey();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.TreatmentBorrowList.Resources.Lang", typeof(HIS.Desktop.Plugins.TreatmentBorrowList.frmTreatmentBorrowList).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmTreatmentBorrowList.layoutControl1.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void SetIcon()
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(LocalStorage.Location.ApplicationStoreLocation.ApplicationDirectory, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
