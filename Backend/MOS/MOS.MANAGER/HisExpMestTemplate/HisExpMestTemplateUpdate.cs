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
    partial class HisExpMestTemplateUpdate : BusinessBase
    {
        private HIS_EXP_MEST_TEMPLATE recentHisExpMestTemplate;

        internal HisExpMestTemplateUpdate()
            : base()
        {

        }

        internal HisExpMestTemplateUpdate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Update(HisExpMestTemplateSDO data)
        {
            bool result = false;
            try
            {
                if (data == null || data.ExpMestTemplate == null || (!IsNotNullOrEmpty(data.EmteMedicineTypes) && !IsNotNullOrEmpty(data.EmteMaterialTypes)))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMestTemplate_KhongCoDuLieuThuocVatTu);
                    throw new Exception("Du lieu ko hop le. Data null hoac EmteMedicineTypes rong");
                }
                this.ProcessExpMestTemplate(data);
                this.ProcessEmteMedicineType(data);
                this.ProcessEmteMaterialType(data);
                this.PassResult(data);
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                data = null;
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Xu ly du lieu template
        /// </summary>
        private void ProcessExpMestTemplate(HisExpMestTemplateSDO data)
        {
            bool valid = true;
            HisExpMestTemplateCheck checker = new HisExpMestTemplateCheck(param);
            valid = valid && checker.VerifyRequireField(data.ExpMestTemplate);
            valid = valid && IsGreaterThanZero(data.ExpMestTemplate.ID);
            valid = valid && checker.IsUnLock(data.ExpMestTemplate.ID);
            valid = valid && checker.ExistsCode(data.ExpMestTemplate.EXP_MEST_TEMPLATE_CODE, data.ExpMestTemplate.ID);

            if (!valid || !DAOWorker.HisExpMestTemplateDAO.Update(data.ExpMestTemplate))
            {
                throw new Exception("Update du lieu ExpMestTemplate that bai");
            }
            this.recentHisExpMestTemplate = data.ExpMestTemplate;
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        /// <returns></returns>
        private void RollbackData()
        {
            this.RollbackDataMaterial();
            this.RollbackDataMedicine();
            if (this.recentHisExpMestTemplate != null)
            {
                if (!new HisExpMestTemplateTruncate(param).Truncate(this.recentHisExpMestTemplate.ID))
                {
                    LogSystem.Warn("Rollback thong tin HisExpMestTemplate that bai. Can kiem tra lai log.");
                }
                this.recentHisExpMestTemplate = null;
            }
        }

        /// <summary>
        /// Truyen ket qua thong qua bien "data"
        /// </summary>
        /// <param name="data"></param>
        private void PassResult(HisExpMestTemplateSDO resultData)
        {
            resultData = new HisExpMestTemplateSDO();
            resultData.ExpMestTemplate = this.recentHisExpMestTemplate;

            if (this.recentHisEmteMedicineTypes != null)
            {
                resultData.EmteMedicineTypes = this.recentHisEmteMedicineTypes;
            }
        }
    }
}
