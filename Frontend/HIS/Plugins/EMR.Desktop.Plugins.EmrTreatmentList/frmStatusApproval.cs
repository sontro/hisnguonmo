using EMR.EFMODEL.DataModels;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMR.Desktop.Plugins.EmrTreatmentList
{
	public partial class frmStatusApproval : Form
	{
		List<EFMODEL.DataModels.V_EMR_TREATMENT> lstEmrTreatment; //DANG FIX
		Action<bool> IsReload;
		int countTreatment = 0;
		int indexTreatment = 1;
		V_EMR_TREATMENT currentTreatment;
		string MessError = null;
		List<string> lstError = new List<string>();
		public frmStatusApproval(List<EFMODEL.DataModels.V_EMR_TREATMENT> lstEmrTreatment,Action<bool> IsReload)
		{
			InitializeComponent();
			try
			{
				this.lstEmrTreatment = lstEmrTreatment;
				this.IsReload = IsReload;
				string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
				this.Icon = Icon.ExtractAssociatedIcon(iconPath);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				int count = 0;
				foreach (var item in lstEmrTreatment)
				{
					count++;
					if (backgroundWorker1.CancellationPending)
						break;
					currentTreatment = item;
					backgroundWorker1.ReportProgress(indexTreatment);
					CommonParam paramCommon = new CommonParam();
					var apiResult = new Inventec.Common.Adapter.BackendAdapter
							   (paramCommon).Post<bool>
							   ("api/EmrTreatment/ApproveSign", ApiConsumers.EmrConsumer, item.ID, SessionManager.ActionLostToken, paramCommon);
					if (!apiResult)
					{
						lstError.Add(String.Format("{0}: Thất bại. {1}",item.TREATMENT_CODE,paramCommon.GetMessage()));
						if(count == lstEmrTreatment.Count)
							backgroundWorker1.ReportProgress(count);
					}
					if (count < lstEmrTreatment.Count)
						indexTreatment++;
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			try
			{
				pbProcess.Value = (int)indexTreatment;
				lblProcessNumber.Text = string.Format("{0}/{1}", indexTreatment, countTreatment);
				lblProcessName.Text = string.Format("{0} - {1}", currentTreatment.TREATMENT_CODE, currentTreatment.VIR_PATIENT_NAME);
				memResult.Text = null;
				memResult.Text = String.Join("\r\n", lstError);
				memResult.SelectionStart = Int32.MaxValue;
				memResult.ScrollToCaret();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if(indexTreatment < countTreatment)
				{
					indexTreatment = countTreatment;
					lblProcessNumber.Text = string.Format("{0}/{1}", indexTreatment, countTreatment);
					pbProcess.Value = (int)indexTreatment;
				}
				lblProcessName.Text = "";
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void frmStatusApproval_Load(object sender, EventArgs e)
		{
			try
			{
				countTreatment = lstEmrTreatment.Count();
				pbProcess.Maximum = (int)countTreatment;
				backgroundWorker1.RunWorkerAsync();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void btnEnd_Click(object sender, EventArgs e)
		{
			try
			{
				backgroundWorker1.CancelAsync();
				if (IsReload != null)
					IsReload(true);
				this.Close();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
	}
}
