using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisReportTypeCat;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceRetyCat
{
    class HisServiceRetyCatCopyByRetyCat : BusinessBase
    {
        internal HisServiceRetyCatCopyByRetyCat()
            : base()
        {

        }

        internal HisServiceRetyCatCopyByRetyCat(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisServiceRetyCatCopyByRetyCatSDO data, ref List<HIS_SERVICE_RETY_CAT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyReportTypeCatId);
                valid = valid && IsGreaterThanZero(data.PasteReportTypeCatId);
                if (valid)
                {
                    List<HIS_SERVICE_RETY_CAT> newMestMatys = new List<HIS_SERVICE_RETY_CAT>();

                    List<HIS_SERVICE_RETY_CAT> copyMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_RETY_CAT>("SELECT * FROM HIS_SERVICE_RETY_CAT WHERE REPORT_TYPE_CAT_ID = :param1", data.CopyReportTypeCatId);
                    List<HIS_SERVICE_RETY_CAT> pasteMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_RETY_CAT>("SELECT * FROM HIS_SERVICE_RETY_CAT WHERE REPORT_TYPE_CAT_ID = :param1", data.PasteReportTypeCatId);
                    if (!IsNotNullOrEmpty(copyMestMatys))
                    {
                        HIS_REPORT_TYPE_CAT room = new HisReportTypeCatGet().GetById(data.CopyReportTypeCatId);
                        string name = room != null ? room.REPORT_TYPE_CODE : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisReportTypeCat_MauBaoCaoChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyServiceRetyCats");
                    }

                    foreach (HIS_SERVICE_RETY_CAT copyData in copyMestMatys)
                    {
                        HIS_SERVICE_RETY_CAT mestMaty = pasteMestMatys != null ? pasteMestMatys.FirstOrDefault(o => o.SERVICE_ID
                            == copyData.SERVICE_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_SERVICE_RETY_CAT();
                            mestMaty.REPORT_TYPE_CAT_ID = data.PasteReportTypeCatId;
                            mestMaty.SERVICE_ID = copyData.SERVICE_ID;
                            newMestMatys.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        if (!DAOWorker.HisServiceRetyCatDAO.CreateList(newMestMatys))
                        {
                            throw new Exception("Khong tao duoc HIS_SERVICE_RETY_CAT");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_SERVICE_RETY_CAT>();
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
