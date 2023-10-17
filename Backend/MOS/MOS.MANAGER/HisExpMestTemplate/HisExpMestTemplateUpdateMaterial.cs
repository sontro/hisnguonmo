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
    partial class HisExpMestTemplateUpdate : BusinessBase
    {
        private List<HIS_EMTE_MATERIAL_TYPE> recentHisEmteMaterialTypes;

        /// <summary>
        /// Xu ly du lieu chi tiet cua template
        /// </summary>
        private void ProcessEmteMaterialType(HisExpMestTemplateSDO data)
        {
            //Xoa het du lieu cu
            HisEmteMaterialTypeFilterQuery filter = new HisEmteMaterialTypeFilterQuery();
            filter.EXP_MEST_TEMPLATE_ID = data.ExpMestTemplate.ID;
            List<HIS_EMTE_MATERIAL_TYPE> exists = new HisEmteMaterialTypeGet(param).Get(filter);
            if (IsNotNullOrEmpty(exists))
            {
                if (!new HisEmteMaterialTypeTruncate(param).TruncateList(exists))
                {
                    throw new Exception("Xoa du lieu HisEmteMaterialType cu that bai");
                }
            }

            //Cap nhat ID cua EXP_MEST_TEMPLATE_ID va ID cua cac ban ghi
            //(update ID ve 0 de trong truong hop client gui len cac ban ghi da ton tai truoc do)
            if (IsNotNullOrEmpty(data.EmteMaterialTypes))
            {
                data.EmteMaterialTypes.ForEach(o =>
                {
                    o.EXP_MEST_TEMPLATE_ID = this.recentHisExpMestTemplate.ID;
                    o.ID = 0;
                });

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
