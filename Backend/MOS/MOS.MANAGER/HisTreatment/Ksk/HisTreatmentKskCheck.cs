using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Ksk
{
    class HisTreatmentKskCheck : BusinessBase
    {
        internal HisTreatmentKskCheck()
            : base()
        {

        }

        internal HisTreatmentKskCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }


        internal bool IsNotApprove(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.IsNotApprove(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotApprove(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> isApproves = datas != null ? datas.Where(o => o.IS_KSK_APPROVE.HasValue && o.IS_KSK_APPROVE.Value == Constant.IS_TRUE).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(isApproves))
                {
                    string codes = String.Join(",", isApproves);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDieuTriDaDuocDuyetKhamSucKhoe, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsKskTreatment(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.IsKskTreatment(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsKskTreatment(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> isNotKsks = datas != null ? datas.Where(o => !o.TDL_KSK_CONTRACT_ID.HasValue).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(isNotKsks))
                {
                    string codes = String.Join(",", isNotKsks);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDieuTriKhongPhaiHoSoKhamSucKhoe, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsApprove(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                valid = this.IsApprove(new List<HIS_TREATMENT>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsApprove(List<HIS_TREATMENT> datas)
        {
            bool valid = true;
            try
            {
                List<string> isNotApproves = datas != null ? datas.Where(o => !o.IS_KSK_APPROVE.HasValue || o.IS_KSK_APPROVE.Value != Constant.IS_TRUE).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(isNotApproves))
                {
                    string codes = String.Join(",", isNotApproves);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDieuTriChuaDuocDuyetKhamSucKhoe, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistsReqFinishOrProcessing(long treatmentId)
        {
            bool valid = true;
            try
            {
                valid = this.IsNotExistsReqFinishOrProcessing(new List<long>() { treatmentId });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistsReqFinishOrProcessing(List<long> treatmentIds)
        {
            bool valid = true;
            try
            {
                string sql = DAOWorker.SqlDAO.AddInClause(treatmentIds, "SELECT DISTINCT(TDL_TREATMENT_CODE) FROM HIS_SERVICE_REQ WHERE (IS_DELETE IS NULL OR IS_DELETE <> 1) AND (IS_NO_EXECUTE IS NULL OR IS_NO_EXECUTE <> 1) AND SERVICE_REQ_STT_ID <> 1 AND %IN_CLAUSE%", "TREATMENT_ID");

                List<string> isFinishOrProcessing = DAOWorker.SqlDAO.GetSql<string>(sql);
                if (IsNotNullOrEmpty(isFinishOrProcessing))
                {
                    string codes = String.Join(",", isFinishOrProcessing);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDieuTriTonTaiYLenhODaXuLy, codes);
                    return false;
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
