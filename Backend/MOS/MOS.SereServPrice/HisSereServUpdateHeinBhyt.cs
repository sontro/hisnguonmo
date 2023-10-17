using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Common;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServUpdateHein : BusinessBase
    {
        /// <summary>
        /// Cap nhat thong tin cho cac sere_serv su dung BHYT
        /// </summary>
        /// <param name="sereServs"></param>
        /// <param name="treatmentTypeCode"></param>
        private void UpdateBhyt(List<HIS_SERE_SERV> sereServs, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER patientTypeAlter)
        {
            string treatmentTypeCode = null;
            if (patientTypeAlter != null)
            {
                HIS_TREATMENT_TYPE t = HisTreatmentTypeCFG.DATA
                    .Where(o => o.ID == patientTypeAlter.TREATMENT_TYPE_ID).FirstOrDefault();
                treatmentTypeCode = t != null ? t.HEIN_TREATMENT_TYPE_CODE : null;
            }

            //Ko xu ly voi dich vu hao phi hoac khac BHYT
            List<HIS_SERE_SERV> heinSereServs = sereServs
                .Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.IS_EXPEND != MOS.UTILITY.Constant.IS_TRUE)
                .ToList();
            if (IsNotNullOrEmpty(heinSereServs))
            {
                //Set lai don gia cua dich vu theo chinh sach cua BYT
                this.setPriceWithBhytPolicy.Update(heinSereServs, treatment, treatmentTypeCode);

                //Tinh lai ti le BHYT chi tra
                if (!this.UpdateRatioBhyt(treatmentTypeCode, heinSereServs))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        /// <summary>
        /// Cap nhat thong tin ti le BHYT
        /// </summary>
        /// <returns></returns>
        private bool UpdateRatioBhyt(string treatmentTypeCode, List<HIS_SERE_SERV> hisSereServs)
        {
            bool result = true;
            try
            {
                List<BhytServiceRequestData> heinServiceData = new List<BhytServiceRequestData>();
                foreach (HIS_SERE_SERV data in hisSereServs)
                {
                    //chi lay vat tu thay the cho vao goi de tinh
                    long? parentId = data.TDL_HEIN_SERVICE_TYPE_ID.HasValue && (data.TDL_HEIN_SERVICE_TYPE_ID.Value == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TT || data.TDL_HEIN_SERVICE_TYPE_ID.Value == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TL) ? data.PARENT_ID : null;
                    string ratioType = BhytRatioTypeMapConfig.GetRatioType(data.TDL_HEIN_SERVICE_TYPE_ID);
                    bool isStent = HisMaterialTypeCFG.IsStentByServiceId(data.SERVICE_ID);
                    decimal price = data.PRICE * (data.VAT_RATIO + 1);
                    BhytServiceRequestData t = new BhytServiceRequestData(data.ID, parentId, price, data.HEIN_LIMIT_PRICE, data.AMOUNT, data.JSON_PATIENT_TYPE_ALTER, ratioType, isStent, data.STENT_ORDER, data.ORIGINAL_PRICE, data.TDL_INTRUCTION_TIME);
                    heinServiceData.Add(t);
                }

                if (!new BhytHeinProcessor().UpdateHeinInfo(treatmentTypeCode, heinServiceData))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin BHYT cho cac Sere_Serv that bai. Du lieu se bi rollback." + LogUtil.TraceData("heinServiceData", heinServiceData));
                }

                foreach (RequestServiceData heinService in heinServiceData)
                {
                    HIS_SERE_SERV hisSereServ = hisSereServs.Where(o => o.ID == heinService.Id).SingleOrDefault();
                    hisSereServ.HEIN_RATIO = heinService.HeinRatio;
                    hisSereServ.HEIN_PRICE = heinService.HeinPrice;
                    hisSereServ.PRICE = heinService.Price / (hisSereServ.VAT_RATIO + 1);
                    hisSereServ.HEIN_LIMIT_PRICE = heinService.LimitPrice;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
