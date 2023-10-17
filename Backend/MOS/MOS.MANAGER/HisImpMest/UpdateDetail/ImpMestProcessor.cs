using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UpdateDetail
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
        }

        internal bool Run(HisImpMestUpdateDetailSDO data, HIS_IMP_MEST impMest, HisImpMestUpdateDetailLog logProcessor)
        {
            bool result = false;
            try
            {
                if (impMest != null && impMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC)
                {
                    Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
                    HIS_IMP_MEST before = Mapper.Map<HIS_IMP_MEST>(impMest);
                    impMest.DELIVERER = data.Deliverer;
                    impMest.DESCRIPTION = data.Description;
                    impMest.DISCOUNT = data.DiscountPrice;
                    impMest.DISCOUNT_RATIO = data.DiscountRatio;
                    impMest.DOCUMENT_DATE = data.DocumentDate;
                    impMest.DOCUMENT_NUMBER = data.DocumentNumber;
                    impMest.DOCUMENT_PRICE = data.DocumentPrice;
                    if (!this.hisImpMestUpdate.Update(impMest, before))
                    {
                        throw new Exception("hisImpMestUpdate. Ket thuc nghiep vu");
                    }
                    logProcessor.GenerateLogMessage(before, impMest);
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
