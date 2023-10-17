using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockMaty
{
    class HisMediStockMatyCopyByMaty : BusinessBase
    {
        private List<HIS_MEDI_STOCK_MATY> recentMediStockMatys;

        internal HisMediStockMatyCopyByMaty()
            : base()
        {

        }

        internal HisMediStockMatyCopyByMaty(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisMestMatyCopyByMatySDO data, ref List<HIS_MEDI_STOCK_MATY> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyMaterialTypeId);
                valid = valid && IsGreaterThanZero(data.PasteMaterialTypeId);
                if (valid)
                {
                    List<HIS_MEDI_STOCK_MATY> newMestMatys = new List<HIS_MEDI_STOCK_MATY>();
                    List<HIS_MEDI_STOCK_MATY> oldMestMatys = new List<HIS_MEDI_STOCK_MATY>();
                    List<HIS_MEDI_STOCK_MATY> copyMestMatys = DAOWorker.SqlDAO.GetSql<HIS_MEDI_STOCK_MATY>("SELECT * FROM HIS_MEDI_STOCK_MATY WHERE MATERIAL_TYPE_ID = :param1", data.CopyMaterialTypeId);
                    List<HIS_MEDI_STOCK_MATY> pasteMestMatys = DAOWorker.SqlDAO.GetSql<HIS_MEDI_STOCK_MATY>("SELECT * FROM HIS_MEDI_STOCK_MATY WHERE MATERIAL_TYPE_ID = :param1", data.PasteMaterialTypeId);
                    if (!IsNotNullOrEmpty(copyMestMatys))
                    {
                        HIS_MATERIAL_TYPE materialType = Config.HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == data.CopyMaterialTypeId);
                        string name = materialType != null ? materialType.MATERIAL_TYPE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMaterialType_VatTuChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyMestMatys");
                    }

                    foreach (HIS_MEDI_STOCK_MATY copyData in copyMestMatys)
                    {
                        HIS_MEDI_STOCK_MATY mestMaty = pasteMestMatys != null ? pasteMestMatys.FirstOrDefault(o => o.MEDI_STOCK_ID == copyData.MEDI_STOCK_ID) : null;
                        if (mestMaty != null)
                        {
                            mestMaty.ALERT_MAX_IN_STOCK = copyData.ALERT_MAX_IN_STOCK;
                            mestMaty.ALERT_MIN_IN_STOCK = copyData.ALERT_MIN_IN_STOCK;
                            mestMaty.IS_GOODS_RESTRICT = copyData.IS_GOODS_RESTRICT;
                            mestMaty.IS_PREVENT_MAX = copyData.IS_PREVENT_MAX;
                            oldMestMatys.Add(mestMaty);
                        }
                        else
                        {
                            mestMaty = new HIS_MEDI_STOCK_MATY();
                            mestMaty.MATERIAL_TYPE_ID = data.PasteMaterialTypeId;
                            mestMaty.MEDI_STOCK_ID = copyData.MEDI_STOCK_ID;
                            mestMaty.ALERT_MAX_IN_STOCK = copyData.ALERT_MAX_IN_STOCK;
                            mestMaty.ALERT_MIN_IN_STOCK = copyData.ALERT_MIN_IN_STOCK;
                            mestMaty.IS_GOODS_RESTRICT = copyData.IS_GOODS_RESTRICT;
                            mestMaty.IS_PREVENT_MAX = copyData.IS_PREVENT_MAX;
                            newMestMatys.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        if (!DAOWorker.HisMediStockMatyDAO.CreateList(newMestMatys))
                        {
                            throw new Exception("Khong tao duoc HIS_MEDI_STOCK_MATY");
                        }
                        this.recentMediStockMatys = newMestMatys;
                    }

                    if (IsNotNullOrEmpty(oldMestMatys))
                    {
                        if (!DAOWorker.HisMediStockMatyDAO.UpdateList(oldMestMatys))
                        {
                            throw new Exception("Khong sua duoc HIS_MEDI_STOCK_MATY");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_MEDI_STOCK_MATY>();
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
                if (IsNotNullOrEmpty(this.recentMediStockMatys))
                {
                    if (!DAOWorker.HisMediStockMatyDAO.TruncateList(this.recentMediStockMatys))
                    {
                        Logging("Rollback HIS_MEDI_STOCK_MATY that bai. Kiem tra lai du lieu", LogType.Warn);
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
