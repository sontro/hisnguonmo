using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisImportPatient.PopUp
{
	public partial class frmWaiting : Form
	{
		int total;
		public frmWaiting(int total)
		{
			this.total = total;
			InitializeComponent();
			string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
			this.Icon = Icon.ExtractAssociatedIcon(iconPath);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				
				progressBar1.Value = UpdateIndex.currentIndex;
				lblStatus.Text = "Đã xử lý: " + UpdateIndex.currentIndex + "/" + total;
				if (UpdateIndex.currentIndex == total)
				{
					btnClose.Enabled = true;
					timer1.Stop();
				}	
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}

		private void frmWaiting_Load(object sender, EventArgs e)
		{
			try
			{
				progressBar1.Maximum = total;
				timer1.Start();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
	}
}
