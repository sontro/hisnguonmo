using HIS.Desktop.Common;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.AssignPrescriptionKidney.AssignPrescription
{
    public partial class frmIsExpendType : Form
    {
        DelegateSelectData ReloadData;
        public frmIsExpendType(DelegateSelectData ReloadData)
        {
            InitializeComponent();
            this.ReloadData = ReloadData;
        }

        private void frmExpendType_Load(object sender, EventArgs e)
        {
            try
            {
                SetCaptionByLanguageKey();
                //InitComboExpendType();
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        private void SetCaptionByLanguageKey()
        {
            try
            {
                Resources.ResourceLanguageManager.LanguagefrmAssignPrescription = new ResourceManager("HIS.Desktop.Plugins.AssignPrescriptionKidney.Resources.Lang", typeof(HIS.Desktop.Plugins.AssignPrescriptionKidney.AssignPrescription.frmIsExpendType).Assembly);

                this.Text = Inventec.Common.Resource.Get.Value("frmExpendType.Text", Resources.ResourceLanguageManager.LanguagefrmAssignPrescription, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());

                this.layoutControlItem2.Text = Inventec.Common.Resource.Get.Value("frmExpendType.layoutControlItem2.Text", Resources.ResourceLanguageManager.LanguagefrmAssignPrescription, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());

                this.btnSave.Text = Inventec.Common.Resource.Get.Value("frmExpendType.btnSave.Text", Resources.ResourceLanguageManager.LanguagefrmAssignPrescription, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ReloadData != null)
                {
                    this.ReloadData(chkIsExpendType.Checked);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }
    }
}
