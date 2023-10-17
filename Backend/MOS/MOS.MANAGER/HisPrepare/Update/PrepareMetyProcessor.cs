using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Update
{
    class PrepareMetyProcessor : BusinessBase
    {
        private HisPrepareMetyCreate hisPrepareMetyCreate;
        private HisPrepareMetyUpdate hisPrepareMetyUpdate;

        internal PrepareMetyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMetyCreate = new HisPrepareMetyCreate(param);
            this.hisPrepareMetyUpdate = new HisPrepareMetyUpdate(param);
        }

        internal bool Run(HIS_PREPARE hisPrepare, List<HIS_PREPARE_METY> prepareMetys, ref List<HIS_PREPARE_METY> medicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_PREPARE_METY> olds = new HisPrepareMetyGet().GetByPrepareId(hisPrepare.ID);
                if (IsNotNullOrEmpty(prepareMetys) || IsNotNullOrEmpty(olds))
                {
                    List<HIS_PREPARE_METY> createList = new List<HIS_PREPARE_METY>();
                    List<HIS_PREPARE_METY> updateList = new List<HIS_PREPARE_METY>();
                    List<HIS_PREPARE_METY> beforeList = new List<HIS_PREPARE_METY>();
                    List<HIS_PREPARE_METY> deleteList = new List<HIS_PREPARE_METY>();
                    if (IsNotNullOrEmpty(prepareMetys))
                    {
                        Mapper.CreateMap<HIS_PREPARE_METY, HIS_PREPARE_METY>();
                        foreach (HIS_PREPARE_METY item in prepareMetys)
                        {
                            HIS_PREPARE_METY old = olds != null ? olds.FirstOrDefault(o => o.MEDICINE_TYPE_ID == item.MEDICINE_TYPE_ID) : null;
                            if (old != null)
                            {
                                HIS_PREPARE_METY before = Mapper.Map<HIS_PREPARE_METY>(old);
                                old.REQ_AMOUNT = item.REQ_AMOUNT;
                                old.PREPARE_ID = hisPrepare.ID;
                                old.TDL_TREATMENT_ID = hisPrepare.TREATMENT_ID;
                                updateList.Add(old);
                                beforeList.Add(before);
                            }
                            else
                            {
                                item.PREPARE_ID = hisPrepare.ID;
                                item.APPROVAL_AMOUNT = null;
                                item.TDL_TREATMENT_ID = hisPrepare.TREATMENT_ID;
                                createList.Add(item);
                            }
                        }
                    }

                    deleteList = olds != null ? olds.Where(o => !updateList.Exists(e => e.ID == o.ID)).ToList() : null;

                    if (IsNotNullOrEmpty(updateList))
                    {
                        if (!this.hisPrepareMetyUpdate.UpdateList(updateList, beforeList))
                        {
                            throw new Exception("hisPrepareMetyUpdate. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(createList))
                    {
                        if (!this.hisPrepareMetyCreate.CreateList(createList))
                        {
                            throw new Exception("hisPrepareMetyCreate. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(deleteList))
                    {
                        string sql = DAOWorker.SqlDAO.AddInClause(deleteList.Select(s => s.ID).ToList(), "DELETE HIS_PREPARE_METY WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(sql);
                    }

                    updateList.AddRange(createList);
                    if (IsNotNullOrEmpty(updateList))
                    {
                        medicines = updateList;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisPrepareMetyCreate.RollbackData();
                this.hisPrepareMetyUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
