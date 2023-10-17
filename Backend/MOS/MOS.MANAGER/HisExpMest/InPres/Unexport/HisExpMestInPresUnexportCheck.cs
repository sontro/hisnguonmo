using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Unexport
{
    class HisExpMestInPresUnexportCheck : BusinessBase
    {
        internal HisExpMestInPresUnexportCheck()
            : base()
        {

        }

        internal HisExpMestInPresUnexportCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsAllowed(HisExpMestSDO data, ref HIS_EXP_MEST expMest)
        {
            try
            {
                HIS_EXP_MEST tmpExp = new HisExpMestGet().GetById(data.ExpMestId);

                if (tmpExp == null || tmpExp.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    Inventec.Common.Logging.LogSystem.Warn("exp_mest_id ko hop le hoac loai ko phai la don noi tru");
                    return false;
                }

                if (tmpExp.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmpExp.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepHuyThucXuat, expMestStt.EXP_MEST_STT_NAME);
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != tmpExp.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }
                expMest = tmpExp;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal bool HasNoMediStockPeriod(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<HIS_EXP_MEST_MATERIAL> materials)
        {
            try
            {
                //Lay cac du lieu da duoc xuat
                HisExpMestMedicineFilterQuery filter = new HisExpMestMedicineFilterQuery();
                filter.EXP_MEST_ID = expMest.ID;
                filter.IS_EXPORT = true;
                medicines = new HisExpMestMedicineGet().Get(filter);

                //Lay cac du lieu da duoc xuat
                HisExpMestMaterialFilterQuery materialFilter = new HisExpMestMaterialFilterQuery();
                materialFilter.EXP_MEST_ID = expMest.ID;
                materialFilter.IS_EXPORT = true;
                materials = new HisExpMestMaterialGet().Get(materialFilter);

                //Kiem tra xem da co du lieu nao duoc chot ky chua
                bool check = true;
                check = check && (!IsNotNullOrEmpty(medicines) || !medicines.Exists(o => o.MEDI_STOCK_PERIOD_ID.HasValue));
                check = check && (!IsNotNullOrEmpty(materials) || !materials.Exists(o => o.MEDI_STOCK_PERIOD_ID.HasValue));

                if (!check)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_DaDuocChotKyKhongChoPhepCapNhatHoacXoa);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        internal bool HasNoBill(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqId(expMest.SERVICE_REQ_ID.Value);
                hisSereServs = hisSereServs != null ? hisSereServs.Where(o => o.AMOUNT > 0 && o.VIR_TOTAL_PATIENT_PRICE > 0).ToList() : null;
                if (IsNotNullOrEmpty(hisSereServs))
                {
                    HisSereServCheck ssChecker = new HisSereServCheck(param);
                    valid = ssChecker.HasNoBill(hisSereServs);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra xem cac phieu xuat da co phieu nhap thu hoi nao chua
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        internal bool HasNoMoba(HIS_EXP_MEST expMest)
        {
            try
            {
                List<HIS_IMP_MEST> mobaImpMests = new HisImpMestGet().GetByMobaExpMestId(expMest.ID);
                if (IsNotNullOrEmpty(mobaImpMests))
                {
                    List<string> impMestCodes = mobaImpMests.Select(o => o.IMP_MEST_CODE).ToList();
                    string impMestCodeStr = string.Join(",", impMestCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuDaCoPhieuNhapHoi, impMestCodeStr);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
           
        }


        internal bool IsValidCancellationOfExport(List<HIS_EXP_MEST_MATERIAL> materials, HIS_EXP_MEST expMest)
        {
            try
            {
                List<HIS_EXP_MEST_MATERIAL> materialProcess = IsNotNullOrEmpty(materials) ? materials.Where(o => (o.EXP_MEST_ID == expMest.ID)
                        && !String.IsNullOrWhiteSpace(o.SERIAL_NUMBER)).ToList() : null;

                if (IsNotNullOrEmpty(materialProcess))
                {
                    List<string> serialNumbers = materialProcess.Select(o => o.SERIAL_NUMBER).Distinct().ToList();
                    List<long> materialIds = materialProcess.Select(o => o.MATERIAL_ID.Value).Distinct().ToList();
                    HisImpMestMaterialView4FilterQuery filterImp = new HisImpMestMaterialView4FilterQuery();
                    filterImp.SERIAL_NUMBERs = serialNumbers;
                    filterImp.MATERIAL_IDs = materialIds;
                    filterImp.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    List<V_HIS_IMP_MEST_MATERIAL_4> imp = new HisImpMestMaterialGet().GetView4(filterImp);

                    List<V_HIS_IMP_MEST_MATERIAL_4> impMest = IsNotNullOrEmpty(imp) ? imp.Where(o => (o.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT) || (o.IMP_TIME > expMest.LAST_EXP_TIME)).ToList() : null;

                    if (IsNotNullOrEmpty(impMest))
                    {
                        List<string> serials = impMest.Select(o => o.SERIAL_NUMBER).Distinct().ToList();
                        List<string> impMestCodes = impMest.Select(o => o.IMP_MEST_CODE).Distinct().ToList();

                        string serial = string.Join(",", serials);
                        string impMestCode = string.Join(",", impMestCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_VatTuCoSoSerialDaDuocNhapTaiSuDung
, serial, impMestCode);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }
    }
}
