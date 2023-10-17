using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisHoldReturn;
using MOS.MANAGER.HisHoreHoha;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisHoreHandover.Receive
{
    class HisHoreHandoverReceive : BusinessBase
    {
        private HisHoreHandoverUpdate hisHoreHandoverUpdate;
        private HisHoldReturnUpdate hisHoldReturnUpdate;

        internal HisHoreHandoverReceive()
            : base()
        {
            this.Init();
        }

        internal HisHoreHandoverReceive(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisHoreHandoverUpdate = new HisHoreHandoverUpdate(param);
            this.hisHoldReturnUpdate = new HisHoldReturnUpdate(param);
        }

        internal bool Run(HisHoreHandoverSDO data, ref HisHoreHandoverResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_HORE_HANDOVER raw = null;
                WorkPlaceSDO workplace = null;
                HisHoreHandoverCheck checker = new HisHoreHandoverCheck(param);
                valid = valid && checker.VerifyId(data.Id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && checker.CheckReceiveWorkingRoom(raw, data.WorkingRoomId);
                valid = valid && checker.IsNotReceive(raw);
                if (valid)
                {
                    this.ProcessHisHoreHandover(raw);
                    this.ProcessHisHoldReturn(raw);
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

        private void ProcessHisHoreHandover(HIS_HORE_HANDOVER raw)
        {
            Mapper.CreateMap<HIS_HORE_HANDOVER, HIS_HORE_HANDOVER>();
            HIS_HORE_HANDOVER before = Mapper.Map<HIS_HORE_HANDOVER>(raw);
            raw.HORE_HANDOVER_STT_ID = IMSys.DbConfig.HIS_RS.HIS_HORE_HANDOVER_STT.ID__RECEIVE;
            raw.RECEIVE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            raw.RECEIVE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            if (!this.hisHoreHandoverUpdate.Update(raw, before))
            {
                throw new Exception("hisHoreHandoverUpdate. Update HIS_HORE_HANDOVER that bai");
            }
        }

        private void ProcessHisHoldReturn(HIS_HORE_HANDOVER raw)
        {
            List<HIS_HORE_HOHA> hisHoreHohas = new HisHoreHohaGet(param).GetByHoreHandoverId(raw.ID);
            if (IsNotNullOrEmpty(hisHoreHohas))
            {
                List<HIS_HOLD_RETURN> holdReturns = new HisHoldReturnGet().GetByIds(hisHoreHohas.Select(s => s.HOLD_RETURN_ID).ToList());
                Mapper.CreateMap<HIS_HOLD_RETURN, HIS_HOLD_RETURN>();
                List<HIS_HOLD_RETURN> befores = Mapper.Map<List<HIS_HOLD_RETURN>>(holdReturns);
                holdReturns.ForEach(o =>
                {
                    o.IS_HANDOVERING = null;
                    o.RESPONSIBLE_ROOM_ID = raw.RECEIVE_ROOM_ID;
                });

                if (!this.hisHoldReturnUpdate.UpdateList(holdReturns, befores))
                {
                    throw new Exception("hisHoldReturnUpdate. Cap nhat HIS_HOLD_RETURN that bai");
                }
            }
            else if (param.HasException)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.CoLoiXayRa);
                throw new Exception("Co exception xay ra khi get HisHoreHohaGet.GetByHoreHandoverId");
            }
        }

        private void PassResult(HIS_HORE_HANDOVER raw, ref HisHoreHandoverResultSDO resultData)
        {
            resultData = new HisHoreHandoverResultSDO();
            resultData.HoreHandover = new HisHoreHandoverGet().GetViewById(raw.ID);
            resultData.HoreHohas = new HisHoreHohaGet().GetViewByHoreHandoverId(raw.ID);
        }

        private void Rollback()
        {
            try
            {
                this.hisHoldReturnUpdate.RollbackData();
                this.hisHoreHandoverUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
