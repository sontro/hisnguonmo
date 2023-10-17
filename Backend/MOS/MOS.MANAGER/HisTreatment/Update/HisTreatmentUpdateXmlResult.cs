using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update
{
    class HisTreatmentUpdateXmlResult : BusinessBase
    {
        internal HisTreatmentUpdateXmlResult(CommonParam param)
            : base(param)
        {

        }

        internal bool UpdateXmlResult(HisTreatmentXmlResultSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && this.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && this.IsCreateFileSuccess(treatment);
                if (valid)
                {
                    string desc = !String.IsNullOrWhiteSpace(data.Description) ? data.Description : treatment.XML4210_DESC;
                    string sql = String.Format("UPDATE HIS_TREATMENT SET XML4210_RESULT = {0}, XML4210_DESC = '{1}' WHERE ID = {2}", data.XmlResult, (desc ?? ""), treatment.ID);
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        throw new Exception("Update XML4210_RESULT cho HIS_TREATMENT that bai. SQL: " + sql);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal bool UpdateCollinearXmlResult(HisTreatmentXmlResultSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && this.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && this.IsCreateFileSuccessCollinear(treatment);
                if (valid)
                {
                    string desc = !String.IsNullOrWhiteSpace(data.Description) ? data.Description : treatment.COLLINEAR_XML4210_DESC;
                    string sql = String.Format("UPDATE HIS_TREATMENT SET COLLINEAR_XML4210_RESULT = {0}, COLLINEAR_XML4210_DESC = '{1}' WHERE ID = {2}", data.XmlResult, (desc ?? ""), treatment.ID);
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        throw new Exception("Update COLLINEAR_XML4210_RESULT cho HIS_TREATMENT that bai. SQL: " + sql);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private bool VerifyRequireField(HisTreatmentXmlResultSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (data.XmlResult <= 2 || data.XmlResult >= 5) throw new ArgumentNullException("data.XmlResult <= 2 || data.XmlResult >= 5");
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

        private bool IsCreateFileSuccess(HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (treatment.XML4210_RESULT != IMSys.DbConfig.HIS_RS.HIS_TREATMENT.XML4210_RESULT__CREATE_FILE_SUCCESS)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDieuTriChuaTaoDuocFileXML);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        private bool IsCreateFileSuccessCollinear(HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (treatment.COLLINEAR_XML4210_RESULT != IMSys.DbConfig.HIS_RS.HIS_TREATMENT.COLLINEAR_XML4210_RESULT__CREATE_FILE_SUCCESS)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDieuTriChuaTaoDuocFileXML);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
