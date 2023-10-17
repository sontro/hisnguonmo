using HIS.Desktop.LocalStorage.Location;
using MOS.EFMODEL.DataModels;
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

namespace HIS.Desktop.Plugins.AllergyCard
{
    public partial class frmAllergyCard : HIS.Desktop.Utility.FormBase
    {
        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentID;
        V_HIS_PATIENT currentPatient;

        public frmAllergyCard()
        {
            InitializeComponent();
        }

        public frmAllergyCard(Inventec.Desktop.Common.Modules.Module module, long _treatmentId)
            : base(module)
        {
            InitializeComponent();
            try
            {
                this.Text = module.text;
                this.currentModule = module;
                this.treatmentID = _treatmentId;
                SetIcon();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }

        public frmAllergyCard(Inventec.Desktop.Common.Modules.Module module, V_HIS_PATIENT _patient)
            : base(module)
        {
            InitializeComponent();
            try
            {
                this.Text = module.text;
                this.currentModule = module;
                this.currentPatient = _patient;
                SetIcon();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void frmAllergyCard_Load(object sender, EventArgs e)
        {
            try
            {
                //this.Size = new Size(1018, 485);
                if (this.currentPatient == null)
                {
                    UC_AllergyCard uc = new UC_AllergyCard(this.currentModule, this.treatmentID);
                    uc.Dock = DockStyle.Fill;
                    panelControlAllergyCard.Controls.Add(uc);
                }
                else
                {
                    UC_AllergyCard uc = new UC_AllergyCard(this.currentModule, this.currentPatient);
                    uc.Dock = DockStyle.Fill;
                    panelControlAllergyCard.Controls.Add(uc);
                }
                //panelControlAllergyCard.Dock = DockStyle.Fill;
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
    }
}
