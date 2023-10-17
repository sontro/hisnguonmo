using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Print;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System.ComponentModel;

namespace HIS.Desktop.Plugins.BidCreate
{
    public partial class UCBidCreate : HIS.Desktop.Utility.UserControlBase
    {
		Task taskMedicineType { get; set; }
		Task taskMaterialType { get; set; }
		Task taskBloodType { get; set; }

		private async void TaskAll()
		{
			try
			{
				try
				{
					GetBidForm();
					GetSupplier();
					GetBid();
					LoadNation();
					LoadManufacture();
					LoadMediUseForm();
					taskMaterialType = LoadMaterial();
					taskBloodType = LoadBlood();
				}
				catch (Exception ex)
				{
					Inventec.Common.Logging.LogSystem.Error(ex);
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
		private async Task GetBidForm()
		{
			try
			{
				Action myaction = () =>
				{
					lstBidForm = new List<ADO.BidFormADO>();
					lstBidForm.Add(new ADO.BidFormADO() { ID = 1, CODE = "01", NAME = "Đấu thầu rộng rãi" });
					lstBidForm.Add(new ADO.BidFormADO() { ID = 2, CODE = "02", NAME = "Đấu thầu hạn chế" });
					lstBidForm.Add(new ADO.BidFormADO() { ID = 3, CODE = "03", NAME = "Chỉ định thầu" });
					lstBidForm.Add(new ADO.BidFormADO() { ID = 4, CODE = "04", NAME = "Chào hàng cạnh tranh" });
					lstBidForm.Add(new ADO.BidFormADO() { ID = 5, CODE = "05", NAME = "Mua sắm trực tiếp" });
					lstBidForm.Add(new ADO.BidFormADO() { ID = 6, CODE = "06", NAME = "Khác" });
				};
				Task task = new Task(myaction);
				task.Start();

				await task;
				LoadDataToCboBidForm();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
		private async Task GetSupplier()
		{
			try
			{
				Action myaction = () => {
					lstSupplier = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_SUPPLIER>().OrderByDescending(o => o.CREATE_TIME).ToList();
				};
				Task task = new Task(myaction);
				task.Start();

				await task;
				LoadDataToCboSupplier();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
		private async Task GetBid()
		{
			try
			{
				Action myaction = () => {
					MOS.Filter.HisBidTypeFilter bidTypeFilter = new MOS.Filter.HisBidTypeFilter();
					bidTypes = new Inventec.Common.Adapter.BackendAdapter(new Inventec.Core.CommonParam()).Get<List<MOS.EFMODEL.DataModels.HIS_BID_TYPE>>("/api/HisBidType/Get", ApiConsumer.ApiConsumers.MosConsumer, bidTypeFilter, HIS.Desktop.Controls.Session.SessionManager.ActionLostToken, null);
				};
				Task task = new Task(myaction);
				task.Start();

				await task;
				LoadDataToCboBidType();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
		private async Task LoadNation()
		{
			try
			{
				Action myaction = () => {
					lstNational = BackendDataWorker.Get<SDA.EFMODEL.DataModels.SDA_NATIONAL>().OrderBy(o => o.NATIONAL_NAME).ToList();
				};
				Task task = new Task(myaction);
				task.Start();

				await task;
				LoadDataToCboNational();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
		private async Task LoadManufacture()
		{
			try
			{
				Action myaction = () => {
					lstManufacture = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_MANUFACTURER>().OrderBy(o => o.MANUFACTURER_NAME).ToList();
				};
				Task task = new Task(myaction);
				task.Start();

				await task;
				LoadDataToCboManufacture();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
		private async Task LoadMediUseForm()
		{
			try
			{
				Action myaction = () => {
					lstMediUseForm = BackendDataWorker.Get<HIS_MEDICINE_USE_FORM>().Where(o => o.IS_ACTIVE == (short)1).OrderBy(o => o.MEDICINE_USE_FORM_CODE).ToList();
				};
				Task task = new Task(myaction);
				task.Start();

				await task;
				LoadDataToCboMediUseForm();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}	
		private async Task LoadMaterial()
		{
			try
			{
				Action myaction = () => {
					listHisMaterialType = BackendDataWorker.Get<V_HIS_MATERIAL_TYPE>().Where(o =>
	o.IS_STOP_IMP != 1
	&& o.IS_LEAF == 1
	&& o.IS_ACTIVE == 1).ToList();
				};
				Task task = new Task(myaction);
				task.Start();

				await task;
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}

		private async Task LoadBlood()
		{
			try
			{
				Action myaction = () => {
					listHisBloodType = BackendDataWorker.Get<V_HIS_BLOOD_TYPE>().Where(o =>
o.IS_LEAF == 1
&& o.IS_ACTIVE == 1).ToList();
				};
				Task task = new Task(myaction);
				task.Start();

				await task;
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}

		private async Task LoadMedicine()
		{
			try
			{
				Action myaction = () => {
					listHisMedicineType = BackendDataWorker.Get<V_HIS_MEDICINE_TYPE>().Where(o =>

	   o.IS_STOP_IMP != 1
			   && o.IS_LEAF == 1
			   && o.IS_ACTIVE == 1).ToList();
				};
				Task task = new Task(myaction);
				task.Start();

				await task;
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
	

		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				if(listHisMedicineType !=null && listHisMedicineType.Count > 0)
				{
					FillDataToGrid();
					timer1.Stop();
				}	
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
	}
}
