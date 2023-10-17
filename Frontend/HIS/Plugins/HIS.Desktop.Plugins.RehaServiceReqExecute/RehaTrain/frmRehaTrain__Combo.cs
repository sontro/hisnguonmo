using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MOS.SDO;
using MOS.EFMODEL.DataModels;
using AutoMapper;
using Inventec.Core;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using HIS.Desktop.Plugins.RehaServiceReqExecute.ADO;
using Inventec.Common.Adapter;
using HIS.Desktop.ApiConsumer;
using Inventec.Common.Controls.EditorLoader;
using MOS.Filter;

namespace HIS.Desktop.Plugins.RehaServiceReqExecute
{
    public delegate void RefeshDataAfterSuccess();

    public partial class frmRehaTrain : DevExpress.XtraEditors.XtraForm
    {
        private void LoadComboRehaTrainType(DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboRehaTrainType)
        {
            try
            {
                CommonParam param = new CommonParam();
                MOS.Filter.HisRehaTrainTypeViewFilter hisRehaTrainViewFilter = new MOS.Filter.HisRehaTrainTypeViewFilter();
                hisRehaTrainViewFilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                var dataCombo = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.V_HIS_REHA_TRAIN_TYPE>>(HisRequestUriStore.HIS_REHA_TRAIN_TYPE_GETVIEW, ApiConsumers.MosConsumer, hisRehaTrainViewFilter, param);

                List<ColumnInfo> columnInfos = new List<ColumnInfo>();
                columnInfos.Add(new ColumnInfo("REHA_TRAIN_TYPE_CODE", "", 150, 1));
                columnInfos.Add(new ColumnInfo("REHA_TRAIN_TYPE_NAME", "", 250, 2));
                ControlEditorADO controlEditorADO = new ControlEditorADO("REHA_TRAIN_TYPE_NAME", "ID", columnInfos, false, 250);
                ControlEditorLoader.Load(cboRehaTrainType, dataCombo, controlEditorADO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadComboRehaServiceType(DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboRehaTrainType)
        {
            try
            {
                CommonParam param = new CommonParam();
                MOS.Filter.HisRehaTrainTypeViewFilter hisRehaTrainViewFilter = new MOS.Filter.HisRehaTrainTypeViewFilter();
                hisRehaTrainViewFilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                var dataCombo = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.V_HIS_REHA_TRAIN_TYPE>>(HisRequestUriStore.HIS_REHA_TRAIN_TYPE_GETVIEW, ApiConsumers.MosConsumer, hisRehaTrainViewFilter, param);

                List<ColumnInfo> columnInfos = new List<ColumnInfo>();
                columnInfos.Add(new ColumnInfo("REHA_TRAIN_TYPE_CODE", "", 150, 1));
                columnInfos.Add(new ColumnInfo("REHA_TRAIN_TYPE_NAME", "", 250, 2));
                ControlEditorADO controlEditorADO = new ControlEditorADO("REHA_TRAIN_TYPE_NAME", "ID", columnInfos, false, 250);
                ControlEditorLoader.Load(cboRehaTrainType, dataCombo, controlEditorADO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal static void LoadDataToCombo(DevExpress.XtraEditors.GridLookUpEdit cboRehaTrainType, long rehaServiceTypeId)
        {
            try
            {
                MOS.Filter.HisRehaTrainTypeViewFilter hisRehaTrainViewFilter = new MOS.Filter.HisRehaTrainTypeViewFilter();
                hisRehaTrainViewFilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;

                var data =  new BackendAdapter(new CommonParam())
                    .Get<List<MOS.EFMODEL.DataModels.V_HIS_REHA_TRAIN_TYPE>>(HisRequestUriStore.HIS_REHA_TRAIN_TYPE_GETVIEW, ApiConsumers.MosConsumer, hisRehaTrainViewFilter, new CommonParam());
                if (data != null && data.Count > 0)
                {
                    HisRestRetrTypeViewFilter restRetrTypeViewFilter = new MOS.Filter.HisRestRetrTypeViewFilter();
                    restRetrTypeViewFilter.REHA_SERVICE_TYPE_ID = rehaServiceTypeId;

                    var restRehaTrainTypeIds = new BackendAdapter(new CommonParam())
                    .Get<List<MOS.EFMODEL.DataModels.V_HIS_REST_RETR_TYPE>>(HisRequestUriStore.HIS_REST_RETR_TYPE_GETVIEW, ApiConsumers.MosConsumer, restRetrTypeViewFilter, new CommonParam()).Select(o => o.REHA_TRAIN_TYPE_ID).ToArray(); ;

                    data = data.Where(o => restRehaTrainTypeIds.Contains(o.ID)).ToList();
                }

                List<ColumnInfo> columnInfos = new List<ColumnInfo>();
                columnInfos.Add(new ColumnInfo("REHA_TRAIN_TYPE_CODE", "", 150, 1));
                columnInfos.Add(new ColumnInfo("REHA_TRAIN_TYPE_NAME", "", 250, 2));
                ControlEditorADO controlEditorADO = new ControlEditorADO("REHA_TRAIN_TYPE_NAME", "ID", columnInfos, false, 250);
                ControlEditorLoader.Load(cboRehaTrainType, data, controlEditorADO);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}