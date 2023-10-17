using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService.TruncateReq
{
    class ServiceReqTruncateCheck : BusinessBase
    {
        internal ServiceReqTruncateCheck()
            : base()
        {

        }

        internal ServiceReqTruncateCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool Verify(List<HIS_SERVICE_REQ> lstReqTruncate, List<HIS_SERE_SERV> lstSereServTruncate, HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(lstReqTruncate))
                {
                    HisServiceReqCheck checker = new HisServiceReqCheck(param);
                    valid = valid && checker.IsUnLock(lstReqTruncate);
                    valid = valid && this.IsAllow(lstReqTruncate);
                    valid = valid && this.IsAllowStatusForDelete(lstReqTruncate);
                    valid = valid && this.IsNotAprovedSurgeryRemuneration(lstReqTruncate);
                    valid = valid && this.VerifySereServ(lstSereServTruncate);
                    valid = valid && checker.HasNoTempBed(lstSereServTruncate);
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

        internal bool IsAllow(List<HIS_SERVICE_REQ> lstReqTruncate)
        {
            bool valid = true;
            try
            {
                List<string> hasRations = lstReqTruncate.Where(o => o.RATION_SUM_ID.HasValue).Select(s => s.SERVICE_REQ_CODE).ToList();
                if (IsNotNullOrEmpty(hasRations))
                {
                    string codes = String.Join(",", hasRations);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacChiDinhSuatAnDaDuocDuyet, codes);
                    return false;
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

        internal bool IsAllowStatusForDelete(List<HIS_SERVICE_REQ> lstReqTruncate)
        {
            bool valid = true;
            try
            {
                //Neu voi cac chi dinh ko phai la don thuoc thi ko cho phep xoa neu don thuoc da bat dau
                List<string> isNotAllows = lstReqTruncate.Where(o => !HisServiceReqTypeCFG.PRESCRIPTION_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID) && o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL).Select(s => s.SERVICE_REQ_CODE).ToList();
                if (IsNotNullOrEmpty(isNotAllows))
                {
                    string codes = String.Join(",", isNotAllows);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacChiDinhDaDuocXuLy, codes);
                    return false;
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

        internal bool VerifySereServ(List<HIS_SERE_SERV> lstSereServTruncate)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(lstSereServTruncate))
                {
                    HisSereServCheck sereServCheck = new HisSereServCheck(param);
                    valid = valid && sereServCheck.IsUnLock(lstSereServTruncate);
                    valid = valid && sereServCheck.HasNoInvoice(lstSereServTruncate);//Chi cho phep xoa doi voi cac sere_serv chua co invoice
                    valid = valid && sereServCheck.HasNoBill(lstSereServTruncate);//Chi cho phep xoa doi voi cac sere_serv chua co invoice
                    valid = valid && sereServCheck.HasNoHeinApproval(lstSereServTruncate); //da duyet ho so Bao hiem thi ko cho phep sua
                    valid = valid && sereServCheck.HasNoDeposit(lstSereServTruncate.Select(s => s.ID).ToList(), false);
                    valid = valid && sereServCheck.HasNoDebt(lstSereServTruncate.Select(s => s.ID).ToList());
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotAprovedSurgeryRemuneration(List<HIS_SERVICE_REQ> lstReqTruncate)
        {
            try
            {
                //Chi check voi cac loai cdha, ns, sieu am, pt, tt, gpbl để đảm bảo hiệu năng
                //(Thuc ra ban chat la cac dv được khai báo dv BHYT là PTTT, nhưng do đặc thù, viện sẽ khai báo loại dv là ns, sa, ..., nhưng loại dv BHYT là PTTT)
                List<HIS_SERVICE_REQ> needChecks = lstReqTruncate != null ? lstReqTruncate.Where(o => (o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL
                    || o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA
                    || o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN
                    || o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS
                    || o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA
                    || o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT
                    || o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT)).ToList() : null;

                if (IsNotNullOrEmpty(needChecks))
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(needChecks.Select(s => s.ID).ToList(), "SELECT EXT.TDL_SERVICE_REQ_ID FROM HIS_SERE_SERV_EXT EXT WHERE (EXT.IS_FEE = 1 OR EXT.IS_GATHER_DATA = 1) AND %IN_CLAUSE%", "EXT.TDL_SERVICE_REQ_ID");//
                    List<long> hasApproves = DAOWorker.SqlDAO.GetSql<long>(sql);
                    if (IsNotNullOrEmpty(hasApproves))
                    {
                        string codes = String.Join(",", needChecks.Where(o => hasApproves.Contains(o.ID)).Select(s => s.SERVICE_REQ_CODE).ToList());
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_SoLieuCongThucHienPtttDaDuocChot, codes);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return false;
        }

    }
}
