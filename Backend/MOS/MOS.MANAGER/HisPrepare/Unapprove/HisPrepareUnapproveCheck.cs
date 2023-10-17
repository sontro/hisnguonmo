using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisPrepare.Check;
using MOS.MANAGER.HisPrepareMaty;
using MOS.MANAGER.HisPrepareMety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Unapprove
{
    class HisPrepareUnapproveCheck : BusinessBase
    {
        internal HisPrepareUnapproveCheck()
            : base()
        {

        }

        internal HisPrepareUnapproveCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool CheckAllowUnapprove(HIS_PREPARE data, ref List<HIS_PREPARE_MATY> materials, ref List<HIS_PREPARE_METY> medicines)
        {
            bool valid = true;
            try
            {
                List<HIS_PREPARE_MATY> prepareMatys = new HisPrepareMatyGet().GetByPrepareId(data.ID);
                List<HIS_PREPARE_METY> prepareMetys = new HisPrepareMetyGet().GetByPrepareId(data.ID);
                List<string> names = new List<string>();
                HisPrepareCheckAmount checkAmount = new HisPrepareCheckAmount(param);
                List<long> idErrors = new List<long>();
                if (IsNotNullOrEmpty(prepareMatys))
                {
                    foreach (HIS_PREPARE_MATY maty in prepareMatys)
                    {
                        if (!checkAmount.CheckAmountMaterialNotInPrepare(data.TREATMENT_ID, maty.MATERIAL_TYPE_ID, data.ID, 0))
                        {
                            idErrors.Add(maty.MATERIAL_TYPE_ID);
                        }
                    }
                    if (IsNotNullOrEmpty(idErrors))
                    {
                        names.AddRange(HisMaterialTypeCFG.DATA.Where(o => idErrors.Contains(o.ID)).Select(s => s.MATERIAL_TYPE_NAME).ToList());
                    }
                }

                idErrors = new List<long>();
                if (IsNotNullOrEmpty(prepareMetys))
                {
                    foreach (HIS_PREPARE_METY mety in prepareMetys)
                    {
                        if (!checkAmount.CheckAmountMedicineNotInPrepare(data.TREATMENT_ID, mety.MEDICINE_TYPE_ID, data.ID, 0))
                        {
                            idErrors.Add(mety.MEDICINE_TYPE_ID);
                        }
                    }
                    if (IsNotNullOrEmpty(idErrors))
                    {
                        names.AddRange(HisMedicineTypeCFG.DATA.Where(o => idErrors.Contains(o.ID)).Select(s => s.MEDICINE_TYPE_NAME).ToList());
                    }
                }
                if (IsNotNullOrEmpty(names))
                {
                    string typeName = String.Join(";", names);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPrepare_CacThuocVatTuSauDaDuocKe, typeName);
                    return false;
                }

                materials = prepareMatys;
                medicines = prepareMetys;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsApproverOrAdmin(HIS_PREPARE raw)
        {
            bool valid = true;
            try
            {
                if (HisEmployeeUtil.IsAdmin())
                {
                    return true;
                }

                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (raw.APPROVAL_LOGINNAME != loginname)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common_BanKhongPhaiLaNguoiDuyet);
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
