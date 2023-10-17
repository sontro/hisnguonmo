using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Register
{
    class HisVaccinationExamRegisterCheck : BusinessBase
    {
        internal HisVaccinationExamRegisterCheck()
            : base()
        {

        }

        internal HisVaccinationExamRegisterCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidServicePaty(long requestTime, HisPatientVaccinationSDO data, WorkPlaceSDO workPlace)
        {
            try
            {
                if (IsNotNullOrEmpty(data.VaccinationMeties))
                {
                    Dictionary<VaccinationMetySDO, V_HIS_SERVICE_PATY> dic = new Dictionary<VaccinationMetySDO, V_HIS_SERVICE_PATY>();
                    foreach (VaccinationMetySDO sdo in data.VaccinationMeties)
                    {
                        V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == sdo.MediStockId).FirstOrDefault();
                        HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA != null ? HisMedicineTypeCFG.DATA.Where(o => o.ID == sdo.MedicineTypeId).FirstOrDefault() : null;

                        if (mediStock == null || medicineType == null)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            LogSystem.Warn("MediStockId hoac medicine_type_id ko hop le");
                            return false;
                        }

                        V_HIS_SERVICE_PATY servicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, workPlace.BranchId, mediStock.ROOM_ID, workPlace.RoomId, workPlace.DepartmentId, requestTime, requestTime, medicineType.SERVICE_ID, sdo.PatientTypeId, null);

                        if (servicePaty == null)
                        {
                            HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == sdo.PatientTypeId).FirstOrDefault();
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, medicineType.MEDICINE_TYPE_NAME, medicineType.MEDICINE_TYPE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }


        internal bool IsAllowMediStock(HisPatientVaccinationSDO data)
        {
            //Kiem tra cac kho su dung co hop le khong
            List<long> mediStockIds = IsNotNullOrEmpty(data.VaccinationMeties) ? data.VaccinationMeties.Select(o => o.MediStockId).ToList() : null;

            if (IsNotNullOrEmpty(mediStockIds))
            {
                //Kiem tra xem co kho khong nam trong d/s cac kho duoc cau hinh cho phep xuat tu phong dang lam viec hay khong
                List<long> forbiddenStocks = mediStockIds.Where(o => HisMestRoomCFG.DATA == null || !HisMestRoomCFG.DATA.Exists(t => t.MEDI_STOCK_ID == o && t.ROOM_ID == data.HisVaccinationExam.EXECUTE_ROOM_ID && t.IS_ACTIVE == Constant.IS_TRUE)).ToList();

                if (IsNotNullOrEmpty(forbiddenStocks))
                {
                    List<string> mediStockNames = HisMediStockCFG.DATA.Where(o => forbiddenStocks.Contains(o.ID)).Select(o => o.MEDI_STOCK_NAME).ToList();
                    string mediStockNameStr = string.Join(",", mediStockNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacKhoSauKhongChoPhepXuatDenPhongDangLamViec, mediStockNameStr);
                    return false;
                }

                //Kiem tra trong cac kho ke thuoc co kho nao bi khoa hay khong
                List<string> lockMediStockNames = HisMediStockCFG.DATA
                    .Where(o => mediStockIds.Contains(o.ID) && o.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    .Select(o => o.MEDI_STOCK_NAME)
                    .ToList();

                if (IsNotNullOrEmpty(lockMediStockNames))
                {
                    string mediStockNameStr = string.Join(",", lockMediStockNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa, mediStockNameStr);
                    return false;
                }
            }
            return true;
        }
    }
}
