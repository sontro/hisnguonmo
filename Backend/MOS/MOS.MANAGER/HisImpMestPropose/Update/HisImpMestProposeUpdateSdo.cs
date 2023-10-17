using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMestPropose.Update
{
    class HisImpMestProposeUpdateSdo : BusinessBase
    {

        private HisImpMestProposeUpdate hisImpMestProposeUpdate;
        private HisImpMestUpdate hisImpMestUpdate;

        internal HisImpMestProposeUpdateSdo()
            : base()
        {
            this.Init();
        }

        internal HisImpMestProposeUpdateSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestProposeUpdate = new HisImpMestProposeUpdate(param);
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
        }

        internal bool Run(HisImpMestProposeSDO data, ref HisImpMestProposeResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_IMP_MEST> impMests = new List<HIS_IMP_MEST>();
                WorkPlaceSDO workPlace = null;
                HIS_IMP_MEST_PROPOSE raw = null;
                HisImpMestProposeUpdateCheck checker = new HisImpMestProposeUpdateCheck(param);
                HisImpMestProposeCheck commonChecker = new HisImpMestProposeCheck(param);
                HisImpMestCheck impMestChecker = new HisImpMestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                
                valid = valid && commonChecker.VerifyId(data.Id.Value, ref raw);
                valid = valid && commonChecker.IsUnLock(raw);
                valid = valid && commonChecker.CheckWorkingRoom(data.WorkingRoomId, raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && commonChecker.IsNotExistsImpMestPay(raw);
                valid = valid && impMestChecker.VerifyIds(data.ImpMestIds, impMests);
                valid = valid && impMestChecker.IsUnLock(impMests);
                valid = valid && impMestChecker.IsNotHasImpMestProposeId(impMests, raw.ID);
                valid = valid && checker.IsValidMedicalContract(impMests, raw); 
                valid = valid && commonChecker.CheckImpMestType(impMests);
                valid = valid && commonChecker.CheckImpMestWorkingRoom(workPlace, impMests);
                valid = valid && commonChecker.CheckImpMestSupplier(impMests, data.SupplierId);
                if (valid)
                {
                    this.ProcessHisImpMestPropose(data, raw);
                    this.ProcessHisImpMest(raw, impMests);

                    this.PassResult(raw, ref resultData);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMestPropose(HisImpMestProposeSDO data, HIS_IMP_MEST_PROPOSE raw)
        {
            if (data.SupplierId != raw.SUPPLIER_ID)
            {
                Mapper.CreateMap<HIS_IMP_MEST_PROPOSE, HIS_IMP_MEST_PROPOSE>();
                HIS_IMP_MEST_PROPOSE before = Mapper.Map<HIS_IMP_MEST_PROPOSE>(raw);
                raw.SUPPLIER_ID = data.SupplierId;
                if (!hisImpMestProposeUpdate.Update(raw, before))
                {
                    throw new Exception("hisImpMestProposeUpdate. Cap nhat HIS_IMP_MEST_PROPOSE that bai");
                }
            }
        }

        private void ProcessHisImpMest(HIS_IMP_MEST_PROPOSE raw, List<HIS_IMP_MEST> impMests)
        {
            List<HIS_IMP_MEST> oldImpMests = new HisImpMestGet().GetByImpMestProposeId(raw.ID);
            List<HIS_IMP_MEST> listUpdate = new List<HIS_IMP_MEST>();
            List<HIS_IMP_MEST> listBefore = new List<HIS_IMP_MEST>();

            List<HIS_IMP_MEST> removes = oldImpMests != null ? oldImpMests.Where(o => impMests == null || !impMests.Any(a => a.ID == o.ID)).ToList() : null;
            List<HIS_IMP_MEST> updates = impMests != null ? impMests.Where(o => oldImpMests == null || !oldImpMests.Any(a => a.ID == o.ID)).ToList() : null;

            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            if (IsNotNullOrEmpty(updates))
            {
                listBefore.AddRange(Mapper.Map<List<HIS_IMP_MEST>>(updates));
                updates.ForEach(o => o.IMP_MEST_PROPOSE_ID = raw.ID);
                listUpdate.AddRange(updates);
            }

            if (IsNotNullOrEmpty(removes))
            {
                listBefore.AddRange(Mapper.Map<List<HIS_IMP_MEST>>(removes));
                removes.ForEach(o => o.IMP_MEST_PROPOSE_ID = null);
                listUpdate.AddRange(removes);
            }

            if (IsNotNullOrEmpty(listUpdate) && !this.hisImpMestUpdate.UpdateList(listUpdate, listBefore))
            {
                throw new Exception("hisImpMestUpdate. Cap nhat HIS_IMP_MEST that bai");
            }
        }

        private void PassResult(HIS_IMP_MEST_PROPOSE raw, ref HisImpMestProposeResultSDO resultData)
        {
            resultData = new HisImpMestProposeResultSDO();
            resultData.ImpMestProposes = new List<V_HIS_IMP_MEST_PROPOSE>(){new HisImpMestProposeGet().GetViewById(raw.ID)};
            resultData.HisImpMests = new HisImpMestGet().GetViewByImpMestProposeId(raw.ID);
        }

        private void Rollback()
        {
            try
            {
                this.hisImpMestUpdate.RollbackData();
                this.hisImpMestProposeUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
