using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient
{
    class HisServiceReqOutPatientPresCheck : BusinessBase
    {
        internal HisServiceReqOutPatientPresCheck()
            : base()
        {
        }

        internal HisServiceReqOutPatientPresCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool IsValidData(OutPatientPresSDO data, ref HIS_SERVICE_REQ parentServiceReq)
        {
            bool valid = true;
            try
            {
                //Neu cau hinh tu dong tao phieu xuat ban khi ke thuoc ngoai kho
                if (HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST && (IsNotNullOrEmpty(data.ServiceReqMeties) || IsNotNullOrEmpty(data.ServiceReqMaties)))
                {
                    if (HisServiceReqCFG.DEFAULT_DRUG_STORE_PATIENT_TYPE_ID <= 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChuaThietLapDoiTuongThanhToanMacDinhCuaNhaThuoc);
                        return false;
                    }

                    if (!data.DrugStoreId.HasValue)
                    {

                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChuaChonNhaThuoc);
                        return false;
                    }

                    V_HIS_MEDI_STOCK drugStore = HisMediStockCFG.DATA.Where(o => o.ID == data.DrugStoreId.Value).FirstOrDefault();
                    if (drugStore == null || drugStore.IS_BUSINESS != Constant.IS_TRUE)
                    {
                        string name = drugStore != null ? drugStore.MEDI_STOCK_NAME : "";
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiLaNhaThuoc, name);
                        return false;
                    }
                }

                if (data.InstructionTime <= 0)
                {
                    LogSystem.Warn("data.InstructionTime");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                };
                if (string.IsNullOrWhiteSpace(data.ClientSessionKey))
                {
                    LogSystem.Warn("data.ClientSessionKey");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                };
                if (IsNotNullOrEmpty(data.Medicines))
                {
                    long count = data.Medicines.Select(t => new
                    {
                        t.IsExpend,
                        t.IsOutParentFee,
                        t.MedicineTypeId,
                        t.MediStockId,
                        t.PatientTypeId,
                        t.SereServParentId,
                        t.MedicineId,
                        t.Tutorial,
                        t.MixedInfusion
                    }).Distinct().Count();

                    if (count != data.Medicines.Count)
                    {
                        LogSystem.Debug("data.Medicines chua 2 dong co thong tin IsExpend, IsOutParentFee, MedicineTypeId, MediStockId, PatientTypeId, SereServParentId, MedicineId, t.Tutorial giong nhau");
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        return false;
                    }

                    if (!HisServiceReqCFG.IS_USING_SUB_PRESCRIPTION_MECHANISM && data.Medicines.Exists(t => t.MedicineBeanIds == null || t.MedicineBeanIds.Count == 0))
                    {
                        LogSystem.Debug("Khong bat cau hinh su dung co che ke don phu (MOS.HIS_SERVICE_REQ.IS_USING_SUB_PRESCRIPTION_MECHANISM) va Ton tai MedicineBeanIds null");
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        return false;
                    }
                }

                if (IsNotNullOrEmpty(data.Materials))
                {
                    //ko check duplicate voi stent (vi stent neu ke so luong lon hon 1 thi se tach 
                    //thanh nhieu dong co so luong ko vuot qua 1 va server tu dong set stent_order)
                    var tmp = data.Materials.Where(o => !HisMaterialTypeCFG.IsStent(o.MaterialTypeId)).ToList();
                    if (IsNotNullOrEmpty(tmp))
                    {
                        long count = tmp.Select(t => new
                        {
                            t.IsExpend,
                            t.IsOutParentFee,
                            t.MaterialTypeId,
                            t.MediStockId,
                            t.PatientTypeId,
                            t.SereServParentId,
                            t.MaterialId
                        }).Distinct().Count();

                        if (count != tmp.Count)
                        {
                            LogSystem.Warn("data.Materials chua 2 dong co thong tin IsExpend, IsOutParentFee, MaterialTypeId, MediStockId, PatientTypeId, SereServParentId giong nhau");
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            return false;
                        }
                    }

                    if (!HisServiceReqCFG.IS_USING_SUB_PRESCRIPTION_MECHANISM && data.Materials.Exists(t => t.MaterialBeanIds == null || t.MaterialBeanIds.Count == 0))
                    {
                        LogSystem.Warn("Khong bat cau hinh su dung co che ke don phu (MOS.HIS_SERVICE_REQ.IS_USING_SUB_PRESCRIPTION_MECHANISM) va Ton tai MaterialBeanIds null");
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        return false;
                    }

                    if (data.RemedyCount.HasValue && data.PrescriptionTypeId == PrescriptionType.NEW)
                    {
                        LogSystem.Warn("Don tan duoc ko duoc phep nhap truong 'so thang'");
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        return false;
                    }
                    if (!data.RemedyCount.HasValue && data.PrescriptionTypeId == PrescriptionType.TRADITIONAL)
                    {
                        LogSystem.Warn("Don YHCT bat buoc phai nhap truong 'so thang'");
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        return false;
                    }
                }

                //Khong cho phep ke ca don phu va don chinh trong cung 1 lan ke don
                List<PresMaterialSDO> materials = IsNotNullOrEmpty(data.Materials) ? data.Materials.Where(o => IsNotNullOrEmpty(o.MaterialBeanIds)).ToList() : null;
                List<PresMaterialSDO> subPresMaterials = IsNotNullOrEmpty(data.Materials) ? data.Materials.Where(o => !IsNotNullOrEmpty(o.MaterialBeanIds)).ToList() : null;
                List<PresMedicineSDO> medicines = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(o => IsNotNullOrEmpty(o.MedicineBeanIds)).ToList() : null;
                List<PresMedicineSDO> subPresMedicines = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(o => !IsNotNullOrEmpty(o.MedicineBeanIds)).ToList() : null;

                if ((IsNotNullOrEmpty(materials) || IsNotNullOrEmpty(medicines))
                    && (IsNotNullOrEmpty(subPresMaterials) || IsNotNullOrEmpty(subPresMedicines)))
                {
                    LogSystem.Warn("Ton tai data.Materials/data.Medicines co MaterialBeanIds/MedicineBeanIds va ca ko co MaterialBeanIds/MedicineBeanIds");
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiCaDonPhuLanDonChinh);
                    return false;
                }

                if (data.ParentServiceReqId.HasValue)
                {
                    parentServiceReq = new HisServiceReqGet().GetById(data.ParentServiceReqId.Value);
                    if (parentServiceReq.IS_MAIN_EXAM == Constant.IS_TRUE && (IsNotNullOrEmpty(subPresMaterials) || IsNotNullOrEmpty(subPresMedicines)))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepKeDonPhuKhiXuTriKhamChinh);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidStentAmount(PrescriptionSDO data)
        {
            try
            {
                List<long> invalidStents = IsNotNullOrEmpty(data.Materials) ? data.Materials.Where(o => HisMaterialTypeCFG.IsStent(o.MaterialTypeId) && o.Amount > 1).Select(o => o.MaterialTypeId).ToList() : null;
                if (IsNotNullOrEmpty(invalidStents))
                {
                    List<string> materialTypeNames = HisMaterialTypeCFG.DATA.Where(o => invalidStents.Contains(o.ID)).Select(o => o.MATERIAL_TYPE_NAME).ToList();
                    string materialTypeNameStr = string.Join(",", materialTypeNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_StentVoiSoLuongLonHon1CanTachThanhNhieuDong, materialTypeNameStr);
                    return false;
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

        internal bool IsValidGroup(OutPatientPresSDO data, HIS_SERVICE_REQ old)
        {
            bool result = false;
            try
            {
                if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT1
                    || HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT2)
                {
                    List<PresMedicineSDO> huongThan = null;
                    List<PresMedicineSDO> gayNghien = null;
                    List<PresMedicineSDO> thuocThuong = null;
                    List<PresMedicineSDO> thucPham = null;
                    List<PresOutStockMetySDO> huongThan2 = null;
                    List<PresOutStockMetySDO> gayNghien2 = null;
                    List<PresOutStockMetySDO> thucPham2 = null;
                    List<PresOutStockMetySDO> thuocThuong2 = null;

                    new HisServiceReqOutPatientPresUtil().SplitByGroup(data, ref huongThan, ref gayNghien, ref thuocThuong, ref thucPham, ref huongThan2, ref gayNghien2, ref thucPham2, ref thuocThuong2);

                    
                    if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.HUONG_THAN
                    && (IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thuocThuong) || IsNotNullOrEmpty(thucPham)
                    || IsNotNullOrEmpty(gayNghien2) || IsNotNullOrEmpty(thuocThuong2) || IsNotNullOrEmpty(thucPham2)
                    || IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.ServiceReqMaties)))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonHuongThanKhongChoPhepBoSungLoaiKhac);
                        return false;
                    }

                    if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.GAY_NGHIEN
                        && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(thuocThuong) || IsNotNullOrEmpty(thucPham)
                        || IsNotNullOrEmpty(huongThan2) || IsNotNullOrEmpty(thuocThuong2) || IsNotNullOrEmpty(thucPham2)
                        || IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.ServiceReqMaties)))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonGayNghienKhongChoPhepBoSungLoaiKhac);
                        return false;
                    }

                    if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT1)
                    {
                        if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.THUONG
                            && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thucPham)
                            || IsNotNullOrEmpty(huongThan2) || IsNotNullOrEmpty(gayNghien2) || IsNotNullOrEmpty(thucPham2)
                            || IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.ServiceReqMaties)))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocThuongKhongChoPhepBoSungLoaiKhac);
                            return false;
                        }

                        if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.HO_TRO
                            && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thuocThuong)
                            || IsNotNullOrEmpty(huongThan2) || IsNotNullOrEmpty(gayNghien2) || IsNotNullOrEmpty(thuocThuong2)))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonHoTroKhongChoPhepBoSungLoaiKhac);
                            return false;
                        }
                    }
                    else if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT2)
                    {
                        if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.THUONG
                            && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thucPham)
                            || IsNotNullOrEmpty(huongThan2) || IsNotNullOrEmpty(gayNghien2) || IsNotNullOrEmpty(thucPham2)))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonThuocThuongKhongChoPhepBoSungLoaiKhac);
                            return false;
                        }

                        if (old.PRES_GROUP == (long)HisServiceReqCFG.PresGroup.HO_TRO
                            && (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(thuocThuong)
                            || IsNotNullOrEmpty(huongThan2) || IsNotNullOrEmpty(gayNghien2) || IsNotNullOrEmpty(thuocThuong2)
                            || IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.ServiceReqMaties)))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonHoTroKhongChoPhepBoSungLoaiKhac);
                            return false;
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }

            return result;
        }

        internal bool IsValidSpecialMedicineType(PrescriptionSDO data, HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                if (HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK)
                {
                    if (serviceReq.SPECIAL_MEDICINE_TYPE == (long)HisExpMestCFG.SpecialMedicineType.DOC)
                    {
                        List<long> invalidMedicineIds = data.Medicines != null ?
                            data.Medicines
                            .Where(t => HisMedicineTypeCFG.THUOC_DOC_IDs == null || !HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId))
                            .Select(t => t.MedicineTypeId).ToList() : null;

                        List<long> invalidReqMetyIds = data.ServiceReqMeties != null ?
                            data.ServiceReqMeties
                            .Where(t => t.MedicineTypeId.HasValue && (HisMedicineTypeCFG.THUOC_DOC_IDs == null || !HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId.Value)))
                            .Select(t => t.MedicineTypeId.Value).ToList() : null;

                        List<long> invalidIds = new List<long>();
                        if (IsNotNullOrEmpty(invalidMedicineIds))
                        {
                            invalidIds.AddRange(invalidMedicineIds);
                        }
                        if (IsNotNullOrEmpty(invalidReqMetyIds))
                        {
                            invalidIds.AddRange(invalidReqMetyIds);
                        }

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
                        List<long> invalidMedicineIds = data.Medicines != null ?
                            data.Medicines
                            .Where(t => (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                                && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                            )
                            .Select(t => t.MedicineTypeId).ToList() : null;

                        List<long> invalidReqMetyIds = data.ServiceReqMeties != null ?
                            data.ServiceReqMeties
                            .Where(t => t.MedicineTypeId.HasValue && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId.Value))
                                && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId.Value)))
                            .Select(t => t.MedicineTypeId.Value).ToList() : null;

                        List<long> invalidIds = new List<long>();
                        if (IsNotNullOrEmpty(invalidMedicineIds))
                        {
                            invalidIds.AddRange(invalidMedicineIds);
                        }
                        if (IsNotNullOrEmpty(invalidReqMetyIds))
                        {
                            invalidIds.AddRange(invalidReqMetyIds);
                        }

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
                        List<long> invalidMedicineIds = data.Medicines != null ?
                            data.Medicines
                            .Where(t => (HisMedicineTypeCFG.THUOC_DOC_IDs != null && HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId))
                                || (HisMedicineTypeCFG.GAY_NGHIEN_IDs != null && HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                                || (HisMedicineTypeCFG.HUONG_THAN_IDs != null && HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                            )
                            .Select(t => t.MedicineTypeId).ToList() : null;

                        List<long> invalidReqMetyIds = data.ServiceReqMeties != null ?
                            data.ServiceReqMeties
                            .Where(t => t.MedicineTypeId.HasValue && ((HisMedicineTypeCFG.THUOC_DOC_IDs != null && HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId.Value))
                                || (HisMedicineTypeCFG.GAY_NGHIEN_IDs != null && HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId.Value))
                                || (HisMedicineTypeCFG.HUONG_THAN_IDs != null && HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId.Value))))
                            .Select(t => t.MedicineTypeId.Value).ToList() : null;

                        List<long> invalidIds = new List<long>();
                        if (IsNotNullOrEmpty(invalidMedicineIds))
                        {
                            invalidIds.AddRange(invalidMedicineIds);
                        }
                        if (IsNotNullOrEmpty(invalidReqMetyIds))
                        {
                            invalidIds.AddRange(invalidReqMetyIds);
                        }

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
