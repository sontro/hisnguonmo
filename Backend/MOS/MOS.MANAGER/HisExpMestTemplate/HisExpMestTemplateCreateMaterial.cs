using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmteMaterialType;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestTemplate
{
    partial class HisExpMestTemplateCreate : BusinessBase
    {
        private List<HIS_EMTE_MATERIAL_TYPE> recentHisEmteMaterialTypes;

        /// <summary>
        /// Xu ly du lieu chi tiet cua template
        /// </summary>
        private void ProcessEmteMaterialType(HisExpMestTemplateSDO data)
        {
            if (IsNotNullOrEmpty(data.EmteMaterialTypes))
            {
                data.EmteMaterialTypes.ForEach(o => o.EXP_MEST_TEMPLATE_ID = this.recentHisExpMestTemplate.ID);
                if (!new HisEmteMaterialTypeCreate(param).CreateList(data.EmteMaterialTypes))
                {
                    throw new Exception("Tao du lieu HisEmteMaterialType that bai");
                }
                this.recentHisEmteMaterialTypes = data.EmteMaterialTypes;
            }
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        /// <returns></returns>
        private void RollbackDataMaterial()
        {
            if (IsNotNullOrEmpty(this.recentHisEmteMaterialTypes))
            {
                if (!new HisEmteMaterialTypeTruncate(param).TruncateList(this.recentHisEmteMaterialTypes))
                {
                    LogSystem.Warn("Rollback thong tin HisEmteMaterialTypes that bai. Can kiem tra lai log.");
                }
                this.recentHisEmteMaterialTypes = null;
            }
        }
    }
}
