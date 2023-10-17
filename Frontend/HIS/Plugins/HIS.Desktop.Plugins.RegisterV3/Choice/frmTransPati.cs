using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Utility;
using HIS.UC.UCTransPati;
using HIS.UC.UCTransPati.ADO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.DelegateRegister;

namespace HIS.Desktop.Plugins.RegisterV3
{
    public partial class frmTransPati : FormBase
    {
        bool isValidate { get; set; }
        UCTransPatiADO UCTransPatiADO { get; set; }
        UpdateSelectedTranPati UpdateSelectedTranPati { get; set; }
        DelegateVisible dlgSetValidateForValidTTCT;
        bool _RunValidate { get; set; }

        public frmTransPati(
            bool _isValidate,
            UCTransPatiADO _ucTransPatiADO,
            UpdateSelectedTranPati _updateSelectedTranPati)
            : this(null)
        {
            this.isValidate = _isValidate;
            this.UCTransPatiADO = _ucTransPatiADO;
            this.UpdateSelectedTranPati = _updateSelectedTranPati;
        }

        public frmTransPati(
            bool _isValidate,
            UCTransPatiADO _ucTransPatiADO,
            UpdateSelectedTranPati _updateSelectedTranPati,
            bool _runValidate)
            : this(null)
        {
            this._RunValidate = _runValidate;
            this.isValidate = _isValidate;
            this.UCTransPatiADO = _ucTransPatiADO;
            this.UpdateSelectedTranPati = _updateSelectedTranPati;
        }

        public frmTransPati(Inventec.Desktop.Common.Modules.Module module)
            : base(module)
        {
            InitializeComponent();
        }

        private void frmTransPati_Load(object sender, EventArgs e)
        {
            try
            {
                SetIcon();
                this.ucTransPati1.ResetRequiredField(this.isValidate);
                this.ucTransPati1.SetValue(this.UCTransPatiADO);
                if (this._RunValidate)
                    this.ucTransPati1.ValidateRequiredField();
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
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void bbntNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                btnClose_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dlgSetValidateForValidTTCT != null)
                    this.dlgSetValidateForValidTTCT(ValidateRequiredField());
                this.UpdateSelectedTranPati(this.ucTransPati1.GetValue());
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public bool ValidateRequiredField()
        {
            bool valid = true;
            try
            {
                valid = this.ucTransPati1.ValidateRequiredField();
                Inventec.Common.Logging.LogSystem.Debug("Get validate tu form TransPati thanh cong. valid = " + valid);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
                Inventec.Common.Logging.LogSystem.Debug("Get validate tu form TransPati that bai.");
            }
            return valid;
        }

        public void RefreshFormTransPati()
        {
            try
            {
                this.ucTransPati1.RefreshUserControl();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void SetValidForTTCT(DelegateVisible _dlgHideForm)
        {
            try
            {
                if (_dlgHideForm != null)
                    this.dlgSetValidateForValidTTCT = _dlgHideForm;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
