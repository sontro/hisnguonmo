using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMest.Moba
{
    class HisImpMestMobaCheck : BusinessBase
    {
        private static List<long> impMestTypeMobas = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DMTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__HPTL
        };
        internal HisImpMestMobaCheck()
            : base()
        {

        }

        internal HisImpMestMobaCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!data.MOBA_EXP_MEST_ID.HasValue || !IsGreaterThanZero(data.MOBA_EXP_MEST_ID.Value)) throw new ArgumentNullException("data.MOBA_EXP_MEST_ID");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool CheckValid(long? impMediStockId, HIS_IMP_MEST data, ref HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                //Kiem tra loai nhap la thu hoi
                if (!impMestTypeMobas.Contains(data.IMP_MEST_TYPE_ID))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("IMP_MEST_TYPE_ID invalid: " + data.IMP_MEST_TYPE_ID);
                }

                expMest = new HisExpMestGet().GetByServiceReqId(data.MOBA_EXP_MEST_ID.Value);
                if (expMest == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("MOBA_EXP_MEST_ID invalid: " + data.MOBA_EXP_MEST_ID.Value);
                }

                if (expMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuChuaThucXuat);
                    return false;
                }
                data.MOBA_EXP_MEST_ID = expMest.ID;
                if (impMediStockId.HasValue)
                {
                    data.MEDI_STOCK_ID = impMediStockId.Value;
                }
                else
                {
                    data.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool VerifyExpMestId(HIS_IMP_MEST data, long expMestId, long? impMediStockId, ref HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                expMest = new HisExpMestGet().GetById(expMestId);
                if (expMest == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("expMestId invalid: " + expMestId);
                }

                if (expMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuChuaThucXuat);
                    return false;
                }
                data.MOBA_EXP_MEST_ID = expMest.ID;
                if (impMediStockId.HasValue)
                {
                    data.MEDI_STOCK_ID = impMediStockId.Value;
                }
                else
                {
                    data.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckValidMobaPres(HIS_IMP_MEST data, HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                HIS_MEDI_STOCK mediStock = new HisMediStockGet().GetById(data.MEDI_STOCK_ID);
                if (mediStock == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("MEDI_STOCK_ID invalid: " + data.MEDI_STOCK_ID);
                }

                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                {
                    if (!HisMobaImpMestCFG.MOBA_INTO_MEDI_STOCK_EXPORT__CABINET)
                    {
                        if (mediStock.IS_CABINET.HasValue && mediStock.IS_CABINET.Value == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Thu hoi tu truc. Kho nhap phai la kho thuong (khong phai tu truc)" + LogUtil.TraceData("MediStock", mediStock));
                        }
                    }
                    else
                    {
                        if (expMest.MEDI_STOCK_ID != data.MEDI_STOCK_ID)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_KhoNhapKhongPhaiLaKhoXuat);
                            return false;
                        }

                        if (HisMediStockCFG.IS_USE_BASE_AMOUNT_CABINET && expMest.XBTT_EXP_MEST_ID.HasValue)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_DonDaDuocBuCoSoKhongChoPhepThuHoiVeTuTruc);
                            return false;
                        }
                    }
                    if (HisMobaImpMestCFG.NOT_ALLOW_FOR_CABINET)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMobaImpMest_KhongChoPhepThuHoiDoiVoiPhieuXuatTuTuTruc);
                        return false;
                    }
                    data.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL;

                }
                else if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                {
                    data.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL;
                }
                else
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai la don noi tru hoac don tu truc: " + LogUtil.TraceData("ExpMest", expMest));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckValidMobaBlood(HIS_IMP_MEST data, HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai la don mau, khong cho phep nhap thu hoi don mau: " + LogUtil.TraceData("ExpMest", expMest));
                }
                data.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DMTL;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckValidMobaDepa(HIS_IMP_MEST data, HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__HPKP)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai la don hao phi khoa phong, khong cho phep nhap thu hoi don hao phi khoa phong: " + LogUtil.TraceData("ExpMest", expMest));
                }
                data.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__HPTL;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckValidMobaSale(HIS_IMP_MEST data, HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai la xua ban, khong cho phep nhap thu hoi xua ban: " + LogUtil.TraceData("ExpMest", expMest));
                }
                data.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BTL;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool VerifyServiceReq(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (!expMest.SERVICE_REQ_ID.HasValue || !expMest.TDL_TREATMENT_ID.HasValue)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Phieu xuat khong co SERVICE_REQ_ID || TDL_TREATMENT_ID " + LogUtil.TraceData("ExpMest", expMest));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool VerifySereServ(HIS_EXP_MEST expMest, List<HIS_SERE_SERV> hisSereServs)
        {
            bool valid = true;
            try
            {
                //Can lay toan bo sere_serv (vi khi xu ly update ti le BHYT can xu ly toan bo sere_serv trong bang ke)
                List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByTreatmentId(expMest.TDL_TREATMENT_ID ?? 0);

                if (!IsNotNullOrEmpty(sereServs))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong lay duoc HIS_SERE_SERV theo TDL_TREATMENT_ID: " + expMest.TDL_TREATMENT_ID);
                }
                hisSereServs.AddRange(sereServs.OrderBy(o => o.SERVICE_REQ_ID).ThenBy(o => o.AMOUNT).ToList());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckValidRequestRoom(HIS_IMP_MEST data, HIS_EXP_MEST expMest, long requestRoomId)
        {
            bool valid = true;
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(requestRoomId);
                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    return false;
                }

                //Xuat ban co the do phong kham tao ra nhung nha thuoc thuc hien thu hoi
                if (expMest.REQ_DEPARTMENT_ID != workPlace.DepartmentId
                    && expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMobaImpMest_DonThuocKhongThuocKhoaDangLamViecKhongChoPhepTaoThuHoi);
                    return false;
                }

                data.REQ_ROOM_ID = workPlace.RoomId;
                data.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool ValidateDataDepa(HisImpMestMobaDepaSDO data)
        {
            bool valid = true;
            try
            {
                if (!IsNotNullOrEmpty(data.MobaMedicines) && !IsNotNullOrEmpty(data.MobaMaterials))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Du lieu khong hop le. Ko ton tai du lieu thuoc va vat tu de thu hoi." + LogUtil.TraceData("data", data));
                }
                if (IsNotNullOrEmpty(data.MobaMedicines) && data.MobaMedicines.Exists(e => e.Amount <= 0 || e.MedicineId <= 0))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Du lieu thuoc khong hop le." + LogUtil.TraceData("MobaMedicines", data.MobaMedicines));
                }

                if (IsNotNullOrEmpty(data.MobaMaterials) && data.MobaMaterials.Exists(e => e.Amount <= 0 || e.MaterialId <= 0))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Du lieu vat tu khong hop le." + LogUtil.TraceData("MobaMaterials", data.MobaMaterials));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool ValidateDataSale(HisImpMestMobaSaleSDO data)
        {
            bool valid = true;
            try
            {
                if (!IsNotNullOrEmpty(data.MobaMedicines) && !IsNotNullOrEmpty(data.MobaMaterials))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Du lieu khong hop le. Ko ton tai du lieu thuoc va vat tu de thu hoi.");
                }
                if (IsNotNullOrEmpty(data.MobaMedicines) && data.MobaMedicines.Exists(e => e.Amount <= 0 || e.MedicineId <= 0))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Du lieu thuoc khong hop le (Amount or MedicineId Invalid)");
                }

                if (IsNotNullOrEmpty(data.MobaMaterials) && data.MobaMaterials.Exists(e => e.Amount <= 0 || e.MaterialId <= 0))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Du lieu vat tu khong hop le (Amount or MaterialId Invalid)." + LogUtil.TraceData("MobaMaterials", data.MobaMaterials));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool ValidateDataPres(HisImpMestMobaPresSDO data, List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (!IsNotNullOrEmpty(data.MobaPresMedicines) && !IsNotNullOrEmpty(data.MobaPresMaterials))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Du lieu khong hop le. Ko ton tai du lieu thuoc/vat tu de thu hoi." + LogUtil.TraceData("data", data));
                }

                if (IsNotNullOrEmpty(data.MobaPresMedicines))
                {
                    List<long> expMestMedicineIds = data.MobaPresMedicines.Select(s => s.ExpMestMedicineId).ToList();

                    if (data.MobaPresMedicines.Exists(e => e.ExpMestMedicineId <= 0 || e.Amount <= 0))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu khong hop le." + LogUtil.TraceData("data.MobaPresMedicines", data.MobaPresMedicines));
                    }

                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new HisExpMestMedicineGet().GetByIds(expMestMedicineIds);
                    if (expMestMedicines == null || expMestMedicines.Count != expMestMedicineIds.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc HisExpMestMedicines hoac so luong HisExpMestMedicines khong bang so luong expMestMedicineIds" + LogUtil.TraceData("expMestMedicineIds", expMestMedicineIds));
                    }

                    if (expMestMedicines.Exists(e => !e.IS_EXPORT.HasValue || e.IS_EXPORT.Value != MOS.UTILITY.Constant.IS_TRUE))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai HisExpMestMedicine chua duoc thuc xuat (is_export <> 1)" + LogUtil.TraceData("expMestMedicines", expMestMedicines));
                    }

                    if (expMestMedicines.Exists(e => e.EXP_MEST_ID != data.ExpMestId))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai HisExpMestMedicine khong thuoc phieu xuat" + LogUtil.TraceData("expMestMedicines", expMestMedicines));
                    }
                    hisExpMestMedicines.AddRange(expMestMedicines);
                }

                if (IsNotNullOrEmpty(data.MobaPresMaterials))
                {
                    List<long> expMestMaterialIds = data.MobaPresMaterials.Select(s => s.ExpMestMaterialId).ToList();

                    if (data.MobaPresMaterials.Exists(e => e.ExpMestMaterialId <= 0 || e.Amount <= 0))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu khong hop le." + LogUtil.TraceData("data.MobaPresMaterials", data.MobaPresMaterials));
                    }
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new HisExpMestMaterialGet().GetByIds(expMestMaterialIds);
                    if (expMestMaterials == null || expMestMaterials.Count != expMestMaterialIds.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc HisExpMestMaterials hoac so luong HisExpMestMaterials khong bang so luong expMestMaterialIds" + LogUtil.TraceData("expMestMaterialIds", expMestMaterialIds));
                    }

                    if (expMestMaterials.Exists(e => !e.IS_EXPORT.HasValue || e.IS_EXPORT.Value != MOS.UTILITY.Constant.IS_TRUE))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai HisExpMestMaterial chua duoc thuc xuat (is_export <> 1)" + LogUtil.TraceData("expMestMaterials", expMestMaterials));
                    }

                    if (expMestMaterials.Exists(e => e.EXP_MEST_ID != data.ExpMestId))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai HisExpMestMaterials khong thuoc data.ExpMestId" + LogUtil.TraceData("expMestMaterials", expMestMaterials));
                    }
                    hisExpMestMaterials.AddRange(expMestMaterials);
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckMaxDayAllowMobaPrescription(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (HisImpMestCFG.MAX_SUSPENDING_DAY_ALLOWED_FOR_PRESCRIPTION <= 0)
                {
                    return true;
                }
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT
                    || expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                {
                    DateTime dt = DateTime.Now.AddDays((-HisImpMestCFG.MAX_SUSPENDING_DAY_ALLOWED_FOR_PRESCRIPTION));
                    long date = Convert.ToInt64(dt.ToString("yyyyMMddHHmmss"));

                    string sql = "SELECT IMP_MEST_CODE FROM HIS_IMP_MEST WHERE IMP_MEST_STT_ID <> 5 AND CREATE_TIME < :param1 AND REQ_DEPARTMENT_ID = :param2 AND IMP_MEST_TYPE_ID IN (:param3, :param4)";

                    List<string> notImports = DAOWorker.SqlDAO.GetSql<string>(sql, date, expMest.REQ_DEPARTMENT_ID, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL);
                    if (IsNotNullOrEmpty(notImports))
                    {
                        string codes = String.Join(",", notImports);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_CacPhieuNhapTraLaiSauQuaHanChuaThucNhap, codes);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
