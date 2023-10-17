using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceRetyCat
{
    class HisServiceRetyCatCopyByService : BusinessBase
    {
        internal HisServiceRetyCatCopyByService()
            : base()
        {

        }

        internal HisServiceRetyCatCopyByService(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisServiceRetyCatCopyByServiceSDO data, ref List<HIS_SERVICE_RETY_CAT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyServiceId);
                valid = valid && IsGreaterThanZero(data.PasteServiceId);
                if (valid)
                {
                    List<HIS_SERVICE_RETY_CAT> newMestMatys = new List<HIS_SERVICE_RETY_CAT>();

                    List<HIS_SERVICE_RETY_CAT> copyMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_RETY_CAT>("SELECT * FROM HIS_SERVICE_RETY_CAT WHERE SERVICE_ID = :param1", data.CopyServiceId);
                    List<HIS_SERVICE_RETY_CAT> pasteMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_RETY_CAT>("SELECT * FROM HIS_SERVICE_RETY_CAT WHERE SERVICE_ID = :param1", data.PasteServiceId);
                    if (!IsNotNullOrEmpty(copyMestMatys))
                    {
                        V_HIS_SERVICE stock = Config.HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == data.CopyServiceId);
                        string name = stock != null ? stock.SERVICE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_DichVuChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyServiceRetyCats");
                    }

                    foreach (HIS_SERVICE_RETY_CAT copyData in copyMestMatys)
                    {
                        HIS_SERVICE_RETY_CAT mestMaty = pasteMestMatys != null ? pasteMestMatys.FirstOrDefault(o => o.REPORT_TYPE_CAT_ID
                            == copyData.REPORT_TYPE_CAT_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_SERVICE_RETY_CAT();
                            mestMaty.SERVICE_ID = data.PasteServiceId;
                            mestMaty.REPORT_TYPE_CAT_ID = copyData.REPORT_TYPE_CAT_ID;
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
