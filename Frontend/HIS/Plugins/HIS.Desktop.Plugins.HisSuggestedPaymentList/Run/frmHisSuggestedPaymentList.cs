using AutoMapper;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Common;
using HIS.Desktop.IsAdmin;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisSuggestedPaymentList.Run
{
    public partial class frmHisSuggestedPaymentList : HIS.Desktop.Utility.FormBase
    {
        Inventec.Desktop.Common.Modules.Module currentModule;

        int rowCount = 0;
        int dataTotal = 0;
        int start = 0;
        int limit = 0;
        int pageSize = 0;


        public frmHisSuggestedPaymentList()
        {
            InitializeComponent();
        }

        public frmHisSuggestedPaymentList(Inventec.Desktop.Common.Modules.Module currentModule)
            : base(currentModule)
        {
            InitializeComponent();
            try
            {
                this.currentModule = currentModule;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }


        private void frmHisSuggestedPaymentList_Load(object sender, EventArgs e)
        {
            try
            {
                SetIconFrm();
                SetCaptionByLanguageKey();
                if (this.currentModule != null)
                {
                    this.Text = this.currentModule.text;
                }

                dtFromTime.EditValue = null;
                dtToTime.EditValue = DateTime.Now;

                LoadDataList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void SetCaptionByLanguageKey()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        void SetIconFrm()
        {
            try
            {
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataList()
        {
            try
            {
                if (ucPaging1.pagingGrid != null)
                {
                    pageSize = ucPaging1.pagingGrid.PageSize;
                }
                else
                {
                    pageSize = (int)ConfigApplications.NumPageSize;
                }
                ucPagingData(new CommonParam(0, (int)pageSize));
                CommonParam param = new CommonParam();
                param.Limit = rowCount;
                param.Count = dataTotal;
                ucPaging1.Init(ucPagingData, param, (int)pageSize, gridControls);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ucPagingData(object param)
        {
            try
            {
                WaitingManager.Show();
                gridControls.DataSource = null;

                start = ((CommonParam)param).Start ?? 0;
                limit = ((CommonParam)param).Limit ?? 0;
                CommonParam paramCommon = new CommonParam(start, limit);
                MOS.Filter.HisImpMestProposeViewFilter _Filter = new MOS.Filter.HisImpMestProposeViewFilter();
                if (this.currentModule != null && this.currentModule.RoomId > 0)
                {
                    _Filter.PROPOSE_ROOM_ID = this.currentModule.RoomId;
                }
                else
                {
                    return;
                }
                _Filter.ORDER_FIELD = "CREATE_TIME";
                _Filter.ORDER_DIRECTION = "DESC";
                if (dtFromTime.EditValue != null && dtFromTime.DateTime != DateTime.MinValue)
                {
                    _Filter.CREATE_TIME_FROM = Inventec.Common.TypeConvert.Parse.ToInt64(Convert.ToDateTime(dtFromTime.EditValue).ToString("yyyyMMdd") + "000000");
                }
                if (dtToTime.EditValue != null && dtToTime.DateTime != DateTime.MinValue)
                {
                    _Filter.CREATE_TIME_TO = Inventec.Common.TypeConvert.Parse.ToInt64(Convert.ToDateTime(dtToTime.EditValue).ToString("yyyyMMdd") + "235959");
                }
                List<V_HIS_IMP_MEST_PROPOSE> vHisSuggestedPaymentList = new List<V_HIS_IMP_MEST_PROPOSE>();
                var result = new BackendAdapter(paramCommon).GetRO<List<V_HIS_IMP_MEST_PROPOSE>>("api/HisImpMestPropose/GetView", ApiConsumers.MosConsumer, _Filter, paramCommon);
                if (result != null)
                {
                    vHisSuggestedPaymentList = (List<V_HIS_IMP_MEST_PROPOSE>)result.Data;
                    rowCount = (vHisSuggestedPaymentList == null ? 0 : vHisSuggestedPaymentList.Count);
                    dataTotal = (result.Param == null ? 0 : result.Param.Count ?? 0);
                }

                gridControls.BeginUpdate();
                gridControls.DataSource = vHisSuggestedPaymentList;
                gridControls.EndUpdate();
                gridViews.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridViews.OptionsSelection.EnableAppearanceFocusedRow = false;
                //gridViewTrackings.BestFitColumns();
                WaitingManager.Hide();
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridViews_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.IsGetData && e.Column.UnboundType != UnboundColumnType.Bound)
                {
                    V_HIS_IMP_MEST_PROPOSE data = (V_HIS_IMP_MEST_PROPOSE)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
                    if (data == null)
                        return;
                    if (e.Column.FieldName == "STT")
                    {
                        e.Value = e.ListSourceRowIndex + 1 + start;
                    }
                    else if (e.Column.FieldName == "CREATE_TIME_DISPLAY")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.CREATE_TIME.Value);
                    }
                    else if (e.Column.FieldName == "MODIFY_TIME_DISPLAY")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.MODIFY_TIME.Value);
                    }

                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridViews_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    string creator = (gridViews.GetRowCellValue(e.RowHandle, "CREATOR") ?? "").ToString().Trim();
                    //long DEPARTMENT_ID = Inventec.Common.TypeConvert.Parse.ToInt64((gridViews.GetRowCellValue(e.RowHandle, "DEPARTMENT_ID") ?? "0").ToString());
                    string loginName = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                    long departmentId = WorkPlace.WorkPlaceSDO.FirstOrDefault(p => p.RoomId == this.currentModule.RoomId).DepartmentId;
                    if (e.Column.FieldName == "DELETE")
                    {
                        if (loginName == creator || CheckLoginAdmin.IsAdmin(loginName))
                        {
                            e.RepositoryItem = repositoryItemButton__Delete_Enable;
                        }
                        else
                        {
                            e.RepositoryItem = repositoryItemButton__Delete_Disable;
                        }
                    }
                    else if (e.Column.FieldName == "EDIT")
                    {
                        if (loginName == creator || CheckLoginAdmin.IsAdmin(loginName))
                        {
                            e.RepositoryItem = repositoryItemButton__Edit;
                        }
                        else
                        {
                            e.RepositoryItem = repositoryItemButton__Edit_D;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridViews_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
                {
                    GridView view = sender as GridView;
                    GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
                    GridHitInfo hi = view.CalcHitInfo(e.Location);
                    if (hi.HitTest == GridHitTest.RowCell)
                    {
                        long departmentId = WorkPlace.WorkPlaceSDO.FirstOrDefault(p => p.RoomId == this.currentModule.RoomId).DepartmentId;
                        string loginName = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                        if (hi.Column.FieldName == "EDIT")
                        {
                            #region EDIT
                            var row = (V_HIS_IMP_MEST_PROPOSE)gridViews.GetRow(hi.RowHandle);
                            if (row != null)
                            {
                                var creator = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                                if (creator.Trim() == row.CREATOR.Trim() || CheckLoginAdmin.IsAdmin(loginName))
                                {
                                    Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where(o => o.ModuleLink == "HIS.Desktop.Plugins.HisSuggestedPayment").FirstOrDefault();
                                    if (moduleData == null) Inventec.Common.Logging.LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.HisSuggestedPayment");
                                    if (moduleData.IsPlugin && moduleData.ExtensionInfo != null)
                                    {
                                        List<object> listArgs = new List<object>();
                                        listArgs.Add(row);
                                        listArgs.Add((int)2);
                                        listArgs.Add(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId));
                                        var extenceInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId), listArgs);
                                        if (extenceInstance == null) throw new ArgumentNullException("moduleData is null");

                                        ((Form)extenceInstance).ShowDialog();

                                    }
                                }
                            }
                            #endregion
                        }
                        else if (hi.Column.FieldName == "VIEW")
                        {
                            #region View
                            var row = (V_HIS_IMP_MEST_PROPOSE)gridViews.GetRow(hi.RowHandle);
                            if (row != null)
                            {
                                Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where(o => o.ModuleLink == "HIS.Desktop.Plugins.HisSuggestedPayment").FirstOrDefault();
                                if (moduleData == null) Inventec.Common.Logging.LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.HisSuggestedPayment");
                                if (moduleData.IsPlugin && moduleData.ExtensionInfo != null)
                                {
                                    List<object> listArgs = new List<object>();
                                    listArgs.Add(row);
                                    listArgs.Add((int)3);
                                    listArgs.Add(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId));
                                    var extenceInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId), listArgs);
                                    if (extenceInstance == null) throw new ArgumentNullException("moduleData is null");

                                    ((Form)extenceInstance).ShowDialog();

                                }
                            }
                            #endregion
                        }
                        else if (hi.Column.FieldName == "DELETE")
                        {
                            #region DELETE
                            var row = (V_HIS_IMP_MEST_PROPOSE)gridViews.GetRow(hi.RowHandle);
                            if (row != null)
                            {
                                var creator = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                                if (creator.Trim() == row.CREATOR.Trim() || CheckLoginAdmin.IsAdmin(loginName))
                                {
                                    if (DevExpress.XtraEditors.XtraMessageBox.Show(
                                    MessageUtil.GetMessage(LibraryMessage.Message.Enum.HeThongTBCuaSoThongBaoBanCoMuonHuyDuLieuKhong),
                                    MessageUtil.GetMessage(LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao),
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        bool success = false;
                                        CommonParam param = new CommonParam();
                                        HisImpMestProposeDeleteSDO sdo = new HisImpMestProposeDeleteSDO();
                                        sdo.Id = row.ID;
                                        sdo.WorkingRoomId = this.currentModule.RoomId;
                                        var rs = new BackendAdapter(param).Post<bool>("api/HisImpMestPropose/Delete", ApiConsumers.MosConsumer, sdo, param);
                                        if (rs)
                                        {
                                            success = true;
                                            //Load lại tracking
                                            LoadDataList();
                                        }
                                        MessageManager.Show(this.ParentForm, param, success);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where(o => o.ModuleLink == "HIS.Desktop.Plugins.HisSuggestedPayment").FirstOrDefault();
                if (moduleData == null) Inventec.Common.Logging.LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.HisSuggestedPayment");
                if (moduleData.IsPlugin && moduleData.ExtensionInfo != null)
                {
                    List<object> listArgs = new List<object>();
                    listArgs.Add((DelegateRefreshData)ReLoad);
                    listArgs.Add(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId));
                    var extenceInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId), listArgs);
                    if (extenceInstance == null) throw new ArgumentNullException("moduleData is null");

                    ((Form)extenceInstance).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ReLoad()
        {
            try
            {
                btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barButton__Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barButton__New_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                btnNew_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
