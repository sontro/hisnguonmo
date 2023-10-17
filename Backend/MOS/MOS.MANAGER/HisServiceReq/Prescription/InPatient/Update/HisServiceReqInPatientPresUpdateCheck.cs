using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Update
{
    class HisServiceReqInPatientPresUpdateCheck : BusinessBase
    {
        internal HisServiceReqInPatientPresUpdateCheck()
            : base()
        {
        }

        internal HisServiceReqInPatientPresUpdateCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool IsValidData(InPatientPresSDO data, HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                //Neu co cau hinh tach thuoc dau * ra don rieng
                if (HisExpMestCFG.IS_SPLIT_STAR_MARK)
                {
                    if (serviceReq.IS_STAR_MARK == Constant.IS_TRUE)
                    {
                        List<long> nonStarMedicineIds = data.Medicines != null ?
                            data.Medicines
                            .Where(t => HisMedicineTypeCFG.STAR_IDs == null || !HisMedicineTypeCFG.STAR_IDs.Contains(t.MedicineTypeId))
                            .Select(t => t.MedicineTypeId).ToList() : null;

                        List<long> nonStarMetyIds = data.ServiceReqMeties != null ?
                            data.ServiceReqMeties
                            .Where(t => t.MedicineTypeId.HasValue)
                            .Where(t => HisMedicineTypeCFG.STAR_IDs == null || !HisMedicineTypeCFG.STAR_IDs.Contains(t.MedicineTypeId.Value))
                            .Select(t => t.MedicineTypeId.Value).ToList() : null;

                        if (IsNotNullOrEmpty(nonStarMedicineIds)
                            || IsNotNullOrEmpty(nonStarMetyIds))
                        {
                            List<string> names = HisMedicineTypeCFG.DATA.Where(o => (nonStarMedicineIds != null && nonStarMedicineIds.Contains(o.ID)) || (nonStarMetyIds != null && nonStarMetyIds.Contains(o.ID))).Select(o => o.MEDICINE_TYPE_NAME).ToList();
                            string nameStr = string.Join(",", names);

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocDauSaoKhongChoPhepKeThuocThuong, nameStr);
                            return false;
                        }

                        if (IsNotNullOrEmpty(data.Materials)||IsNotNullOrEmpty(data.ServiceReqMaties))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocDauSaoKhongChoPhepKeVatTu);
                            return false;
                        }
                    }
                    else
                    {
                        List<long> starMedicineIds = data.Medicines != null ?
                            data.Medicines
                            .Where(t => HisMedicineTypeCFG.STAR_IDs != null && HisMedicineTypeCFG.STAR_IDs.Contains(t.MedicineTypeId))
                            .Select(t => t.MedicineTypeId).ToList() : null;

                        List<long> starMetyIds = data.ServiceReqMeties != null ?
                            data.ServiceReqMeties
                            .Where(t => t.MedicineTypeId.HasValue)
                            .Where(t => HisMedicineTypeCFG.STAR_IDs != null && HisMedicineTypeCFG.STAR_IDs.Contains(t.MedicineTypeId.Value))
                            .Select(t => t.MedicineTypeId.Value).ToList() : null;
                        if (IsNotNullOrEmpty(starMedicineIds) || IsNotNullOrEmpty(starMetyIds))
                        {
                            List<string> names = HisMedicineTypeCFG.DATA.Where(o => (starMedicineIds != null && starMedicineIds.Contains(o.ID)) || (starMetyIds != null && starMetyIds.Contains(o.ID))).Select(o => o.MEDICINE_TYPE_NAME).ToList();
                            string nameStr = string.Join(",", names);

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocThuongKhongChoPhepKeThuocDauSao, nameStr);
                            return false;
                        }
                    }
                }

                if (HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK)
                {
                    if (serviceReq.SPECIAL_MEDICINE_TYPE == (long) HisExpMestCFG.SpecialMedicineType.DOC)
                    {
                        List<long> invalidIds = data.Medicines != null ?
                            data.Medicines
                            .Where(t => HisMedicineTypeCFG.THUOC_DOC_IDs == null || !HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId))
                            .Select(t => t.MedicineTypeId).ToList() : null;

                        if (IsNotNullOrEmpty(invalidIds))
                        {
                            List<string> names = HisMedicineTypeCFG.DATA.Where(o => invalidIds != null && invalidIds.Contains(o.ID)).Select(o => o.MEDICINE_TYPE_NAME).ToList();
                            string nameStr = string.Join(",", names);

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocDocKhongChoPhepKeThuocThuong, nameStr);
                            return false;
                        }
                        if (IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.SerialNumbers))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocDocKhongChoPhepKeVatTu);
                            return false;
                        }
                    }
                    else if (serviceReq.SPECIAL_MEDICINE_TYPE == (long)HisExpMestCFG.SpecialMedicineType.GN_HT)
                    {
                        List<long> invalidIds = data.Medicines != null ?
                            data.Medicines
                            .Where(t => (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                                && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                            )
                            .Select(t => t.MedicineTypeId).ToList() : null;

                        if (IsNotNullOrEmpty(invalidIds))
                        {
                            List<string> names = HisMedicineTypeCFG.DATA.Where(o => invalidIds != null && invalidIds.Contains(o.ID)).Select(o => o.MEDICINE_TYPE_NAME).ToList();
                            string nameStr = string.Join(",", names);

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocGayNghienHuongThanKhongChoPhepKeThuocThuong, nameStr);
                            return false;
                        }
                        if (IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.SerialNumbers))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocGayNghienHuongThanKhongChoPhepKeVatTu);
                            return false;
                        }
                    }
                    else
                    {
                        List<long> invalidIds = data.Medicines != null ?
                            data.Medicines
                            .Where(t => (HisMedicineTypeCFG.THUOC_DOC_IDs != null && HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId))
                                || (HisMedicineTypeCFG.GAY_NGHIEN_IDs != null && HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                                || (HisMedicineTypeCFG.HUONG_THAN_IDs != null && HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                            )
                            .Select(t => t.MedicineTypeId).ToList() : null;

                        if (IsNotNullOrEmpty(invalidIds))
                        {
                            List<string> names = HisMedicineTypeCFG.DATA.Where(o => invalidIds != null && invalidIds.Contains(o.ID)).Select(o => o.MEDICINE_TYPE_NAME).ToList();
                            string nameStr = string.Join(",", names);

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocThuongKhongChoPhepKeThuocGayNghienHuongThanThuocDoc, nameStr);
                            return false;
                        }
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }
    }
}
