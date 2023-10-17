using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMediStockMaty;
using MOS.MANAGER.HisMediStockMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base
{
    class HisExpMestBaseCheck : BusinessBase
    {
        internal HisExpMestBaseCheck()
            : base()
        {

        }

        internal HisExpMestBaseCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool IsAllowEdit(HIS_EXP_MEST raw, WorkPlaceSDO workplace)
        {
            bool valid = true;
            try
            {
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (!(raw.CREATOR == loginname || raw.MEDI_STOCK_ID == workplace.MediStockId))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_BanKhongCoQuyenXoa);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }


        internal bool IsNotExistsApproveDetail(HIS_EXP_MEST raw, ref List<HIS_MEDI_STOCK_METY> stockMetys, ref List<HIS_MEDI_STOCK_MATY> stockMatys, ref List<HIS_EXP_MEST_METY_REQ> metyReqs, ref List<HIS_EXP_MEST_MATY_REQ> matyReqs)
        {
            bool valid = true;
            try
            {
                List<HIS_MEDI_STOCK_METY> metyInStocks = null;
                List<HIS_MEDI_STOCK_MATY> matyInStocks = null;
                List<HIS_EXP_MEST_MATERIAL> materials = new HisExpMestMaterialGet().GetByExpMestId(raw.ID);
                List<HIS_EXP_MEST_MEDICINE> medicines = new HisExpMestMedicineGet().GetByExpMestId(raw.ID);
                if (IsNotNullOrEmpty(materials) || IsNotNullOrEmpty(medicines))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Phieu xua bo sung/ hoan co so ton tai HIS_EXP_MEST_MATERIAL,HIS_EXP_MEST_MEDICINE. Kiem tra lai du lieu");
                }
                if (raw.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__ADDITION)
                {
                    metyInStocks = new HisMediStockMetyGet().GetByMediStockId(raw.IMP_MEDI_STOCK_ID.Value);
                    matyInStocks = new HisMediStockMatyGet().GetByMediStockId(raw.IMP_MEDI_STOCK_ID.Value);
                }
                else
                {
                    metyInStocks = new HisMediStockMetyGet().GetByMediStockId(raw.MEDI_STOCK_ID);
                    matyInStocks = new HisMediStockMatyGet().GetByMediStockId(raw.MEDI_STOCK_ID);
                }
                metyReqs = new HisExpMestMetyReqGet().GetByExpMestId(raw.ID);
                matyReqs = new HisExpMestMatyReqGet().GetByExpMestId(raw.ID);
                stockMatys = matyInStocks;
                stockMetys = metyInStocks;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistsImpMest(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> exists = new HisImpMestGet().GetByChmsExpMestId(raw.ID);
                if (IsNotNullOrEmpty(exists))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Da ton tai phieu nhap tuong ung voi phieu xuat " + LogUtil.TraceData("ExpMest", raw));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistsExpMestBase(HIS_MEDI_STOCK cabinetStock, List<ExpMedicineTypeSDO> medicineTypes, List<ExpMaterialTypeSDO> materialTypes)
        {
            bool valid = true;
            try
            {
                List<L_HIS_EXP_MEST_MATY_REQ> materials = null;
                List<L_HIS_EXP_MEST_METY_REQ> medicines = null;

                List<string> expCodes = new List<string>();

                if (IsNotNullOrEmpty(materialTypes))
                {
                    HisExpMestMatyReqLViewFilterQuery filter = new HisExpMestMatyReqLViewFilterQuery();
                    filter.HAS_CHMS_TYPE_ID = true;
                    filter.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID = cabinetStock.ID;
                    filter.EXP_MEST_STT_IDs = new List<long>(){
                        IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST,
                        IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
                    };
                    filter.MATERIAL_TYPE_IDs = materialTypes.Select(o => o.MaterialTypeId).ToList();

                    materials = new HisExpMestMatyReqGet().GetLView(filter);
                    if (IsNotNullOrEmpty(materials))
                    {
                        List<string> codes = materials.Select(o => o.EXP_MEST_CODE).ToList();
                        expCodes.AddRange(codes);
                    }
                }
                if (IsNotNullOrEmpty(medicineTypes))
                {
                    HisExpMestMetyReqLViewFilterQuery filter = new HisExpMestMetyReqLViewFilterQuery();
                    filter.HAS_CHMS_TYPE_ID = true;
                    filter.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID = cabinetStock.ID;
                    filter.EXP_MEST_STT_IDs = new List<long>(){
                        IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST,
                        IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
                    };
                    filter.MEDICINE_TYPE_IDs = medicineTypes.Select(o => o.MedicineTypeId).ToList();

                    medicines = new HisExpMestMetyReqGet().GetLView(filter);

                    if (IsNotNullOrEmpty(medicines))
                    {
                        List<string> codes = medicines.Select(o => o.EXP_MEST_CODE).ToList();
                        expCodes.AddRange(codes);
                    }
                }

                if (IsNotNullOrEmpty(expCodes))
                {
                    expCodes = expCodes.Distinct().ToList();
                    string codes = String.Join(",", expCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStock_TonTaiPhieuHoanBoSungChuaXuat, cabinetStock.MEDI_STOCK_NAME, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

    }
}
