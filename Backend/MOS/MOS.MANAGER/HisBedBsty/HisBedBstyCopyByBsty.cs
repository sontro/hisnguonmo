using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBedBsty
{
    class HisBedBstyCopyByBsty : BusinessBase
    {
        internal HisBedBstyCopyByBsty()
            : base()
        {

        }

        internal HisBedBstyCopyByBsty(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisBedBstyCopyByBstySDO data, ref List<HIS_BED_BSTY> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyBedServiceTypeId);
                valid = valid && IsGreaterThanZero(data.PasteBedServiceTypeId);
                if (valid)
                {
                    List<HIS_BED_BSTY> newMestMatys = new List<HIS_BED_BSTY>();

                    List<HIS_BED_BSTY> copyMestMatys = DAOWorker.SqlDAO.GetSql<HIS_BED_BSTY>("SELECT * FROM HIS_BED_BSTY WHERE BED_SERVICE_TYPE_ID = :param1", data.CopyBedServiceTypeId);
                    List<HIS_BED_BSTY> pasteMestMatys = DAOWorker.SqlDAO.GetSql<HIS_BED_BSTY>("SELECT * FROM HIS_BED_BSTY WHERE BED_SERVICE_TYPE_ID = :param1", data.PasteBedServiceTypeId);
                    if (!IsNotNullOrEmpty(copyMestMatys))
                    {
                        V_HIS_SERVICE service = Config.HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == data.CopyBedServiceTypeId);
                        string name = service != null ? service.SERVICE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_DichVuChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyBedBstys");
                    }

                    foreach (HIS_BED_BSTY copyData in copyMestMatys)
                    {
                        HIS_BED_BSTY mestMaty = pasteMestMatys != null ? pasteMestMatys.FirstOrDefault(o => o.BED_ID
                            == copyData.BED_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_BED_BSTY();
                            mestMaty.BED_SERVICE_TYPE_ID = data.PasteBedServiceTypeId;
                            mestMaty.BED_ID = copyData.BED_ID;
                            newMestMatys.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        if (!DAOWorker.HisBedBstyDAO.CreateList(newMestMatys))
                        {
                            throw new Exception("Khong tao duoc HIS_BED_BSTY");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_BED_BSTY>();
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        resultData.AddRange(newMestMatys);
                    }
                    if (IsNotNullOrEmpty(pasteMestMatys))
                    {
                        resultData.AddRange(pasteMestMatys);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                resultData = null;
            }
            return result;
        }
    }
}
