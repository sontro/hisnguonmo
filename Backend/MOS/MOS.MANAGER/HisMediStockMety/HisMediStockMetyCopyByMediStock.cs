using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockMety
{
    class HisMediStockMetyCopyByMediStock : BusinessBase
    {
        private List<HIS_MEDI_STOCK_METY> recentMediStockMetys;

        internal HisMediStockMetyCopyByMediStock()
            : base()
        {

        }

        internal HisMediStockMetyCopyByMediStock(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisMestMetyCopyByMediStockSDO data, ref List<HIS_MEDI_STOCK_METY> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyMediStockId);
                valid = valid && IsGreaterThanZero(data.PasteMediStockId);
                if (valid)
                {
                    List<HIS_MEDI_STOCK_METY> newMestMetys = new List<HIS_MEDI_STOCK_METY>();
                    List<HIS_MEDI_STOCK_METY> oldMestMetys = new List<HIS_MEDI_STOCK_METY>();
                    List<HIS_MEDI_STOCK_METY> copyMestMetys = DAOWorker.SqlDAO.GetSql<HIS_MEDI_STOCK_METY>("SELECT * FROM HIS_MEDI_STOCK_METY WHERE MEDI_STOCK_ID = :param1", data.CopyMediStockId);
                    List<HIS_MEDI_STOCK_METY> pasteMestMetys = DAOWorker.SqlDAO.GetSql<HIS_MEDI_STOCK_METY>("SELECT * FROM HIS_MEDI_STOCK_METY WHERE MEDI_STOCK_ID = :param1", data.PasteMediStockId);
                    if (!IsNotNullOrEmpty(copyMestMetys))
                    {
                        V_HIS_MEDI_STOCK stock = Config.HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == data.CopyMediStockId);
                        string name = stock != null ? stock.MEDI_STOCK_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStock_KhoChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyMestMetys");
                    }

                    foreach (HIS_MEDI_STOCK_METY copyData in copyMestMetys)
                    {
                        HIS_MEDI_STOCK_METY mestMety = pasteMestMetys != null ? pasteMestMetys.FirstOrDefault(o => o.MEDICINE_TYPE_ID == copyData.MEDICINE_TYPE_ID) : null;
                        if (mestMety != null)
                        {
                            mestMety.ALERT_MAX_IN_STOCK = copyData.ALERT_MAX_IN_STOCK;
                            mestMety.ALERT_MIN_IN_STOCK = copyData.ALERT_MIN_IN_STOCK;
                            mestMety.IS_GOODS_RESTRICT = copyData.IS_GOODS_RESTRICT;
                            mestMety.IS_PREVENT_MAX = copyData.IS_PREVENT_MAX;
                            oldMestMetys.Add(mestMety);
                        }
                        else
                        {
                            mestMety = new HIS_MEDI_STOCK_METY();
                            mestMety.MEDI_STOCK_ID = data.PasteMediStockId;
                            mestMety.MEDICINE_TYPE_ID = copyData.MEDICINE_TYPE_ID;
                            mestMety.ALERT_MAX_IN_STOCK = copyData.ALERT_MAX_IN_STOCK;
                            mestMety.ALERT_MIN_IN_STOCK = copyData.ALERT_MIN_IN_STOCK;
                            mestMety.IS_GOODS_RESTRICT = copyData.IS_GOODS_RESTRICT;
                            mestMety.IS_PREVENT_MAX = copyData.IS_PREVENT_MAX;
                            newMestMetys.Add(mestMety);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestMetys))
                    {
                        if (!DAOWorker.HisMediStockMetyDAO.CreateList(newMestMetys))
                        {
                            throw new Exception("Khong tao duoc HIS_MEDI_STOCK_METY");
                        }
                        this.recentMediStockMetys = newMestMetys;
                    }

                    if (IsNotNullOrEmpty(oldMestMetys))
                    {
                        if (!DAOWorker.HisMediStockMetyDAO.UpdateList(oldMestMetys))
                        {
                            throw new Exception("Khong sua duoc HIS_MEDI_STOCK_METY");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_MEDI_STOCK_METY>();
                    if (IsNotNullOrEmpty(newMestMetys))
                    {
                        resultData.AddRange(newMestMetys);
                    }
                    if (IsNotNullOrEmpty(pasteMestMetys))
                    {
                        resultData.AddRange(pasteMestMetys);
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
                if (IsNotNullOrEmpty(this.recentMediStockMetys))
                {
                    if (!DAOWorker.HisMediStockMetyDAO.TruncateList(this.recentMediStockMetys))
                    {
                        Logging("Rollback HIS_MEDI_STOCK_METY that bai. Kiem tra lai du lieu", LogType.Warn);
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
