using DevExpress.XtraEditors.Controls;
using Inventec.Common.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;

namespace HIS.Desktop.Plugins.CreatePatientList.Load
{
    class MilitaryRankLoader
    {
        public static void LoadDataCombo(DevExpress.XtraEditors.LookUpEdit cboMilitaryRank)
        {
            try
            {
                CommonParam param = new CommonParam();
                MOS.Filter.HisMilitaryRankFilter filter = new MOS.Filter.HisMilitaryRankFilter();
                var rank = new BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.HIS_MILITARY_RANK>>(HisRequestUriStore.HIS_MILITARY_RANK_GET, ApiConsumers.MosConsumer, filter, param);
                //cboGender.Properties.DataSource = GetDataBehaviorFactory.HisMilitaryRanks.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).ToList();
                //cboGender.Properties.DataSource = new HisMilitaryRanks.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).ToList();
                cboMilitaryRank.Properties.DataSource = rank;
                cboMilitaryRank.Properties.DisplayMember = "MILITARY_RANK_NAME";
                cboMilitaryRank.Properties.ValueMember = "ID";
                cboMilitaryRank.Properties.ForceInitialize();

                cboMilitaryRank.Properties.Columns.Clear();
                cboMilitaryRank.Properties.Columns.Add(new LookUpColumnInfo("MILITARY_RANK_CODE", "", 100));
                cboMilitaryRank.Properties.Columns.Add(new LookUpColumnInfo("MILITARY_RANK_NAME", "", 150));

                cboMilitaryRank.Properties.ShowHeader = false;
                cboMilitaryRank.Properties.ImmediatePopup = true;
                cboMilitaryRank.Properties.DropDownRows = 10;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public static void LoadDataCombo(DevExpress.XtraEditors.LookUpEdit cboMilitaryRank, object data)
        {
            try
            {
                CommonParam param = new CommonParam();
                MOS.Filter.HisMilitaryRankFilter filter = new MOS.Filter.HisMilitaryRankFilter();
                var rank = new BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.HIS_MILITARY_RANK>>(HisRequestUriStore.HIS_MILITARY_RANK_GET, ApiConsumers.MosConsumer, filter, param);
                cboMilitaryRank.Properties.DataSource = rank;
                cboMilitaryRank.Properties.DisplayMember = "MILITARY_RANK_NAME";
                cboMilitaryRank.Properties.ValueMember = "ID";
                cboMilitaryRank.Properties.ForceInitialize();

                cboMilitaryRank.Properties.Columns.Clear();
                cboMilitaryRank.Properties.Columns.Add(new LookUpColumnInfo("MILITARY_RANK_CODE", "", 100));
                cboMilitaryRank.Properties.Columns.Add(new LookUpColumnInfo("MILITARY_RANK_NAME", "", 150));

                cboMilitaryRank.Properties.ShowHeader = false;
                cboMilitaryRank.Properties.ImmediatePopup = true;
                cboMilitaryRank.Properties.DropDownRows = 10;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
