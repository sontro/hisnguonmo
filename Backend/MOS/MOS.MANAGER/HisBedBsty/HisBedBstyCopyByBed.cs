using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBed;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBedBsty
{
    class HisBedBstyCopyByBed : BusinessBase
    {
        internal HisBedBstyCopyByBed()
            : base()
        {

        }

        internal HisBedBstyCopyByBed(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisBedBstyCopyByBedSDO data, ref List<HIS_BED_BSTY> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyBedId);
                valid = valid && IsGreaterThanZero(data.PasteBedId);
                if (valid)
                {
                    List<HIS_BED_BSTY> newBedBstys = new List<HIS_BED_BSTY>();

                    List<HIS_BED_BSTY> copyBedBstys = DAOWorker.SqlDAO.GetSql<HIS_BED_BSTY>("SELECT * FROM HIS_BED_BSTY WHERE BED_ID = :param1", data.CopyBedId);
                    List<HIS_BED_BSTY> pasteBedBstys = DAOWorker.SqlDAO.GetSql<HIS_BED_BSTY>("SELECT * FROM HIS_BED_BSTY WHERE BED_ID = :param1", data.PasteBedId);
                    if (!IsNotNullOrEmpty(copyBedBstys))
                    {
                        HIS_BED bed = new HisBedGet(param).GetById(data.CopyBedId);
                        string name = bed != null ? bed.BED_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisBed_GiuongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyBedBstys");
                    }

                    foreach (HIS_BED_BSTY copyData in copyBedBstys)
                    {
                        HIS_BED_BSTY mestMaty = pasteBedBstys != null ? pasteBedBstys.FirstOrDefault(o => o.BED_SERVICE_TYPE_ID
                            == copyData.BED_SERVICE_TYPE_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_BED_BSTY();
                            mestMaty.BED_ID = data.PasteBedId;
                            mestMaty.BED_SERVICE_TYPE_ID = copyData.BED_SERVICE_TYPE_ID;
                            newBedBstys.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newBedBstys))
                    {
                        if (!DAOWorker.HisBedBstyDAO.CreateList(newBedBstys))
                        {
                            throw new Exception("Khong tao duoc HIS_BED_BSTY");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_BED_BSTY>();
                    if (IsNotNullOrEmpty(newBedBstys))
                    {
                        resultData.AddRange(newBedBstys);
                    }
                    if (IsNotNullOrEmpty(pasteBedBstys))
                    {
                        resultData.AddRange(pasteBedBstys);
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
