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

namespace MOS.MANAGER.HisImpMestPropose.Create
{
    class HisImpMestProposeCreateSdo : BusinessBase
    {
        private List<HIS_IMP_MEST_PROPOSE> recentImpMestProposes = null;

        private HisImpMestProposeCreate hisImpMestProposeCreate;
        private HisImpMestUpdate hisImpMestUpdate;

        internal HisImpMestProposeCreateSdo()
            : base()
        {
            this.Init();
        }

        internal HisImpMestProposeCreateSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestProposeCreate = new HisImpMestProposeCreate(param);
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
                HisImpMestProposeCreateCheck checker = new HisImpMestProposeCreateCheck(param);
                HisImpMestProposeCheck commonChecker = new HisImpMestProposeCheck(param);
                HisImpMestCheck impMestChecker = new HisImpMestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && impMestChecker.VerifyIds(data.ImpMestIds, impMests);
                valid = valid && impMestChecker.IsUnLock(impMests);
                valid = valid && impMestChecker.IsNotHasImpMestProposeId(impMests, null);
                valid = valid && commonChecker.CheckImpMestType(impMests);
                valid = valid && commonChecker.CheckImpMestWorkingRoom(workPlace, impMests);
                valid = valid && commonChecker.CheckImpMestSupplier(impMests, data.SupplierId);
                if (valid)
                {
                    this.ProcessHisImpMestPropose(data, impMests, workPlace);
                    this.ProcessHisImpMest(impMests);

                    this.PassResult(impMests, ref resultData);
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

        private void ProcessHisImpMestPropose(HisImpMestProposeSDO data, List<HIS_IMP_MEST> impMests, WorkPlaceSDO workPlace)
        {
            List<HIS_IMP_MEST_PROPOSE> impMestProposes = new List<HIS_IMP_MEST_PROPOSE>();
            var group = impMests.GroupBy(o => o.MEDICAL_CONTRACT_ID);
            foreach (var g in group)
            {
                HIS_IMP_MEST_PROPOSE propose = new HIS_IMP_MEST_PROPOSE();
                propose.PROPOSE_ROOM_ID = workPlace.RoomId;
                propose.PROPOSE_DEPARTMENT_ID = workPlace.DepartmentId;
                propose.SUPPLIER_ID = data.SupplierId;
                propose.MEDICAL_CONTRACT_ID = g.Key;
                propose.PROPOSE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                propose.PROPOSE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                impMestProposes.Add(propose);
            }

            if (!this.hisImpMestProposeCreate.CreateList(impMestProposes))
            {
                throw new Exception("hisImpMestProposeCreate. Tao HIS_IMP_MEST_PROPOSE that bai");
            }
            this.recentImpMestProposes = impMestProposes;
        }

        private void ProcessHisImpMest(List<HIS_IMP_MEST> impMests)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            List<HIS_IMP_MEST> befores = Mapper.Map<List<HIS_IMP_MEST>>(impMests);

            foreach (HIS_IMP_MEST imp in impMests)
            {
                HIS_IMP_MEST_PROPOSE propose = this.recentImpMestProposes.Where(o => o.MEDICAL_CONTRACT_ID == imp.MEDICAL_CONTRACT_ID).FirstOrDefault();
                imp.IMP_MEST_PROPOSE_ID = propose.ID;
            }

            if (!this.hisImpMestUpdate.UpdateList(impMests, befores))
            {
                throw new Exception("hisImpMestUpdate. Cap nhat  HIS_IMP_MEST that bai");
            }
        }

        private void PassResult(List<HIS_IMP_MEST> impMests, ref HisImpMestProposeResultSDO resultData)
        {
            resultData = new HisImpMestProposeResultSDO();
            resultData.ImpMestProposes = new HisImpMestProposeGet().GetViewByIds(this.recentImpMestProposes.Select(o => o.ID).ToList());
            resultData.HisImpMests = new HisImpMestGet().GetViewByIds(impMests.Select(o => o.ID).ToList());
        }

        private void Rollback()
        {
            try
            {
                this.hisImpMestUpdate.RollbackData();
                this.hisImpMestProposeCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
