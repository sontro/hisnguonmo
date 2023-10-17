using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient
{
    class HisServiceReqOutPatientPresUtil: BusinessBase
    {
        public void SplitByGroup(OutPatientPresSDO data, ref List<PresMedicineSDO> huongThan, ref List<PresMedicineSDO> gayNghien, ref List<PresMedicineSDO> thuocThuong, ref List<PresMedicineSDO> thucPham, ref List<PresOutStockMetySDO> huongThan2, ref List<PresOutStockMetySDO> gayNghien2, ref List<PresOutStockMetySDO> thucPham2, ref List<PresOutStockMetySDO> thuocThuong2)
        {
            try
            {
                ///Thuoc vat tu ke trong kho
                huongThan = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => HisMedicineTypeCFG.HUONG_THAN_IDs != null && HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId)).ToList() : null;

                gayNghien = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => HisMedicineTypeCFG.GAY_NGHIEN_IDs != null && HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId)).ToList() : null;

                thuocThuong = IsNotNullOrEmpty(data.Medicines) ? data.Medicines
                    .Where(t => (HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs == null || !HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs.Contains(t.MedicineTypeId))
                        && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                        && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                        ).ToList() : null;

                thucPham = IsNotNullOrEmpty(data.Medicines) ? data.Medicines
                    .Where(t => HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs != null && HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs.Contains(t.MedicineTypeId)
                        && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                        && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                        ).ToList() : null;

                ///Thuoc ke ngoai kho
                huongThan2 = IsNotNullOrEmpty(data.ServiceReqMeties) ?
                    data.ServiceReqMeties.Where(t => HisMedicineTypeCFG.HUONG_THAN_IDs != null
                        && t.MedicineTypeId.HasValue && HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId.Value)).ToList() : null;

                gayNghien2 = IsNotNullOrEmpty(data.ServiceReqMeties) ?
                    data.ServiceReqMeties.Where(t => HisMedicineTypeCFG.GAY_NGHIEN_IDs != null
                        && t.MedicineTypeId.HasValue && HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId.Value)).ToList() : null;

                thucPham2 = IsNotNullOrEmpty(data.ServiceReqMeties) ?
                    data.ServiceReqMeties.Where(t => HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs != null
                        && t.MedicineTypeId.HasValue && HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs.Contains(t.MedicineTypeId.Value)
                        && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId.Value))
                        && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId.Value))
                        ).ToList() : null;

                thuocThuong2 = IsNotNullOrEmpty(data.ServiceReqMeties) ?
                    data.ServiceReqMeties.Where(t => (HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs == null || !t.MedicineTypeId.HasValue || !HisMedicineTypeCFG.THUC_PHAM_CHUC_NANG_IDs.Contains(t.MedicineTypeId.Value))
                        && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !t.MedicineTypeId.HasValue || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId.Value))
                        && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !t.MedicineTypeId.HasValue || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId.Value))
                        ).ToList() : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public List<HIS_SERVICE_REQ_METY> MakeMety(List<PresMedicineSDO> medicines, List<HIS_SERVICE_REQ> inStockServiceReqs)
        {
            List<HIS_SERVICE_REQ_METY> result = null;

            //Lay cac thuoc cua "don phu" (la cac thuoc ma ko co bean)
            List<PresMedicineSDO> subPresMeties = IsNotNullOrEmpty(medicines) ? medicines.Where(o => !IsNotNullOrEmpty(o.MedicineBeanIds)).ToList() : null;

            if (IsNotNullOrEmpty(subPresMeties) && IsNotNullOrEmpty(inStockServiceReqs))
            {
                List<HIS_MEDICINE_TYPE> medicineTypes = HisMedicineTypeCFG.DATA.Where(o => subPresMeties.Exists(t => t.MedicineTypeId == o.ID)).ToList();

                result = new List<HIS_SERVICE_REQ_METY>();
                foreach (PresMedicineSDO sdo in subPresMeties)
                {
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == sdo.MediStockId).FirstOrDefault();
                    HIS_SERVICE_REQ serviceReq = inStockServiceReqs.Where(o => o.EXECUTE_ROOM_ID == mediStock.ROOM_ID).FirstOrDefault();

                    if (serviceReq != null)
                    {
                        HIS_MEDICINE_TYPE mt = medicineTypes.Where(o => o.ID == sdo.MedicineTypeId).FirstOrDefault();
                        HIS_SERVICE_UNIT su = HisServiceUnitCFG.DATA.Where(o => o.ID == mt.TDL_SERVICE_UNIT_ID).FirstOrDefault();
                        HIS_SERVICE_REQ_METY mety = new HIS_SERVICE_REQ_METY();
                        mety.AMOUNT = sdo.Amount;
                        mety.MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                        mety.MEDICINE_TYPE_NAME = mt.MEDICINE_TYPE_NAME;
                        mety.NUM_ORDER = sdo.NumOrder;
                        mety.SERVICE_REQ_ID = serviceReq.ID;
                        mety.UNIT_NAME = su.SERVICE_UNIT_NAME;
                        mety.MEDICINE_USE_FORM_ID = sdo.MedicineUseFormId;
                        mety.SPEED = sdo.Speed;
                        mety.TUTORIAL = sdo.Tutorial;
                        mety.MORNING = sdo.Morning;
                        mety.NOON = sdo.Noon;
                        mety.AFTERNOON = sdo.Afternoon;
                        mety.EVENING = sdo.Evening;
                        mety.HTU_ID = sdo.HtuId;
                        mety.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                        if (sdo.NumOfDays.HasValue)
                        {
                            long dayCount = sdo.NumOfDays.Value == 0 ? 1 : sdo.NumOfDays.Value;

                            DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.USE_TIME > 0 ? serviceReq.USE_TIME.Value : serviceReq.INTRUCTION_TIME).Value;
                            DateTime useTimeTo = time.AddDays(dayCount - 1);
                            mety.USE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(useTimeTo);
                        }

                        mety.IS_SUB_PRES = Constant.IS_TRUE;
                        result.Add(mety);
                    }
                    
                }
            }
            return result;
        }

        public List<HIS_SERVICE_REQ_MATY> MakeMaty(List<PresMaterialSDO> materials, List<HIS_SERVICE_REQ> inStockServiceReqs)
        {
            List<HIS_SERVICE_REQ_MATY> result = null;

            //Lay cac vat tu cua "don phu" (la cac thuoc ma ko co bean)
            List<PresMaterialSDO> subPresMaties = IsNotNullOrEmpty(materials) ? materials.Where(o => !IsNotNullOrEmpty(o.MaterialBeanIds)).ToList() : null;

            if (IsNotNullOrEmpty(subPresMaties) && IsNotNullOrEmpty(inStockServiceReqs))
            {
                List<HIS_MATERIAL_TYPE> materialTypes = HisMaterialTypeCFG.DATA.Where(o => subPresMaties.Exists(t => t.MaterialTypeId == o.ID)).ToList();

                result = new List<HIS_SERVICE_REQ_MATY>();
                foreach (PresMaterialSDO sdo in subPresMaties)
                {
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == sdo.MediStockId).FirstOrDefault();
                    HIS_SERVICE_REQ serviceReq = inStockServiceReqs.Where(o => o.EXECUTE_ROOM_ID == mediStock.ROOM_ID).FirstOrDefault();

                    HIS_MATERIAL_TYPE mt = materialTypes.Where(o => o.ID == sdo.MaterialTypeId).FirstOrDefault();
                    HIS_SERVICE_UNIT su = HisServiceUnitCFG.DATA.Where(o => o.ID == mt.TDL_SERVICE_UNIT_ID).FirstOrDefault();

                    HIS_SERVICE_REQ_MATY maty = new HIS_SERVICE_REQ_MATY();
                    maty.AMOUNT = sdo.Amount;
                    maty.MATERIAL_TYPE_ID = sdo.MaterialTypeId;
                    maty.MATERIAL_TYPE_NAME = mt.MATERIAL_TYPE_NAME;
                    maty.NUM_ORDER = sdo.NumOrder;
                    maty.TUTORIAL = sdo.Tutorial;
                    maty.SERVICE_REQ_ID = serviceReq.ID;
                    maty.UNIT_NAME = su.SERVICE_UNIT_NAME;
                    maty.IS_SUB_PRES = Constant.IS_TRUE;
                    maty.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                    result.Add(maty);
                }
            }
            return result;
        }
    }

}
