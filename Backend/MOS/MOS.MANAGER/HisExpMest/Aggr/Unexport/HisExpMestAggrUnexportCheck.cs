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
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServ;

namespace MOS.MANAGER.HisExpMest.Aggr.Unexport
{
    partial class HisExpMestAggrUnexportCheck : BusinessBase
    {
        internal HisExpMestAggrUnexportCheck()
            : base()
        {

        }

        internal HisExpMestAggrUnexportCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsAllowed(HisExpMestSDO data, ref HIS_EXP_MEST aggrExpMest, ref List<HIS_EXP_MEST> children)
        {
            try
            {
                HIS_EXP_MEST tmpAggr = new HisExpMestGet().GetById(data.ExpMestId);

                if (tmpAggr.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    HIS_EXP_MEST_STT expMestStt = HisExpMestSttCFG.DATA.Where(o => o.ID == tmpAggr.EXP_MEST_STT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuDaOTrangThaiKhongChoPhepHuyThucXuat, expMestStt.EXP_MEST_STT_NAME);
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != tmpAggr.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }
                aggrExpMest = tmpAggr;

                children = new HisExpMestGet().GetByAggrExpMestId(tmpAggr.ID);
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
        /// Kiem tra xem cac phieu xuat da co phieu nhap thu hoi nao chua
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        internal bool HasNoMoba(List<HIS_EXP_MEST> children)
        {
            try
            {
                if (IsNotNullOrEmpty(children))
                {
                    List<long> expMestIds = children.Select(o => o.ID).ToList();
                    List<HIS_IMP_MEST> mobaImpMests = new HisImpMestGet().GetByMobaExpMestIds(expMestIds);
                    if (IsNotNullOrEmpty(mobaImpMests))
                    {
                        List<string> impMestCodes = mobaImpMests.Select(o => o.IMP_MEST_CODE).ToList();
                        string impMestCodeStr = string.Join(",", impMestCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuDaCoPhieuNhapHoi, impMestCodeStr);
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

        internal bool HasNoMediStockPeriod(long aggrExpMestId, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<HIS_EXP_MEST_MATERIAL> materials)
        {
            try
            {
                //Lay cac du lieu da duoc xuat
                //Luu y: lay cac exp_mest_medicine của các đơn nội trú/đơn bù lẻ thuộc phiếu lĩnh 
                //(lấy theo TDL_AGGR_EXP_MEST_ID) hoặc các dữ liệu bù lẻ gắn trực tiếp vào phiếu lĩnh (lấy theo EXP_MEST_ID)
                HisExpMestMedicineFilterQuery filter = new HisExpMestMedicineFilterQuery();
                filter.TDL_AGGR_EXP_MEST_ID__OR__EXP_MEST_ID = aggrExpMestId;
                filter.IS_EXPORT = true;
                medicines = new HisExpMestMedicineGet().Get(filter);

                //Lay cac du lieu da duoc xuat
                HisExpMestMaterialFilterQuery materialFilter = new HisExpMestMaterialFilterQuery();
                materialFilter.TDL_AGGR_EXP_MEST_ID__OR__EXP_MEST_ID = aggrExpMestId;
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
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool HasNoBill(List<HIS_EXP_MEST> children)
        {
            bool valid = true;
            try
            {
                List<long> servicereqIds = children.Where(o => o.SERVICE_REQ_ID.HasValue).Select(o => o.SERVICE_REQ_ID.Value).ToList();
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqIds(servicereqIds);
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

        internal bool CheckImpOdd(List<HIS_EXP_MEST> children, ref List<HIS_IMP_MEST> impOdds)
        {
            bool valid = true;
            try
            {
                List<long> oddExpMestIds = children != null ? children.Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL).Select(s => s.ID).ToList() : null;
                if (IsNotNullOrEmpty(oddExpMestIds))
                {
                    impOdds = new HisImpMestGet().GetByChmsExpMestIds(oddExpMestIds);
                    List<string> approves = impOdds != null ? impOdds.Where(o => o.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL).Select(s => s.IMP_MEST_CODE).ToList() : null;
                    if (IsNotNullOrEmpty(approves))
                    {
                        string ms = String.Join(",", approves);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuNhapBuLeSauDaDuocDuyet, ms);
                        return false;
                    }

                    List<string> imports = impOdds != null ? impOdds.Where(o => o.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT).Select(s => s.IMP_MEST_CODE).ToList() : null;
                    if (IsNotNullOrEmpty(imports))
                    {
                        string ms = String.Join(",", imports);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuNhapBuLeSauDaDuocNhap, ms);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
