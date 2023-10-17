using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMediRecord;
using MOS.MANAGER.HisMrCheckSummary;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.MediRecord
{
    class HisTreatmentStoreCheck : BusinessBase
    {
        internal HisTreatmentStoreCheck()
            : base()
        {

        }

        internal HisTreatmentStoreCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyData(HisTreatmentStoreSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.TreatmentIds)) new ArgumentNullException("data.TreatmentIds");
                if (data.DataStoreId <= 0) new ArgumentNullException("data.DataStoreId");
                if (data.StoreTime <= 0) new ArgumentNullException("data.StoreTime");
                if (!data.IsOutPatient.HasValue) new ArgumentNullException("data.IsOutPatient");
                if (data.IsOutPatient.Value && !data.ProgramId.HasValue) new ArgumentNullException("data.IsOutPatient && data.ProgramId");
                if (IsNotNullOrEmpty(data.StoreCode) && IsNotNullOrEmpty(data.TreatmentIds) && data.TreatmentIds.Count > 1)
                {
                    new ArgumentException("data.TreatmentIds, data.StoreCode: chi cho phep nhap ma luu tru khi luu tung ho so");
                }
            }
            catch (ArgumentNullException ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                param.HasException = true;
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool VerifyIsOutPatient(HisTreatmentStoreSDO data, List<HIS_TREATMENT> treatments)
        {
            bool valid = true;
            try
            {
                if (data.IsOutPatient.HasValue && data.IsOutPatient.Value)
                {
                    List<string> notIns = treatments != null ? treatments.Where(o => o.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU).Select(s => s.TREATMENT_CODE).ToList() : null;
                    if (IsNotNullOrEmpty(notIns))
                    {
                        string codes = String.Join(",", notIns);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDienNoiTruKhongChoPhepLuBenhAnNgoaiTru, codes);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistStoreCode(HisTreatmentStoreSDO data, List<HIS_TREATMENT> treatments)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(data.StoreCode))
                {
                    HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
                    filter.STORE_CODE = data.StoreCode;
                    List<HIS_MEDI_RECORD> mediRecords = new HisMediRecordGet().Get(filter);

                    List<HIS_MEDI_RECORD> exists = treatments != null && mediRecords != null ? mediRecords.Where(o => !treatments.Exists(t => t.MEDI_RECORD_ID == o.ID)).ToList() : null;

                    //Neu ton tai ho so co ma day ma khong tuong ung voi ho so dieu tri hien tai thi bao loi
                    if (IsNotNullOrEmpty(exists))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_MaLuuTruDaTonTai, data.StoreCode);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsTreatmentDidNotPassTheChecklist(List<HIS_TREATMENT> treatments)
        {
            try
            {
                if (HisTreatmentCFG.IS_REQUIRED_MR_SUMMARY_DETAIL_WHEN_STORE)
                {
                    if (IsNotNullOrEmpty(treatments))
                    {
                        List<long> treatmentIds = treatments.Select(o => o.ID).ToList();//A
                        HisMrCheckSummaryFilterQuery filter = new HisMrCheckSummaryFilterQuery();
                        filter.TREATMENT_IDs = treatmentIds;
                        List<HIS_MR_CHECK_SUMMARY> mrCheckSummarys = new HisMrCheckSummaryGet().Get(filter);//B

                        List<HIS_TREATMENT> lstTreatmentError = new List<HIS_TREATMENT>();

                        foreach (var treat in treatments)
                        {
                            List<HIS_MR_CHECK_SUMMARY> LstMrCheckSummary = IsNotNullOrEmpty(mrCheckSummarys) ? mrCheckSummarys.Where(o => o.TREATMENT_ID == treat.ID).ToList() : null; //C
                            List<HIS_MR_CHECK_SUMMARY> mrCheckSummaryIsApproveds = IsNotNullOrEmpty(LstMrCheckSummary) ? LstMrCheckSummary.Where(o => o.IS_APPROVED != Constant.IS_TRUE).ToList() : null;//D
                            if (!IsNotNullOrEmpty(LstMrCheckSummary) || IsNotNullOrEmpty(mrCheckSummaryIsApproveds))
                            {
                                lstTreatmentError.Add(treat);
                            }
                        }
                        if (IsNotNullOrEmpty(lstTreatmentError))
                        {
                            List<string> treatCodes = lstTreatmentError.Select(o => o.TREATMENT_CODE).Distinct().ToList();
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDTChuaDatBangKiem, string.Join(", ", treatCodes));
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }
    }
}
