using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.OtherHein;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
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

        internal HisSereServSetInfo(CommonParam paramUpdate, List<HIS_PATIENT_TYPE_ALTER> data)
            : base(paramUpdate)
        {
            this.hisPatientTypeAlters = data;
        }

        internal bool AddInfo(HIS_SERE_SERV data)
        {
            bool result = true;
            try
            {
                HIS_PATIENT_TYPE_ALTER pta = null;
                HisSereServCheck checker = new HisSereServCheck(param);
                bool valid = true;
                valid = valid && checker.HasAppliedPatientTypeAlter(data.TDL_INTRUCTION_TIME, this.hisPatientTypeAlters, ref pta);
                valid = valid && checker.IsAllowUsingPatientType(pta, data.SERVICE_ID, data.PATIENT_TYPE_ID, data.TDL_INTRUCTION_TIME);
                if (valid)
                {
                    this.SetBhytInfo(data, pta);
                    this.SetKskInfo(data, pta);
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

        /// <summary>
        /// Bo sung thong tin kham suc khoe
        /// </summary>
        /// <param name="data"></param>
        /// <param name="usingPatientTypeAlter"></param>
        /// <param name="hisService"></param>
        private void SetKskInfo(HIS_SERE_SERV data, HIS_PATIENT_TYPE_ALTER usingPatientTypeAlter)
        {
            //if (data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK)
            //{
            //    //Lay thong tin dien doi tuong KSK
            //    HIS_PATY_ALTER_KSK patyAlterKsk = this.hisPatyAlterKsks != null ? this.hisPatyAlterKsks.Where(o => o.PATIENT_TYPE_ALTER_ID == usingPatientTypeAlter.ID).FirstOrDefault() : null;
            //    if (patyAlterKsk == null)
            //    {
            //        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_KhongTonTaiThongTinDienDoiTuongKskNenKhongChoPhepThanhToanSuDungKsk);
            //        throw new Exception("Khong ton tai thong tin HIS_PATY_ALTER_KSK, vi vay khong cho phep thanh toan su dung KSK doi voi dich vu");
            //    }
            //    Mapper.CreateMap<HIS_PATY_ALTER_KSK, KskPatientTypeData>();
            //    KskPatientTypeData kskPatientTypeData = Mapper.Map<KskPatientTypeData>(patyAlterKsk);

            //    data.JSON_PATIENT_TYPE_ALTER = kskPatientTypeData.ToJsonString();
            //}
        }
    }
}
