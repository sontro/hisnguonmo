using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Finish
{
    class HisExpMestFinish : BusinessBase
    {
        HisExpMestUpdate hisExpMestUpdate;

        internal HisExpMestFinish()
            : base()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal HisExpMestFinish(CommonParam param)
            : base(param)
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST raw = null;
                HIS_EXP_MEST before = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                HisExpMestCheck checker = new HisExpMestCheck(param);
                HisExpMestFinishCheck finishChecker = new HisExpMestFinishCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && finishChecker.IsAllowFinish(data, ref raw, ref materials, ref medicines);
                valid = valid && checker.HasNotInExpMestAggr(raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    before = Mapper.Map<HIS_EXP_MEST>(raw);
                    this.SetIsExportEqualIsRequest(raw, materials, medicines);
                    raw.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                    raw.FINISH_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    if (!this.hisExpMestUpdate.Update(raw, before))
                    {
                        throw new Exception("Update hoan thanh phieu xuat that bai");
                    }
                    result = true;
                    resultData = raw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void SetIsExportEqualIsRequest(HIS_EXP_MEST raw, List<HIS_EXP_MEST_MATERIAL> materials, List<HIS_EXP_MEST_MEDICINE> medicines)
        {
            List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs = new HisExpMestMatyReqGet().GetByExpMestId(raw.ID);
            List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs = new HisExpMestMetyReqGet().GetByExpMestId(raw.ID);

            bool isEqual = true;

            //Kiem tra voi vat tu
            if (IsNotNullOrEmpty(expMestMatyReqs))
            {
                var matyReqs = expMestMatyReqs.GroupBy(o => o.MATERIAL_TYPE_ID);
                foreach (var t in matyReqs)
                {
                    decimal totalExecuteAmount = IsNotNullOrEmpty(materials) ?
                        materials.Where(o => o.TDL_MATERIAL_TYPE_ID == t.Key).Sum(o => o.AMOUNT) : 0;
                    decimal totalReqAmount = t.Sum(o => o.AMOUNT);
                    if (totalExecuteAmount < totalReqAmount)
                    {
                        isEqual = false;
                    }
                }
            }

            //Kiem tra voi thuoc
            if (isEqual && IsNotNullOrEmpty(expMestMetyReqs))
            {
                var metyReqs = expMestMetyReqs.GroupBy(o => o.MEDICINE_TYPE_ID);
                foreach (var t in metyReqs)
                {
                    decimal totalExecuteAmount = IsNotNullOrEmpty(medicines) ?
                        medicines.Where(o => o.TDL_MEDICINE_TYPE_ID == t.Key).Sum(o => o.AMOUNT) : 0;
                    decimal totalReqAmount = t.Sum(o => o.AMOUNT);
                    if (totalExecuteAmount < totalReqAmount)
                    {
                        isEqual = false;
                    }
                }
            }

            if (isEqual)
            {
                raw.IS_EXPORT_EQUAL_REQUEST = Constant.IS_TRUE;
            }
            else
            {
                raw.IS_EXPORT_EQUAL_REQUEST = null;
            }
        }
    }
}
