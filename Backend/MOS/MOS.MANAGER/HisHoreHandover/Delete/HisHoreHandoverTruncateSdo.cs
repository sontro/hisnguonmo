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

namespace MOS.MANAGER.HisHoreHandover.Delete
{
    class HisHoreHandoverTruncateSdo : BusinessBase
    {
        internal HisHoreHandoverTruncateSdo()
            : base()
        {

        }

        internal HisHoreHandoverTruncateSdo(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisHoreHandoverSDO data)
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
                valid = valid && checker.CheckSendWorkingRoom(raw, data.WorkingRoomId);
                valid = valid && checker.IsNotReceive(raw);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<HIS_HORE_HOHA> hisHoreHohas = null;
                    this.ProcessHisHoreHoha(raw, ref hisHoreHohas, ref sqls);
                    this.ProcessHisHoreHandover(raw, ref sqls);
                    this.ProcessHisHoldReturn(hisHoreHohas, ref sqls);

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    result = true;
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

        private void ProcessHisHoreHoha(HIS_HORE_HANDOVER raw, ref List<HIS_HORE_HOHA> hisHoreHohas, ref List<string> sqls)
        {
            List<HIS_HORE_HOHA> horeHohas = new HisHoreHohaGet().GetByHoreHandoverId(raw.ID);
            if (IsNotNullOrEmpty(horeHohas))
            {
                if (!new HisHoreHohaCheck(param).IsUnLock(horeHohas))
                {
                    throw new Exception("Ton tai HIS_HORE_HOHA co IS_ACTIVE = 0");
                }
                string sql = String.Format("DELETE HIS_HORE_HOHA WHERE HORE_HANDOVER_ID = {0}", raw.ID);
                sqls.Add(sql);
                hisHoreHohas = horeHohas;
            }
        }

        private void ProcessHisHoreHandover(HIS_HORE_HANDOVER raw, ref List<string> sqls)
        {
            string sql = String.Format("DELETE HIS_HORE_HANDOVER WHERE ID = {0}", raw.ID);
            sqls.Add(sql);
        }

        private void ProcessHisHoldReturn(List<HIS_HORE_HOHA> hisHoreHohas, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(hisHoreHohas))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(hisHoreHohas.Select(s => s.HOLD_RETURN_ID).ToList(), "UPDATE HIS_HOLD_RETURN SET IS_HANDOVERING = NULL WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
        }
    }
}
