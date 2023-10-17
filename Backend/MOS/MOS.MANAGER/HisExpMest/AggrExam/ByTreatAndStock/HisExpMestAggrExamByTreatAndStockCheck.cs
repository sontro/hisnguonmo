using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.AggrExam.ByTreatAndStock
{
    class HisExpMestAggrExamByTreatAndStockCheck : BusinessBase
    {
        internal HisExpMestAggrExamByTreatAndStockCheck()
            : base()
        {
        }

        internal HisExpMestAggrExamByTreatAndStockCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(AggrExamByTreatAndStockSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (string.IsNullOrWhiteSpace(data.TreatmentCode)) throw new ArgumentNullException("data.TreatmentCode");
                if (!IsGreaterThanZero(data.MediStockId)) throw new ArgumentNullException("data.MediStockId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidExpMest(long treatmentId, long mediStockId, ref List<HIS_EXP_MEST> aggrs)
        {
            bool valid = true;
            try
            {
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.TDL_TREATMENT_ID = treatmentId;
                filter.MEDI_STOCK_ID = mediStockId;
                filter.HAS_EXP_MEST_TYPE_ID_IS_DPK_OR_THPK = true;
                aggrs = new HisExpMestGet().Get(filter);
                if (!IsNotNullOrEmpty(aggrs))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongCoDonThuoc);
                    return false;
                }
                else
                {
                    if (aggrs.All(o => o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE))
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DonDaDuocPhat);
                        return false;
                    }
                    else if (aggrs.All(o => o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT))
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DonDaTuChoiPhat);
                        return false;
                    }
                    else if (aggrs.All(o => (o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT
                                        || o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)))
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DonDaDuocPhatHoacTuChoiPhat);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidTreatmentCode(string treatmentCode, ref HIS_TREATMENT raw)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(treatmentCode))
                {
                    raw = new HisTreatmentGet().GetByCode(treatmentCode);
                    if (raw == null)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_KhongTimThayThongTinHoSo);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
