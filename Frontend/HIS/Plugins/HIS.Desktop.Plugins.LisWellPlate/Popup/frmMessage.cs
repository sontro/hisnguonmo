using Inventec.Desktop.Common.LocalStorage.Location;
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

namespace HIS.Desktop.Plugins.LisWellPlate.Popup
{
    public partial class frmMessage : Form
    {
        bool isChoose = false;
        Action<EnumOption> action;
        public frmMessage(Action<EnumOption> action)
        {
            InitializeComponent();
            try
            {
                this.action = action;
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            try
            {
                action(EnumOption.AUTO);
                isChoose = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnCondition_Click(object sender, EventArgs e)
        {
            try
            {
                action(EnumOption.CONDITION);
                isChoose = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            try
            {
                action(EnumOption.NONE);
                isChoose = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!isChoose)
                {
                    action(EnumOption.NONE);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
