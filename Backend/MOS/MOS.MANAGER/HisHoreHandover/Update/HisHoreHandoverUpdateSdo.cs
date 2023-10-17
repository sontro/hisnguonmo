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

namespace MOS.MANAGER.HisHoreHandover.Update
{
    class HisHoreHandoverUpdateSdo : BusinessBase
    {

        private HIS_HORE_HANDOVER recentHoreHandover = null;

        private HisHoreHandoverUpdate hisHoreHandoverUpdate;
        private HisHoreHohaCreate hisHoreHohaCreate;
        private HisHoldReturnUpdate hisHoldReturnUpdate;

        internal HisHoreHandoverUpdateSdo()
            : base()
        {
            this.Init();
        }

        internal HisHoreHandoverUpdateSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisHoreHandoverUpdate = new HisHoreHandoverUpdate(param);
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
                HIS_HORE_HANDOVER raw = null;
                List<HIS_HOLD_RETURN> holdReturns = new List<HIS_HOLD_RETURN>();
                List<HIS_HORE_HOHA> horeHohas = null;
                HisHoreHandoverUpdateCheck checker = new HisHoreHandoverUpdateCheck(param);
                HisHoreHandoverCheck commonChecker = new HisHoreHandoverCheck(param);
                HisHoldReturnCheck horeChecker = new HisHoldReturnCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.Id.Value, ref raw);
                valid = valid && commonChecker.IsNotReceive(raw);
                valid = valid && horeChecker.VerifyIds(data.HisHoldReturnIds, holdReturns);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && checker.CheckWorkingRoom(raw, data.WorkingRoomId);
                valid = valid && horeChecker.IsUnLock(holdReturns);
                valid = valid && checker.CheckNotHandovering(raw, holdReturns, ref horeHohas);
                valid = valid && horeChecker.IsNotReturn(holdReturns);
                valid = valid && horeChecker.VerifyResponsibleRoom(holdReturns, data.WorkingRoomId);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<HIS_HOLD_RETURN> removes = null;
                    this.ProcessHisHoreHandover(data, raw);
                    this.ProcessHisHoreHoha(data, horeHohas, ref removes, ref sqls);
                    this.ProcessHisHoldReturn(holdReturns, removes);

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls. " + sqls.ToString());
                    }

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

        private void ProcessHisHoreHandover(HisHoreHandoverCreateSDO data, HIS_HORE_HANDOVER raw)
        {
            if (raw.RECEIVE_ROOM_ID != data.ReceiveRoomId)
            {
                Mapper.CreateMap<HIS_HORE_HANDOVER, HIS_HORE_HANDOVER>();
                HIS_HORE_HANDOVER before = Mapper.Map<HIS_HORE_HANDOVER>(raw);
                raw.RECEIVE_ROOM_ID = data.ReceiveRoomId;
                if (!this.hisHoreHandoverUpdate.Update(raw, before))
                {
                    throw new Exception("hisHoreHandoverUpdate. Update HoreHandover that bai");
                }
            }
            this.recentHoreHandover = raw;
        }

        private void ProcessHisHoreHoha(HisHoreHandoverCreateSDO data, List<HIS_HORE_HOHA> oldHoreHohas, ref List<HIS_HOLD_RETURN> listRemove, ref List<string> sqls)
        {
            List<HIS_HOLD_RETURN> oldHoldReturns = null;
            if (IsNotNullOrEmpty(oldHoreHohas))
            {
                oldHoldReturns = new HisHoldReturnGet().GetByIds(oldHoreHohas.Select(s => s.HOLD_RETURN_ID).ToList());
            }
            List<HIS_HORE_HOHA> createds = new List<HIS_HORE_HOHA>();
            List<HIS_HORE_HOHA> updateds = new List<HIS_HORE_HOHA>();
            List<HIS_HORE_HOHA> deleteds = new List<HIS_HORE_HOHA>();

            foreach (long returnId in data.HisHoldReturnIds)
            {
                HIS_HORE_HOHA oldHH = oldHoreHohas != null ? oldHoreHohas.FirstOrDefault(o => o.HOLD_RETURN_ID == returnId) : null;
                if (oldHH != null)
                {
                    updateds.Add(oldHH);
                }
                else
                {
                    oldHH = new HIS_HORE_HOHA();
                    oldHH.HOLD_RETURN_ID = returnId;
                    oldHH.HORE_HANDOVER_ID = this.recentHoreHandover.ID;
                    createds.Add(oldHH);
                }
            }
            listRemove = oldHoldReturns != null ? oldHoldReturns.Where(o => !data.HisHoldReturnIds.Contains(o.ID)).ToList() : null;
            deleteds = oldHoreHohas != null ? oldHoreHohas.Where(o => updateds == null || !updateds.Any(a => a.ID == o.ID)).ToList() : null;

            if (IsNotNullOrEmpty(createds) && !this.hisHoreHohaCreate.CreateList(createds))
            {
                throw new Exception("hisHoreHohaCreate. Tao HoreHoha that bai");
            }

            if (IsNotNullOrEmpty(deleteds))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteds.Select(s => s.ID).ToList(), "DELETE HIS_HORE_HOHA WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
        }

        private void ProcessHisHoldReturn(List<HIS_HOLD_RETURN> holdReturns, List<HIS_HOLD_RETURN> removes)
        {
            List<HIS_HOLD_RETURN> listUpdate = new List<HIS_HOLD_RETURN>();
            List<HIS_HOLD_RETURN> listBefore = new List<HIS_HOLD_RETURN>();
            Mapper.CreateMap<HIS_HOLD_RETURN, HIS_HOLD_RETURN>();
            listBefore.AddRange(Mapper.Map<List<HIS_HOLD_RETURN>>(holdReturns));
            holdReturns.ForEach(o => o.IS_HANDOVERING = Constant.IS_TRUE);
            listUpdate.AddRange(holdReturns);

            if (IsNotNullOrEmpty(removes))
            {
                listBefore.AddRange(Mapper.Map<List<HIS_HOLD_RETURN>>(removes));
                removes.ForEach(o => o.IS_HANDOVERING = null);
                listUpdate.AddRange(removes);
            }

            if (!this.hisHoldReturnUpdate.UpdateList(listUpdate, listBefore))
            {
                throw new Exception("hisHoldReturnUpdate. Update HoldReturn that bai");
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
                this.hisHoreHandoverUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
