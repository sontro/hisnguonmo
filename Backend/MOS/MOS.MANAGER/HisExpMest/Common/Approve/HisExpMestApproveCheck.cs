using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;

namespace MOS.MANAGER.HisExpMest.Common.Approve
{
    partial class HisExpMestApproveCheck : BusinessBase
    {
        //cac trang thai cho phep duyet
        private static List<long> ALLOW_STT_IDs = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
        };

        internal HisExpMestApproveCheck()
            : base()
        {

        }

        internal HisExpMestApproveCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool ValidateData(HisExpMestApproveSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_METY_REQ> metyReqs, ref List<HIS_EXP_MEST_MATY_REQ> matyReqs, ref List<HIS_EXP_MEST_BLTY_REQ> bltyReqs)
        {
            bool valid = true;
            try
            {
                //Voi cac loai duyet co tao y/c thi phai truyen len thong tin duyet
                if (HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID) && expMest.IS_REQUEST_BY_PACKAGE != Constant.IS_TRUE && !IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.Medicines) && !IsNotNullOrEmpty(data.Bloods) && !IsNotNullOrEmpty(data.SerialNumbers))
                {
                    throw new ArgumentNullException("data.Materials, data.Medicines, data.Bloods, data.SerialNumbers null");
                }

                valid = valid && this.HasNoDuplicate(data, expMest);
                valid = valid && this.IsValidOdd(data);
                valid = valid && this.HasRequest(data, expMest, ref metyReqs, ref matyReqs, ref bltyReqs);
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
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

        internal bool IsAllowed(HisExpMestApproveSDO data, bool isAuto, ref HIS_EXP_MEST expMest)
        {
            try
            {
                var tmp = new HisExpMestGet().GetById(data.ExpMestId);
                if (tmp == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("exp_mest_id ko hop le");
                    return false;
                }

                if (!(HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(tmp.EXP_MEST_TYPE_ID) || HisExpMestConstant.NOT_HAS_REQ_EXP_MEST_TYPE_IDs.Contains(tmp.EXP_MEST_TYPE_ID)))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Loai xuat nay ko cho phep duyet. EXP_MEST_TYPE_ID: " + tmp.EXP_MEST_TYPE_ID + "ALLOW EXP_MEST_TYPE_IDs: " + HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.ToString() + "," + HisExpMestConstant.NOT_HAS_REQ_EXP_MEST_TYPE_IDs.ToString());
                    return false;
                }

                if (!ALLOW_STT_IDs.Contains(tmp.EXP_MEST_STT_ID))
                {
                    string sttName = HisExpMestSttCFG.DATA.Where(o => o.ID == tmp.EXP_MEST_STT_ID).Select(o => o.EXP_MEST_STT_NAME).FirstOrDefault();
                    List<string> allowSttNames = HisExpMestSttCFG.DATA.Where(o => ALLOW_STT_IDs.Contains(o.ID)).Select(o => o.EXP_MEST_STT_NAME).ToList();
                    string allowSttNameStr = string.Join(",", allowSttNames);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDangOTrangThaiKhongChoPhepDuyet, sttName, allowSttNameStr);
                    return false;
                }

                if (tmp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL || tmp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Loai phieu xuat la phieu tong hop (phieu linh). Khong duoc thuc hien chuc nang nay");
                    return false;
                }

                //Neu tu dong duyet thi khong check phong lam viec
                if (!isAuto)
                {

                    WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                    if (workPlace == null || workPlace.MediStockId != tmp.MEDI_STOCK_ID)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                        return false;
                    }
                }
                expMest = tmp;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiem tra xem da vuot qua so luong da duyet hay chua
        /// </summary>
        /// <param name="metyReqs"></param>
        /// <param name="matyReqs"></param>
        /// <param name="bltyReqs"></param>
        /// <returns></returns>
        internal bool IsNotApprovalAmountExceed(HisExpMestApproveSDO data, List<HIS_EXP_MEST_METY_REQ> metyReqs, List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<HIS_EXP_MEST_BLTY_REQ> bltyReqs)
        {
            try
            {
                return this.IsNotApprovalAmountMedicineExceed(data.Medicines, metyReqs)
                    && this.IsNotApprovalAmountMaterialExceed(data.Materials, matyReqs)
                    && this.IsNotApprovalAmountMaterialReuseExceed(data.SerialNumbers, matyReqs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }

        private bool IsNotApprovalAmountMedicineExceed(List<ExpMedicineTypeSDO> medicines, List<HIS_EXP_MEST_METY_REQ> metyReqs)
        {
            if (IsNotNullOrEmpty(medicines) && IsNotNullOrEmpty(metyReqs))
            {
                foreach (HIS_EXP_MEST_METY_REQ mety in metyReqs)
                {
                    //cac loai phieu xuat thuong (ko phai la don thuoc) thi 1 phieu xuat ko co 2 thuoc giong nhau
                    ExpMedicineTypeSDO sdo = medicines.Where(o => o.ExpMestMetyReqId == mety.ID).FirstOrDefault();
                    if (sdo != null && ((mety.DD_AMOUNT ?? 0) + sdo.Amount) > mety.AMOUNT)
                    {
                        HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.Where(o => o.ID == mety.MEDICINE_TYPE_ID).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_SoLuongDuyetVuotQuaSoLuongYeuCau, medicineType.MEDICINE_TYPE_NAME);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsNotApprovalAmountMaterialExceed(List<ExpMaterialTypeSDO> materials, List<HIS_EXP_MEST_MATY_REQ> matyReqs)
        {
            if (IsNotNullOrEmpty(materials) && IsNotNullOrEmpty(matyReqs))
            {
                foreach (HIS_EXP_MEST_MATY_REQ mety in matyReqs)
                {
                    //cac loai phieu xuat thuong (ko phai la don thuoc) thi 1 phieu xuat ko co 2 vat tu giong nhau
                    ExpMaterialTypeSDO sdo = materials.Where(o => o.ExpMestMatyReqId == mety.ID).FirstOrDefault();
                    if (sdo != null && ((mety.DD_AMOUNT ?? 0) + sdo.Amount) > mety.AMOUNT)
                    {
                        HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == mety.MATERIAL_TYPE_ID).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_SoLuongDuyetVuotQuaSoLuongYeuCau, materialType.MATERIAL_TYPE_NAME);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsNotApprovalAmountMaterialReuseExceed(List<PresMaterialBySerialNumberSDO> materials, List<HIS_EXP_MEST_MATY_REQ> matyReqs)
        {
            if (IsNotNullOrEmpty(materials) && IsNotNullOrEmpty(matyReqs))
            {
                foreach (HIS_EXP_MEST_MATY_REQ mety in matyReqs)
                {
                    //cac loai phieu xuat thuong (ko phai la don thuoc) thi 1 phieu xuat ko co 2 vat tu giong nhau
                    List<PresMaterialBySerialNumberSDO> sdos = materials.Where(o => o.ExpMestMatyReqId == mety.ID).ToList();
                    decimal amount = sdos != null ? ((decimal)sdos.Count) : (decimal)0;
                    if (amount > 0 && ((mety.DD_AMOUNT ?? 0) + amount) > mety.AMOUNT)
                    {
                        HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == mety.MATERIAL_TYPE_ID).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_SoLuongDuyetVuotQuaSoLuongYeuCau, materialType.MATERIAL_TYPE_NAME);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool HasRequest(HisExpMestApproveSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_METY_REQ> metyReqs, ref List<HIS_EXP_MEST_MATY_REQ> matyReqs, ref List<HIS_EXP_MEST_BLTY_REQ> bltyReqs)
        {
            try
            {
                if (IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.SerialNumbers) || expMest.IS_REQUEST_BY_PACKAGE == Constant.IS_TRUE)
                {
                    var reqs = new HisExpMestMatyReqGet().GetByExpMestId(data.ExpMestId);

                    //Neu ko phai la xuat chuyen ko thi moi cho phep duyet loai thuoc ko co trong yeu cau
                    if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK
                        && expMest.IS_REQUEST_BY_PACKAGE != Constant.IS_TRUE)
                    {
                        var hasNoReqs = data.Materials != null ? data.Materials.Where(o => reqs == null
                        || !reqs.Exists(t => o.ExpMestMatyReqId.HasValue && t.ID == o.ExpMestMatyReqId.Value)).ToList() : null;
                        if (IsNotNullOrEmpty(hasNoReqs))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai du lieu duyet vat tu khong co yeu cau tuong ung: " + LogUtil.TraceData("hasNoReqs", hasNoReqs));
                        }

                        var noReqSeris = data.SerialNumbers != null ? data.SerialNumbers.Where(o => reqs == null
                        || !reqs.Exists(t => o.ExpMestMatyReqId.HasValue && t.ID == o.ExpMestMatyReqId.Value)).ToList() : null;
                        if (IsNotNullOrEmpty(noReqSeris))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai du lieu duyet vat tu tai du dung khong co yeu cau tuong ung: " + LogUtil.TraceData("noReqSeris", noReqSeris));
                        }
                    }
                    matyReqs = reqs;
                }

                if (IsNotNullOrEmpty(data.Medicines) || expMest.IS_REQUEST_BY_PACKAGE == Constant.IS_TRUE)
                {
                    var reqs = new HisExpMestMetyReqGet().GetByExpMestId(data.ExpMestId);

                    //Neu ko phai la xuat chuyen ko thi moi cho phep duyet loai thuoc ko co trong yeu cau
                    if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK
                        && expMest.IS_REQUEST_BY_PACKAGE != Constant.IS_TRUE)
                    {
                        var hasNoReqs = data.Medicines.Where(o => reqs == null
                            || !reqs.Exists(t => o.ExpMestMetyReqId.HasValue && t.ID == o.ExpMestMetyReqId.Value)).ToList();
                        if (IsNotNullOrEmpty(hasNoReqs))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai du lieu duyet thuoc khong co yeu cau tuong ung: " + LogUtil.TraceData("hasNoReqs", hasNoReqs));
                        }
                    }
                    metyReqs = reqs;
                }

                if (IsNotNullOrEmpty(data.Bloods))
                {
                    var reqs = new HisExpMestBltyReqGet().GetByExpMestId(data.ExpMestId);
                    var hasNoReqs = data.Bloods.Where(o => reqs == null
                        || !reqs.Exists(t => o.ExpMestBltyReqId.HasValue && t.ID == o.ExpMestBltyReqId.Value)).ToList();
                    if (IsNotNullOrEmpty(hasNoReqs))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai du lieu duyet mau khong co yeu cau tuong ung: " + LogUtil.TraceData("hasNoReqs", hasNoReqs));
                    }
                    bltyReqs = reqs;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private bool HasNoDuplicate(HisExpMestApproveSDO data, HIS_EXP_MEST expMest)
        {
            try
            {
                if (IsNotNullOrEmpty(data.Materials) && (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS))
                {
                    if (data.Materials.Select(o => o.MaterialTypeId).Distinct().Count() != data.Materials.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu truyen len chua 2 material_type_id trung nhau");
                    }
                    if (data.Materials.Select(o => o.ExpMestMatyReqId).Distinct().Count() != data.Materials.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu truyen len chua 2 ExpMestMatyReqId trung nhau");
                    }
                }

                if (IsNotNullOrEmpty(data.Medicines) && (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS))
                {
                    if (data.Medicines.Select(o => o.MedicineTypeId).Distinct().Count() != data.Medicines.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu truyen len chua 2 medicine_type_id trung nhau");
                    }
                    if (data.Medicines.Select(o => o.ExpMestMetyReqId).Distinct().Count() != data.Medicines.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu truyen len chua 2 ExpMestMetyReqId trung nhau");
                    }
                }

                if (IsNotNullOrEmpty(data.Bloods))
                {
                    if (data.Bloods.Select(o => o.BloodId).Distinct().Count() != data.Bloods.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu truyen len chua 2 BloodId trung nhau");
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

        private bool IsValidOdd(HisExpMestApproveSDO data)
        {
            try
            {
                List<string> notAllowOdds = new List<string>();
                if (IsNotNullOrEmpty(data.Materials))
                {
                    var tmp = HisMaterialTypeCFG.DATA
                        .Where(o => data.Materials.GroupBy(g => g.MaterialTypeId).ToList().Exists(t => t.Key == o.ID && Math.Ceiling(t.Sum(s => s.Amount)) - t.Sum(s => s.Amount) > 0) && o.IS_ALLOW_EXPORT_ODD != MOS.UTILITY.Constant.IS_TRUE).Select(o => o.MATERIAL_TYPE_NAME).ToList();
                    if (IsNotNullOrEmpty(tmp))
                    {
                        notAllowOdds.AddRange(tmp);
                    }
                }
                if (IsNotNullOrEmpty(data.Medicines))
                {
                    List<string> tmp = HisMedicineTypeCFG.DATA
                        .Where(o => data.Medicines.GroupBy(g => g.MedicineTypeId).ToList().Exists(t => t.Key == o.ID && Math.Ceiling(t.Sum(s => s.Amount)) - t.Sum(s => s.Amount) > 0) && o.IS_ALLOW_EXPORT_ODD != MOS.UTILITY.Constant.IS_TRUE).Select(o => o.MEDICINE_TYPE_NAME).ToList();
                    if (IsNotNullOrEmpty(tmp))
                    {
                        notAllowOdds.AddRange(tmp);
                    }
                }

                if (IsNotNullOrEmpty(notAllowOdds))
                {
                    string nameStr = string.Join(",", notAllowOdds);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_ThuocVatTuKhongChoPhepXuatLe, nameStr);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
