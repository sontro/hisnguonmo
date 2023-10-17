using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.OtherHein;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServicePaty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ
{
    /// <summary>
    /// Cac thong tin ve dich vu (vd: thong tin bao hiem, dien doi tuong,... su dung) duoc set moi khi duyet
    /// </summary>
    class HisSereServSetInfo : BusinessBase
    {
        #region Cac lop xu ly ngoai
        private HisPatientTypeAlterGet hisPatientTypeAlterGet = new HisPatientTypeAlterGet();
        private HisPatientTypeGet hisPatientTypeGet = new HisPatientTypeGet();
        private HisPatientTypeAllowGet hisPatientTypeAllowGet = new HisPatientTypeAllowGet();
        #endregion

        //Luu cac du lieu nham phuc vu xu ly ma ko phai truy van lai CSDL
        private List<HIS_PATIENT_TYPE_ALTER> hisPatientTypeAlters;
        private List<HIS_KSK_CONTRACT> kskContracts;

        internal HisSereServSetInfo(CommonParam paramUpdate, List<HIS_PATIENT_TYPE_ALTER> data)
            : base(paramUpdate)
        {
            this.hisPatientTypeAlters = data;
            this.kskContracts = this.GetKskContract(data);
        }

        private List<HIS_KSK_CONTRACT> GetKskContract(List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            try
            {
                if (IsNotNullOrEmpty(ptas))
                {
                    List<HIS_PATIENT_TYPE_ALTER> kskPtas = ptas
                        .Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK).ToList();
                    List<long> contractIds = kskPtas.Select(o => o.KSK_CONTRACT_ID.Value).ToList();

                    return new HisKskContractGet().GetByIds(contractIds);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }

        internal bool AddInfo(HIS_SERE_SERV data)
        {
            HIS_PATIENT_TYPE_ALTER pta = null;
            return AddInfo(data, ref pta);
        }

        internal bool AddInfo(HIS_SERE_SERV data, ref HIS_PATIENT_TYPE_ALTER pta)
        {
            bool result = true;
            try
            {
                HisSereServCheck checker = new HisSereServCheck(param);
                bool valid = true;
                valid = valid && checker.HasAppliedPatientTypeAlter(data.TDL_INTRUCTION_TIME, this.hisPatientTypeAlters, ref pta);
                valid = valid && checker.IsAllowUsingPatientType(this.kskContracts, pta, data.SERVICE_ID, data.PATIENT_TYPE_ID, data.TDL_INTRUCTION_TIME);
                if (valid)
                {
                    this.SetBhytInfo(data, pta);
                    this.SetKskInfo(data, pta);
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void SetKskInfo(HIS_SERE_SERV data, HIS_PATIENT_TYPE_ALTER pta)
        {
            if (pta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK)
            {
                HIS_KSK_CONTRACT contract = this.kskContracts != null && pta.KSK_CONTRACT_ID.HasValue ? this.kskContracts.Where(o => o.ID == pta.KSK_CONTRACT_ID.Value).FirstOrDefault() : null;

                if (contract == null)
                {
                    V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.SERVICE_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_KhongTonTaiThongTinDienDoiTuongKskNenKhongChoPhepThanhToanSuDungKsk, service.SERVICE_NAME);
                    return;
                }

                data.HEIN_CARD_NUMBER = null;
                data.JSON_PATIENT_TYPE_ALTER = null;
                data.HEIN_RATIO = contract.PAYMENT_RATIO;
                data.HEIN_PRICE = null;
            }
        }

        /// <summary>
        /// Bo sung thong tin BHYT
        /// </summary>
        /// <param name="data"></param>
        /// <param name="usingPatientTypeAlter"></param>
        /// <param name="hisService"></param>
        private void SetBhytInfo(HIS_SERE_SERV data, HIS_PATIENT_TYPE_ALTER pta)
        {
            if (data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
            {
                data.JSON_PATIENT_TYPE_ALTER = BhytPatientTypeData.ToJsonString(pta);
                data.HEIN_CARD_NUMBER = pta.HEIN_CARD_NUMBER;
            }
        }
    }
}
