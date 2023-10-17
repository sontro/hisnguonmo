using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.NotTaken
{
    class HisExpMestNotTakenCheck : BusinessBase
    {
        internal HisExpMestNotTakenCheck()
            : base()
        {

        }

        internal HisExpMestNotTakenCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsNotFinished(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDaThucXuat);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExpoted(HIS_EXP_MEST raw, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<HIS_EXP_MEST_MATERIAL> materials)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDaThucXuat);
                    return false;
                }

                List<HIS_EXP_MEST_MEDICINE> expMedicines = new HisExpMestMedicineGet().GetByExpMestId(raw.ID);
                List<HIS_EXP_MEST_MATERIAL> expMaterials = new HisExpMestMaterialGet().GetByExpMestId(raw.ID);

                if (IsNotNullOrEmpty(expMedicines) && expMedicines.Exists(e => e.IS_EXPORT == Constant.IS_TRUE))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDaThucXuat);
                    return false;
                }

                if (IsNotNullOrEmpty(expMaterials) && expMaterials.Exists(e => e.IS_EXPORT == Constant.IS_TRUE))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDaThucXuat);
                    return false;
                }

                medicines = expMedicines;
                materials = expMaterials;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsExpMestSale(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongPhaiPhieuXuatBan, raw.EXP_MEST_CODE);
                    return false;
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
