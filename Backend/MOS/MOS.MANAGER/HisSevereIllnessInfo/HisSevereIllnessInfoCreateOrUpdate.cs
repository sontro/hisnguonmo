using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEventsCausesDeath;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    class HisSevereIllnessInfoCreateOrUpdate : BusinessBase
    {
        private List<HIS_EVENTS_CAUSES_DEATH> listEventsCausesDeathDeleted = new List<HIS_EVENTS_CAUSES_DEATH>();
        private bool isDeleted = false;

        private HisSevereIllnessInfoCreate severeIllnessInfoCreate;
        private HisSevereIllnessInfoUpdate severeIllnessInfoUpdate;

        private HisEventsCausesDeathCreate eventsCauseDeathCreate;
		
        internal HisSevereIllnessInfoCreateOrUpdate()
            : base()
        {
            this.Init();
        }

        internal HisSevereIllnessInfoCreateOrUpdate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.severeIllnessInfoCreate = new HisSevereIllnessInfoCreate(param);
            this.severeIllnessInfoUpdate = new HisSevereIllnessInfoUpdate(param);

            this.eventsCauseDeathCreate = new HisEventsCausesDeathCreate(param);
        }

        internal bool CreateOrUpdate(SevereIllnessInfoSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;

                HIS_SEVERE_ILLNESS_INFO raw = null;

                HisSevereIllnessInfoCheck checker = new HisSevereIllnessInfoCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                if (data.SevereIllnessInfo.ID > 0)
                {
                    valid = valid && checker.VerifyId(data.SevereIllnessInfo.ID, ref raw);
                }
                if (valid)
                {
                    if (IsNotNull(raw))
                    {
                        this.ProcessUpdateSevereIllnessInfo(data.SevereIllnessInfo);
                        this.ProcessDeleteOldEventsCausesDeaths(data.SevereIllnessInfo.ID);
                        this.ProcessCreateEventsCausesDeaths(data.EventsCausesDeaths, data.SevereIllnessInfo.ID);
                        new EventLogGenerator(EventLog.Enum.HisSevereIllnessInfo_CapNhatThongTin).Run();
                    }
                    else
                    {
                        this.ProcessSevereIllnessInfo(data.SevereIllnessInfo);
                        this.ProcessCreateEventsCausesDeaths(data.EventsCausesDeaths, data.SevereIllnessInfo.ID);
                        new EventLogGenerator(EventLog.Enum.HisSevereIllnessInfo_ThemMoiThongTin).Run();
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessUpdateSevereIllnessInfo(HIS_SEVERE_ILLNESS_INFO data)
        {
            if (IsNotNull(data) && !this.severeIllnessInfoUpdate.Update(data))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback");
            }
        }

        private void ProcessDeleteOldEventsCausesDeaths(long severeIllnessInfoId)
        {
            var oldChildren = new HisEventsCausesDeathGet().GetBySevereIllnessInfoId(severeIllnessInfoId);
            if (IsNotNullOrEmpty(oldChildren))
            {
                Mapper.CreateMap<HIS_EVENTS_CAUSES_DEATH, HIS_EVENTS_CAUSES_DEATH>();
                this.listEventsCausesDeathDeleted = Mapper.Map<List<HIS_EVENTS_CAUSES_DEATH>>(oldChildren);
                if (!DAOWorker.HisEventsCausesDeathDAO.TruncateList(oldChildren))
                {
                    throw new Exception("cap nhat: Xoa lieu cu HisEventsCausesDeath theo HisSevereIllnessInfo that bai." + LogUtil.TraceData("severeIllnessInfoId", severeIllnessInfoId));
                }
                isDeleted = true;
            }
        }

        private void ProcessSevereIllnessInfo(HIS_SEVERE_ILLNESS_INFO data)
        {
            if (IsNotNull(data) && !this.severeIllnessInfoCreate.Create(data))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback");
            }
        }

        private void ProcessCreateEventsCausesDeaths(List<HIS_EVENTS_CAUSES_DEATH> data, long severeIllnessInfo)
        {
            if (IsNotNullOrEmpty(data))
            {
                data.ForEach(o => o.SEVERE_ILLNESS_INFO_ID = severeIllnessInfo);
                if (!this.eventsCauseDeathCreate.CreateList(data))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback");
                }
            }
        }
		
		internal void RollbackData()
        {
            this.eventsCauseDeathCreate.RollbackData();
            if (isDeleted && IsNotNullOrEmpty(this.listEventsCausesDeathDeleted))
            {
                if (!DAOWorker.HisEventsCausesDeathDAO.CreateList(this.listEventsCausesDeathDeleted))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEventsCausesDeath_ThemMoiThatBai);
                    throw new Exception("RollbackData that bai: Them moi thong tin HisEventsCausesDeath that bai." + LogUtil.TraceData("listData", this.listEventsCausesDeathDeleted));
                }
            }
            this.severeIllnessInfoUpdate.RollbackData();
            this.severeIllnessInfoCreate.RollbackData();
        }
    }
}
