using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisHoldReturn;
using MOS.MANAGER.HisHoreHoha;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisHoreHandover.Create
{
    class HisHoreHandoverCreateSdo : BusinessBase
    {

        private HIS_HORE_HANDOVER recentHoreHandover;

        private HisHoreHandoverCreate hisHoreHandoverCreate;
        private HisHoreHohaCreate hisHoreHohaCreate;
        private HisHoldReturnUpdate hisHoldReturnUpdate;

        internal HisHoreHandoverCreateSdo()
            : base()
        {
            this.Init();
        }

        internal HisHoreHandoverCreateSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisHoreHandoverCreate = new HisHoreHandoverCreate(param);
            this.hisHoreHohaCreate = new HisHoreHohaCreate(param);
            this.hisHoldReturnUpdate = new HisHoldReturnUpdate(param);
        }

        internal bool Run(HisHoreHandoverCreateSDO data, ref HisHoreHandoverResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workplace = null;
                List<HIS_HOLD_RETURN> holdReturns = new List<HIS_HOLD_RETURN>();
                HisHoreHandoverCreateCheck checker = new HisHoreHandoverCreateCheck(param);
                HisHoldReturnCheck horeChecker = new HisHoldReturnCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && horeChecker.VerifyIds(data.HisHoldReturnIds, holdReturns);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && horeChecker.IsUnLock(holdReturns);
                valid = valid && horeChecker.IsNotHandovering(holdReturns);
                valid = valid && horeChecker.IsNotReturn(holdReturns);
                valid = valid && horeChecker.VerifyResponsibleRoom(holdReturns, data.WorkingRoomId);

                if (valid)
                {
                    this.ProcessHisHoreHandover(data);
                    this.ProcessHisHoreHoha(data);
                    this.ProcessHisHoldReturn(holdReturns);

                    this.PassResult(ref resultData);
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

        private void ProcessHisHoreHandover(HisHoreHandoverCreateSDO data)
        {
            HIS_HORE_HANDOVER horeHandover = new HIS_HORE_HANDOVER();
            horeHandover.HORE_HANDOVER_STT_ID = IMSys.DbConfig.HIS_RS.HIS_HORE_HANDOVER_STT.ID__REQUEST;
            horeHandover.SEND_ROOM_ID = data.WorkingRoomId;
            horeHandover.RECEIVE_ROOM_ID = data.ReceiveRoomId;
            horeHandover.SEND_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            horeHandover.SEND_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            if (!this.hisHoreHandoverCreate.Create(horeHandover))
            {
                throw new Exception("hisHoreHandoverCreate. Tao HoreHandover that bai");
            }
            this.recentHoreHandover = horeHandover;
        }

        private void ProcessHisHoreHoha(HisHoreHandoverCreateSDO data)
        {
            List<HIS_HORE_HOHA> hisHoreHohas = new List<HIS_HORE_HOHA>();
            foreach (long returnId in data.HisHoldReturnIds)
            {
                HIS_HORE_HOHA hoha = new HIS_HORE_HOHA();
                hoha.HOLD_RETURN_ID = returnId;
                hoha.HORE_HANDOVER_ID = this.recentHoreHandover.ID;
                hisHoreHohas.Add(hoha);
            }

            if (!this.hisHoreHohaCreate.CreateList(hisHoreHohas))
            {
                throw new Exception("hisHoreHohaCreate. Tao HoreHoha that bai");
            }
        }

        private void ProcessHisHoldReturn(List<HIS_HOLD_RETURN> holdReturns)
        {
            Mapper.CreateMap<HIS_HOLD_RETURN, HIS_HOLD_RETURN>();
            List<HIS_HOLD_RETURN> befores = Mapper.Map<List<HIS_HOLD_RETURN>>(holdReturns);
            holdReturns.ForEach(o => o.IS_HANDOVERING = Constant.IS_TRUE);
            if (!hisHoldReturnUpdate.UpdateList(holdReturns, befores))
            {
                throw new Exception("hisHoldReturnUpdate. Update IsHandover that bai");
            }
        }

        private void PassResult(ref HisHoreHandoverResultSDO resultData)
        {
            resultData = new HisHoreHandoverResultSDO();
            resultData.HoreHandover = new HisHoreHandoverGet().GetViewById(this.recentHoreHandover.ID);
            resultData.HoreHohas = new HisHoreHohaGet().GetViewByHoreHandoverId(this.recentHoreHandover.ID);
        }

        private void Rollback()
        {
            try
            {
                this.hisHoldReturnUpdate.RollbackData();
                this.hisHoreHohaCreate.RollbackData();
                this.hisHoreHandoverCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
