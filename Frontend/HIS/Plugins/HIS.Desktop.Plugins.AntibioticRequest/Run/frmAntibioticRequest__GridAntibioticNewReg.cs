using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.AntibioticRequest.ADO;
using Inventec.Common.Adapter;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;
using Inventec.Desktop.CustomControl;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors;
using MOS.Filter;
using System.Collections;
using System.Globalization;

namespace HIS.Desktop.Plugins.AntibioticRequest.Run
{
	public partial class frmAntibioticRequest
	{
		private void LoadDefaultGridAntibioticNewReg()
		{
			try
			{
				for (int i = 0; i < 8; i++)
				{
					AntibioticNewRegADO ado = new AntibioticNewRegADO();
					ado.Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionEdit;
					if (i == 7)
					{
						ado.Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionAdd;
					}
					lstNewRegADO.Add(ado);
				}
				gridControlAntibioticNewReg.DataSource = null;
				gridControlAntibioticNewReg.DataSource = lstNewRegADO;
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
		private void FillDataToGridAntibioticNewReg()
		{
			try
			{
				lstNewRegADO = new List<AntibioticNewRegADO>();
				CommonParam param = new CommonParam();
				if (this.currentAntibioticRequest != null && this.currentAntibioticRequest.AntibioticRequest != null)
				{
					HisAntibioticNewRegViewFilter filter = new HisAntibioticNewRegViewFilter();
					filter.ANTIBIOTIC_REQUEST_ID = this.currentAntibioticRequest.AntibioticRequest.ID;
					var dataDf = new BackendAdapter(param)
		.Get<List<V_HIS_ANTIBIOTIC_NEW_REG>>("api/HisAntibioticNewReg/GetView", ApiConsumers.MosConsumer, filter, param);
					if (dataDf != null && dataDf.Count > 0)
					{
						for (int i = 0; i < dataDf.Count; i++)
						{
							AntibioticNewRegADO ado = new AntibioticNewRegADO();
							Inventec.Common.Mapper.DataObjectMapper.Map<AntibioticNewRegADO>(ado, dataDf[i]);
							ado.Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionEdit;
							if (i == dataDf.Count - 1)
							{
								ado.Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionAdd;
							}
							lstNewRegADO.Add(ado);
						}
					}
				}
				if (this.currentAntibioticRequest != null && this.currentAntibioticRequest.NewRegimen != null && this.currentAntibioticRequest.NewRegimen.Count > 0)
				{
					HisActiveIngredientFilter filterName = new HisActiveIngredientFilter();
					filterName.IDs = this.currentAntibioticRequest.NewRegimen.Select(o => o.ACTIVE_INGREDIENT_ID).ToList();
					var dataName = new BackendAdapter(param)
		.Get<List<HIS_ACTIVE_INGREDIENT>>("api/HisActiveIngredient/Get", ApiConsumers.MosConsumer, filterName, param);
					var lstNewRegSend = this.currentAntibioticRequest.NewRegimen;
					if (lstNewRegADO != null && lstNewRegADO.Count > 0)
					{
						foreach (var itemSend in lstNewRegSend)
						{
							foreach (var item in lstNewRegADO)
							{
								if (item.ACTIVE_INGREDIENT_ID == itemSend.ACTIVE_INGREDIENT_ID)
								{
									item.CONCENTRA = itemSend.CONCENTRA;
									item.DOSAGE = itemSend.DOSAGE;
									item.PERIOD = itemSend.PERIOD;
									item.USE_DAY = itemSend.USE_DAY;
									item.USE_FORM = itemSend.USE_FORM;
									break;
								}
							}
						}
					}
					List<HIS_ANTIBIOTIC_NEW_REG> lstNewRegTempExits = new List<HIS_ANTIBIOTIC_NEW_REG>();
					if (lstNewRegADO != null && lstNewRegADO.Count > 0)
						lstNewRegTempExits = lstNewRegSend.Where(o => !lstNewRegADO.Exists(p => p.ACTIVE_INGREDIENT_ID == o.ACTIVE_INGREDIENT_ID)).ToList();
					else
						lstNewRegTempExits = lstNewRegSend;
					if (lstNewRegTempExits != null && lstNewRegTempExits.Count > 0)
					{
						foreach (var item in lstNewRegTempExits)
						{
							AntibioticNewRegADO ado = new AntibioticNewRegADO();
							ado.ACTIVE_INGREDIENT_NAME = dataName.First(o => o.ID == item.ACTIVE_INGREDIENT_ID).ACTIVE_INGREDIENT_NAME;
							ado.ACTIVE_INGREDIENT_ID = item.ACTIVE_INGREDIENT_ID;
							ado.CONCENTRA = item.CONCENTRA;
							ado.DOSAGE = item.DOSAGE;
							ado.PERIOD = item.PERIOD;
							ado.USE_DAY = item.USE_DAY;
							ado.USE_FORM = item.USE_FORM;
							ado.ANTIBIOTIC_NEW_ADD = true;
							lstNewRegADO.Add(ado);
						}
					}
					if (lstNewRegADO != null && lstNewRegADO.Count > 0)
					{
						var lstNewRegTempNotExits = lstNewRegADO.Where(o => !lstNewRegSend.Exists(p => p.ACTIVE_INGREDIENT_ID == o.ACTIVE_INGREDIENT_ID)).ToList();
						if (lstNewRegTempNotExits != null && lstNewRegTempNotExits.Count > 0)
						{
							foreach (var item in lstNewRegADO.Where(o => !lstNewRegSend.Exists(p => p.ACTIVE_INGREDIENT_ID == o.ACTIVE_INGREDIENT_ID)).ToList())
							{
								item.ANTIBIOTIC_DELETE = true;
							}
						}
					}
					for (int i = 0; i < lstNewRegADO.Count; i++)
					{
						lstNewRegADO[i].Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionEdit;
						if (i == lstNewRegADO.Count - 1)
						{
							lstNewRegADO[i].Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionAdd;
						}
					}

				}
				if (lstNewRegADO == null || lstNewRegADO.Count <= 0)
				{
					LoadDefaultGridAntibioticNewReg();
				}
				else
				{
					gridControlAntibioticNewReg.DataSource = null;
					gridControlAntibioticNewReg.DataSource = lstNewRegADO;
				}
				if (lstNewRegADO != null && lstNewRegADO.Count > 0)
				{
					lstNewRegADOTemp = lstNewRegADO.Where(o => !string.IsNullOrEmpty(o.CONCENTRA)
														|| !string.IsNullOrEmpty(o.DOSAGE)
														|| !string.IsNullOrEmpty(o.PERIOD)
														|| (o.USE_DAY != null && o.USE_DAY > 0)
														|| !string.IsNullOrEmpty(o.USE_FORM)
														).ToList();
					if (lstNewRegADOTemp != null && lstNewRegADOTemp.Count > 0)
					{
						lstNewRegADOTemp = lstNewRegADOTemp.Where(o => !o.ANTIBIOTIC_DELETE).ToList();
					}
				}
				foreach (var item in lstNewRegADOTemp)
				{
					V_HIS_ANTIBIOTIC_NEW_REG obj = new V_HIS_ANTIBIOTIC_NEW_REG();
					Inventec.Common.Mapper.DataObjectMapper.Map<V_HIS_ANTIBIOTIC_NEW_REG>(obj, item);
					this.currentAntibioticNewRegView.Add(obj);
				}

			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
		private void gridViewAntibioticNewReg_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			try
			{
				if (e.Column.FieldName == "BtnDeleteAntibioticNewReg")
				{
					int rowSelected = Convert.ToInt32(e.RowHandle);
					int action = Inventec.Common.TypeConvert.Parse.ToInt32((gridViewAntibioticNewReg.GetRowCellValue(e.RowHandle, "Action") ?? "").ToString());
					if (action == HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionAdd)
					{
						e.RepositoryItem = btnAddAntibioticNewReg;
					}
					else
					{
						e.RepositoryItem = btnDeleteAntibioticNewReg;
					}
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void gridViewAntibioticNewReg_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			try
			{
				DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
				if (e.RowHandle >= 0)
				{

					AntibioticNewRegADO pData = (AntibioticNewRegADO)((IList)((BaseView)sender).DataSource)[e.RowHandle];
					if (pData != null)
					{
						if (pData.ANTIBIOTIC_NEW_ADD)
							e.Appearance.ForeColor = Color.DarkGreen;
						else if (pData.ANTIBIOTIC_DELETE)
							e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);

					}


				}
			}
			catch (Exception ex)
			{

				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}

		private void gridViewAntibioticNewReg_CustomRowColumnError(object sender, Inventec.Desktop.CustomControl.RowColumnErrorEventArgs e)
		{
			try
			{
				if (e.ColumnName == "CONCENTRA" || e.ColumnName == "DOSAGE" || e.ColumnName == "PERIOD" || e.ColumnName == "USE_FORM")
				{
					this.gridViewAntibioticNewReg_CustomRowError(sender, e);
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
		private void gridViewAntibioticNewReg_CustomRowError(object sender, RowColumnErrorEventArgs e)
		{
			try
			{
				var index = this.gridViewAntibioticNewReg.GetDataSourceRowIndex(e.RowHandle);
				if (index < 0)
				{
					e.Info.ErrorType = ErrorType.None;
					e.Info.ErrorText = "";
					return;
				}
				var listDatas = this.gridControlAntibioticNewReg.DataSource as List<AntibioticNewRegADO>;
				var row = listDatas[index];
				if (e.ColumnName == "CONCENTRA")
				{
					if (row.ErrorTypeConcentra == ErrorType.Warning)
					{
						e.Info.ErrorType = (ErrorType)(row.ErrorTypeConcentra);
						e.Info.ErrorText = (string)(row.ErrorMessageConcentra);
					}
					else
					{
						e.Info.ErrorType = (ErrorType)(ErrorType.None);
						e.Info.ErrorText = "";
					}
				}
				else if (e.ColumnName == "DOSAGE")
				{
					if (row.ErrorTypeDosage == ErrorType.Warning)
					{
						e.Info.ErrorType = (ErrorType)(row.ErrorTypeDosage);
						e.Info.ErrorText = (string)(row.ErrorMessageDosage);
					}
					else
					{
						e.Info.ErrorType = (ErrorType)(ErrorType.None);
						e.Info.ErrorText = "";
					}
				}
				else if (e.ColumnName == "PERIOD")
				{
					if (row.ErrorTypePeriod == ErrorType.Warning)
					{
						e.Info.ErrorType = (ErrorType)(row.ErrorTypePeriod);
						e.Info.ErrorText = (string)(row.ErrorMessagePeriod);
					}
					else
					{
						e.Info.ErrorType = (ErrorType)(ErrorType.None);
						e.Info.ErrorText = "";
					}
				}
				else if (e.ColumnName == "USE_FORM")
				{
					if (row.ErrorTypeUseForm == ErrorType.Warning)
					{
						e.Info.ErrorType = (ErrorType)(row.ErrorTypeUseForm);
						e.Info.ErrorText = (string)(row.ErrorMessageUseForm);
					}
					else
					{
						e.Info.ErrorType = (ErrorType)(ErrorType.None);
						e.Info.ErrorText = "";
					}
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
		private void gridViewAntibioticNewReg_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			try
			{
				var AntibioticNewRegADO = (AntibioticNewRegADO)this.gridViewAntibioticNewReg.GetFocusedRow();
				if (AntibioticNewRegADO != null)
				{
					if (e.Column.FieldName == "CONCENTRA" || e.Column.FieldName == "DOSAGE" || e.Column.FieldName == "PERIOD" || e.Column.FieldName == "USE_FORM")
					{
						ValidNewRegProcessing(AntibioticNewRegADO);
					}
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
		private void ValidNewRegProcessing(AntibioticNewRegADO antibioticNewRegADO)
		{
			try
			{
				if (antibioticNewRegADO != null)
				{
					if (!String.IsNullOrEmpty(antibioticNewRegADO.CONCENTRA) && Inventec.Common.String.CountVi.Count(antibioticNewRegADO.CONCENTRA) > 1000)
					{

						antibioticNewRegADO.ErrorMessageConcentra = "Vượt quá độ dài cho phép 1000 ký tự";
						antibioticNewRegADO.ErrorTypeConcentra = ErrorType.Warning;
					}
					else
					{
						antibioticNewRegADO.ErrorMessageConcentra = "";
						antibioticNewRegADO.ErrorTypeConcentra = ErrorType.None;
					}
					if (!String.IsNullOrEmpty(antibioticNewRegADO.DOSAGE) && Inventec.Common.String.CountVi.Count(antibioticNewRegADO.DOSAGE) > 1000)
					{

						antibioticNewRegADO.ErrorMessageDosage = "Vượt quá độ dài cho phép 1000 ký tự";
						antibioticNewRegADO.ErrorTypeDosage = ErrorType.Warning;
					}
					else
					{
						antibioticNewRegADO.ErrorMessageDosage = "";
						antibioticNewRegADO.ErrorTypeDosage = ErrorType.None;
					}
					if (!String.IsNullOrEmpty(antibioticNewRegADO.PERIOD) && Inventec.Common.String.CountVi.Count(antibioticNewRegADO.PERIOD) > 100)
					{

						antibioticNewRegADO.ErrorMessagePeriod = "Vượt quá độ dài cho phép 100 ký tự";
						antibioticNewRegADO.ErrorTypePeriod = ErrorType.Warning;
					}
					else
					{
						antibioticNewRegADO.ErrorMessagePeriod = "";
						antibioticNewRegADO.ErrorTypePeriod = ErrorType.None;
					}
					if (!String.IsNullOrEmpty(antibioticNewRegADO.USE_FORM) && Inventec.Common.String.CountVi.Count(antibioticNewRegADO.USE_FORM) > 100)
					{

						antibioticNewRegADO.ErrorMessageUseForm = "Vượt quá độ dài cho phép 100 ký tự";
						antibioticNewRegADO.ErrorTypeUseForm = ErrorType.Warning;
					}
					else
					{
						antibioticNewRegADO.ErrorMessageUseForm = "";
						antibioticNewRegADO.ErrorTypeUseForm = ErrorType.None;
					}

				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
		private void btnAddAntibioticNewReg_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				List<AntibioticNewRegADO> AntibioticNewRegADOs = new List<AntibioticNewRegADO>();
				var AntibioticNewRegADO = gridControlAntibioticNewReg.DataSource as List<AntibioticNewRegADO>;
				if (AntibioticNewRegADO == null || AntibioticNewRegADO.Count < 1)
				{
					AntibioticNewRegADO ekipUserAdoTemp = new AntibioticNewRegADO();
					AntibioticNewRegADOs.Add(ekipUserAdoTemp);
					AntibioticNewRegADOs.ForEach(o => o.Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionEdit);
					AntibioticNewRegADOs.LastOrDefault().Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionAdd;
					gridControlAntibioticNewReg.DataSource = null;
					gridControlAntibioticNewReg.DataSource = AntibioticNewRegADOs;
				}
				else
				{
					AntibioticNewRegADO participant = new AntibioticNewRegADO();
					AntibioticNewRegADO.Add(participant);
					AntibioticNewRegADO.ForEach(o => o.Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionEdit);
					AntibioticNewRegADO.LastOrDefault().Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionAdd;
					gridControlAntibioticNewReg.DataSource = null;
					gridControlAntibioticNewReg.DataSource = AntibioticNewRegADO;
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
		private void btnDeleteAntibioticNewReg_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				CommonParam param = new CommonParam();
				var AntibioticNewRegADOs = gridControlAntibioticNewReg.DataSource as List<AntibioticNewRegADO>;
				var AntibioticNewRegADO = (AntibioticNewRegADO)gridViewAntibioticNewReg.GetFocusedRow();
				if (AntibioticNewRegADO != null)
				{
					if (AntibioticNewRegADOs.Count > 0)
					{
						AntibioticNewRegADOs.Remove(AntibioticNewRegADO);
						AntibioticNewRegADOs.ForEach(o => o.Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionEdit);
						AntibioticNewRegADOs.LastOrDefault().Action = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionAdd;
						gridControlAntibioticNewReg.DataSource = null;
						gridControlAntibioticNewReg.DataSource = AntibioticNewRegADOs;
					}
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
		private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			try
			{

				if (e.Info == null && e.SelectedControl == gridControlAntibioticNewReg)
				{
					DevExpress.XtraGrid.Views.Grid.GridView view = gridControlAntibioticNewReg.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView;
					GridHitInfo info = view.CalcHitInfo(e.ControlMousePosition);
					if (info.InRowCell)
					{
						if (lastRowHandle != info.RowHandle || lastColumn != info.Column)
						{
							lastColumn = info.Column;
							lastRowHandle = info.RowHandle;
							string text = "";

							if (this.gridViewAntibioticNewReg.GetRowCellValue(lastRowHandle, "ANTIBIOTIC_NEW_ADD") != null && (bool)this.gridViewAntibioticNewReg.GetRowCellValue(lastRowHandle, "ANTIBIOTIC_NEW_ADD"))
							{
								text = "Kháng sinh mới bổ sung vào đơn";
							}
							if (this.gridViewAntibioticNewReg.GetRowCellValue(lastRowHandle, "ANTIBIOTIC_DELETE") != null && (bool)this.gridViewAntibioticNewReg.GetRowCellValue(lastRowHandle, "ANTIBIOTIC_DELETE"))
							{
								text = "Kháng sinh đã bị bỏ khỏi đơn";
							}

							lastInfo = new ToolTipControlInfo(new DevExpress.XtraGrid.GridToolTipInfo(view, new DevExpress.XtraGrid.Views.Base.CellToolTipInfo(info.RowHandle, info.Column, "Text")), text);
						}
						e.Info = lastInfo;
					}
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
		private void txtUseDay_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				AntibioticNewRegADO data = (AntibioticNewRegADO)gridViewAntibioticNewReg.GetFocusedRow();
				if (data != null)
				{

					TextEdit txt = sender as TextEdit;

					if (txt.Text != null && txt.Text != "")
					{
						data.USE_DAY = Convert.ToDecimal(txt.Text, new CultureInfo("en-US"));
					}
					else
					{
						data.USE_DAY = null;
					}
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
		private void txtUseDay_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
				{
					e.Handled = true;
				}
				TextEdit txt = sender as TextEdit;
				if (!string.IsNullOrEmpty(txt.Text) && txt.Text.IndexOf(".") > -1 && (e.KeyChar == '.'
					|| (txt.Text.Length - 5 == txt.Text.IndexOf(".") && char.IsDigit(e.KeyChar) && txt.SelectionStart > txt.Text.IndexOf("."))))
					e.Handled = true;


			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}


		private void repositoryItemSpinEdit1_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				AntibioticNewRegADO data = (AntibioticNewRegADO)gridViewAntibioticNewReg.GetFocusedRow();
				if (data != null)
				{

					SpinEdit spd = sender as SpinEdit;

					if (spd.Text != null && spd.Text != "")
					{
						data.USE_DAY = Inventec.Common.Number.Get.RoundCurrency(spd.Value, 4);
					}
					else
					{
						data.USE_DAY = null;
					}
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
	}
}
