using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceMety
{
    class HisServiceMetyCopyByService : BusinessBase
    {
        private List<HIS_SERVICE_METY> recentServiceMetys;

        internal HisServiceMetyCopyByService()
            : base()
        {

        }

        internal HisServiceMetyCopyByService(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisServiceMetyCopyByServiceSDO data, ref List<HIS_SERVICE_METY> resultData)
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
                    List<HIS_SERVICE_METY> newServiceMetys = new List<HIS_SERVICE_METY>();
                    List<HIS_SERVICE_METY> oldServiceMetys = new List<HIS_SERVICE_METY>();
                    List<HIS_SERVICE_METY> copyServiceMetys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_METY>("SELECT * FROM HIS_SERVICE_METY WHERE SERVICE_ID = :param1", data.CopyServiceId);
                    List<HIS_SERVICE_METY> pasteServiceMetys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_METY>("SELECT * FROM HIS_SERVICE_METY WHERE SERVICE_ID = :param1", data.PasteServiceId);
                    if (!IsNotNullOrEmpty(copyServiceMetys))
                    {
                        V_HIS_SERVICE stock = Config.HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == data.CopyServiceId);
                        string name = stock != null ? stock.SERVICE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_DichVuChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyServiceMetys");
                    }

                    foreach (HIS_SERVICE_METY copyData in copyServiceMetys)
                    {
                        HIS_SERVICE_METY serviceMety = pasteServiceMetys != null ? pasteServiceMetys.FirstOrDefault(o => o.MEDICINE_TYPE_ID == copyData.MEDICINE_TYPE_ID) : null;
                        if (serviceMety != null)
                        {
                            serviceMety.EXPEND_AMOUNT = copyData.EXPEND_AMOUNT;
                            serviceMety.EXPEND_PRICE = copyData.EXPEND_PRICE;
                            oldServiceMetys.Add(serviceMety);
                        }
                        else
                        {
                            serviceMety = new HIS_SERVICE_METY();
                            serviceMety.SERVICE_ID = data.PasteServiceId;
                            serviceMety.MEDICINE_TYPE_ID = copyData.MEDICINE_TYPE_ID;
                            serviceMety.EXPEND_AMOUNT = copyData.EXPEND_AMOUNT;
                            serviceMety.EXPEND_PRICE = copyData.EXPEND_PRICE;
                            serviceMety.SERVICE_UNIT_ID = copyData.SERVICE_UNIT_ID;
                            newServiceMetys.Add(serviceMety);
                        }
                    }
                    if (IsNotNullOrEmpty(newServiceMetys))
                    {
                        if (!DAOWorker.HisServiceMetyDAO.CreateList(newServiceMetys))
                        {
                            throw new Exception("Khong tao duoc HIS_SERVICE_METY");
                        }
                        this.recentServiceMetys = newServiceMetys;
                    }

                    if (IsNotNullOrEmpty(oldServiceMetys))
                    {
                        if (!DAOWorker.HisServiceMetyDAO.UpdateList(oldServiceMetys))
                        {
                            throw new Exception("Khong sua duoc HIS_SERVICE_METY");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_SERVICE_METY>();
                    if (IsNotNullOrEmpty(newServiceMetys))
                    {
                        resultData.AddRange(newServiceMetys);
                    }
                    if (IsNotNullOrEmpty(pasteServiceMetys))
                    {
                        resultData.AddRange(pasteServiceMetys);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
                resultData = null;
            }
            return result;
        }

        private void RollbackData()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentServiceMetys))
                {
                    if (!DAOWorker.HisServiceMetyDAO.TruncateList(this.recentServiceMetys))
                    {
                        Logging("Rollback HIS_SERVICE_METY that bai. Kiem tra lai du lieu", LogType.Warn);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
