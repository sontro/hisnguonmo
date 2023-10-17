using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmteMedicineType;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestTemplate
{
    partial class HisExpMestTemplateCreate : BusinessBase
    {
        private List<HIS_EMTE_MEDICINE_TYPE> recentHisEmteMedicineTypes;

        /// <summary>
        /// Xu ly du lieu chi tiet cua template
        /// </summary>
        private void ProcessEmteMedicineType(HisExpMestTemplateSDO data)
        {
            if (IsNotNullOrEmpty(data.EmteMedicineTypes))
            {
                data.EmteMedicineTypes.ForEach(o => o.EXP_MEST_TEMPLATE_ID = this.recentHisExpMestTemplate.ID);
                if (!new HisEmteMedicineTypeCreate(param).CreateList(data.EmteMedicineTypes))
                {
                    throw new Exception("Tao du lieu HisEmteMedicineType that bai");
                }
                this.recentHisEmteMedicineTypes = data.EmteMedicineTypes;
            }
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        /// <returns></returns>
        private void RollbackDataMedicine()
        {
            if (IsNotNullOrEmpty(this.recentHisEmteMedicineTypes))
            {
                if (!new HisEmteMedicineTypeTruncate(param).TruncateList(this.recentHisEmteMedicineTypes))
                {
                    LogSystem.Warn("Rollback thong tin HisEmteMedicineTypes that bai. Can kiem tra lai log.");
                }
                this.recentHisEmteMedicineTypes = null;
            }
        }
    }
}
