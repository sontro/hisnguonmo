using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisHoldReturn;
using MOS.MANAGER.HisHoreHoha;
using MOS.MANAGER.HisPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisHoreHandover.Unreceive
{
    class HisHoreHandoverUnreceiveCheck : BusinessBase
    {
        internal HisHoreHandoverUnreceiveCheck()
            : base()
        {

        }

        internal HisHoreHandoverUnreceiveCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsNotReturn(HIS_HORE_HANDOVER handover, ref List<HIS_HOLD_RETURN> holdReturns)
        {
            bool valid = true;
            try
            {
                List<HIS_HORE_HOHA> horeHohas = new HisHoreHohaGet(param).GetByHoreHandoverId(handover.ID);
                if (IsNotNullOrEmpty(horeHohas))
                {
                    List<HIS_HOLD_RETURN> listHolds = new HisHoldReturnGet(param).GetByIds(horeHohas.Select(s => s.HOLD_RETURN_ID).ToList());
                    if (!new HisHoldReturnCheck(param).IsNotReturn(listHolds))
                    {
                        return false;
                    }
                    holdReturns = listHolds;
                }
                else if (param.HasException)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.CoLoiXayRa);
                    throw new Exception("Co exception xay ra khi get HisHoreHohaGet.GetByHoreHandoverId");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool HasNotOtherHoreHandover(HIS_HORE_HANDOVER handover, List<HIS_HOLD_RETURN> holdReturns)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(holdReturns))
                {
                    string sqlQuery = new StringBuilder().Append("SELECT HH.* FROM HIS_HORE_HOHA HH")
                   .Append(" JOIN HIS_HORE_HANDOVER HAN ON HH.HORE_HANDOVER_ID = HAN.ID")
                   .Append(" WHERE HAN.ID <> ").Append(handover.ID)
                   .Append(" AND HAN.CREATE_TIME > ").Append(handover.CREATE_TIME.Value)
                   .Append(" AND %IN_CLAUSE% ").ToString();
                    string sql = DAOWorker.SqlDAO.AddInClause(holdReturns.Select(s => s.ID).ToList(), sqlQuery, "HOLD_RETURN_ID");

                    List<HIS_HORE_HOHA> exists = DAOWorker.SqlDAO.GetSql<HIS_HORE_HOHA>(sql);
                    if (IsNotNullOrEmpty(exists))
                    {
                        List<long> patientIds = holdReturns.Where(o => exists.Any(a => a.HOLD_RETURN_ID == o.ID)).Select(s => s.PATIENT_ID).ToList();
                        List<HIS_PATIENT> patients = new HisPatientGet().GetByIds(patientIds);
                        string codes = String.Join(",", patients.Select(s => s.PATIENT_CODE).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisHoreHandover_PhieuGiuTraCuaCacBenhNhanThuocPhieuBanGiaoKhac, codes);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
