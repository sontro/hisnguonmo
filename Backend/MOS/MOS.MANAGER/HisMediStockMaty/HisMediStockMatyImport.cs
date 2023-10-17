using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockMaty
{
    class HisMediStockMatyImport : BusinessBase
    {
        private HisMediStockMatyCreate hisMediStockMatyCreate = null;
        private HisMediStockMatyUpdate hisMediStockMatyUpdate = null;

        internal HisMediStockMatyImport()
            : base()
        {
            this.Init();
        }

        internal HisMediStockMatyImport(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMediStockMatyCreate = new HisMediStockMatyCreate(param);
            this.hisMediStockMatyUpdate = new HisMediStockMatyUpdate(param);
        }

        internal bool Run(List<HIS_MEDI_STOCK_MATY> listData, ref List<HIS_MEDI_STOCK_MATY> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockMatyCheck checker = new HisMediStockMatyCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.CheckDuplicate(listData);
                foreach (HIS_MEDI_STOCK_MATY data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    List<HIS_MEDI_STOCK_MATY> creates = new List<HIS_MEDI_STOCK_MATY>();
                    List<HIS_MEDI_STOCK_MATY> updates = new List<HIS_MEDI_STOCK_MATY>();
                    List<HIS_MEDI_STOCK_MATY> befores = new List<HIS_MEDI_STOCK_MATY>();

                    List<HIS_MEDI_STOCK_MATY> listOld = new HisMediStockMatyGet().GetByMediStockIds(listData.Select(s => s.MEDI_STOCK_ID).Distinct().ToList());

                    foreach (HIS_MEDI_STOCK_MATY data in listData)
                    {
                        HIS_MEDI_STOCK_MATY old = listOld != null ? listOld.FirstOrDefault(o => o.MEDI_STOCK_ID == data.MEDI_STOCK_ID && o.MATERIAL_TYPE_ID == data.MATERIAL_TYPE_ID) : null;
                        if (old != null)
                        {
                            data.ID = old.ID;
                            updates.Add(data);
                            befores.Add(old);
                        }
                        else
                        {
                            creates.Add(data);
                        }
                    }

                    if (IsNotNullOrEmpty(creates) && !this.hisMediStockMatyCreate.CreateList(creates))
                    {
                        throw new Exception("hisMediStockMatyCreate. Ket thuc nghiep vu");
                    }


                    if (IsNotNullOrEmpty(updates) && !this.hisMediStockMatyUpdate.UpdateList(creates, befores))
                    {
                        throw new Exception("hisMediStockMatyUpdate. Ket thuc nghiep vu");
                    }

                    result = true;
                    resultData = listData;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.hisMediStockMatyUpdate.RollbackData();
                this.hisMediStockMatyCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
