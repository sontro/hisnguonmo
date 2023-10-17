using AutoMapper;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.ExpMestAggrExam.ADO;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExpMestAggrExam
{
    public partial class UCExpMestAggrExam : HIS.Desktop.Utility.UserControlBase
    {
        /// <summary>
        /// Kiểm tra lẻ
        /// </summary>
        /// <param name="expMestIds"></param>
        /// <returns></returns>
        bool CheckOldByExpMestIds(List<long> expMestIds)
        {
            CommonParam param = new CommonParam();
            bool result = false;
            try
            {
                MOS.Filter.HisExpMestMedicineViewFilter expMestMediFilter = new HisExpMestMedicineViewFilter();
                expMestMediFilter.EXP_MEST_IDs = expMestIds;
                var expMestMedicines = new BackendAdapter(param).Get<List<V_HIS_EXP_MEST_MEDICINE>>(HisRequestUriStore.HIS_EXP_MEST_MEDICINE_GETVIEW, ApiConsumers.MosConsumer, expMestMediFilter, param);
                if (expMestMedicines != null && expMestMedicines.Count > 0)
                {
                    var mediGroups = expMestMedicines.GroupBy(p => new { p.MEDICINE_TYPE_ID, p.SERVICE_ID }).Select(p => p.ToList()).ToList();
                    var expMestMedicineMerges = (
                        from m in mediGroups select new MssMedicineTypeSDO(m)
                        ).ToList();
                    if (CheckOld(expMestMedicineMerges))
                    {
                        result = true;
                        return result;
                    }
                }

                MOS.Filter.HisExpMestMaterialViewFilter mateFilter = new HisExpMestMaterialViewFilter();
                mateFilter.EXP_MEST_IDs = expMestIds;
                var expMestMaterials = new BackendAdapter(param).Get<List<V_HIS_EXP_MEST_MATERIAL>>(HisRequestUriStore.HIS_EXP_MEST_MATERIAL_GETVIEW, ApiConsumers.MosConsumer, mateFilter, param);
                if (expMestMaterials != null && expMestMaterials.Count > 0)
                {
                    var mateGroups = expMestMaterials.GroupBy(p => new { p.MATERIAL_TYPE_ID, p.SERVICE_ID }).Select(p => p.ToList()).ToList();
                    var expMestMaterialMerges = (
                       from m in mateGroups select new MssMedicineTypeSDO(m)
                       ).ToList();
                    if (CheckOld(expMestMaterialMerges))
                    {
                        result = true;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        bool CheckOld(List<MssMedicineTypeSDO> datas)
        {
            bool result = false;
            try
            {
                foreach (var item in datas)
                {
                    decimal except = (Math.Round(item.AMOUNT) - item.AMOUNT);
                    if (except != 0)
                    {
                        result = true;
                        break;//dung lai khi co le
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
