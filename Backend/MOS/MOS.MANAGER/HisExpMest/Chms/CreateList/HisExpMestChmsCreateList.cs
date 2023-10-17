using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.CreateList
{
    class HisExpMestChmsCreateList : BusinessBase
    {

        private ExpMestProcessor expMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private BloodProcessor bloodProcessor;

        internal HisExpMestChmsCreateList()
            : base()
        {
            this.Init();
        }

        internal HisExpMestChmsCreateList(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
        }

        internal bool Run(HisExpMestChmsListSDO data,ref  List<HisExpMestResultSDO> resultData)
        {
            bool result = false;
            try
            {
                HisExpMestCheck commomChecker = new HisExpMestCheck(param);
                HisExpMestChmsCreateListCheck checker = new HisExpMestChmsCreateListCheck(param);
                bool valid = true;
                valid = valid && checker.ValidData(data);
                valid = valid && checker.VerifyAllow(data);
                valid = valid && commomChecker.HasToExpMestReason(data.ExpMestReasonId);

                if (valid)
                {
                    Dictionary<HIS_EXP_MEST, ExpMestChmsDetailSDO> dicExpMest = new Dictionary<HIS_EXP_MEST, ExpMestChmsDetailSDO>();
                    List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs = null;
                    List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs = null;
                    List<HIS_EXP_MEST_BLTY_REQ> expMestBltyReqs = null;
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                    List<string> sqls = new List<string>();

                    if (!this.expMestProcessor.Run(data, ref dicExpMest))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }


                    //Tao exp_mest_maty_req
                    if (!this.materialProcessor.Run(dicExpMest, ref expMestMatyReqs, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("materialProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_mety_req
                    if (!this.medicineProcessor.Run(dicExpMest, ref expMestMetyReqs, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("medicineProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_blty_req
                    if (!this.bloodProcessor.Run(dicExpMest, ref expMestBltyReqs))
                    {
                        throw new Exception("bloodProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(dicExpMest, expMestMatyReqs, expMestMetyReqs, expMestBltyReqs, expMestMedicines, expMestMaterials, ref resultData);
                    result = true;

                    foreach (var dic in dicExpMest)
                    {
                        new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(dic.Key.EXP_MEST_CODE).Run();
                    }

                    this.ProcessAuto(resultData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void PassResult(Dictionary<HIS_EXP_MEST, ExpMestChmsDetailSDO> dicExpMest, List<HIS_EXP_MEST_MATY_REQ> expMatyReqs, List<HIS_EXP_MEST_METY_REQ> expMetyReqs, List<HIS_EXP_MEST_BLTY_REQ> expBltyReqs, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, ref List<HisExpMestResultSDO> resultData)
        {
            resultData = new List<HisExpMestResultSDO>();
            foreach (var dic in dicExpMest)
            {
                HisExpMestResultSDO sdo = new HisExpMestResultSDO();
                sdo.ExpMest = dic.Key;
                sdo.ExpBltyReqs = expBltyReqs != null ? expBltyReqs.Where(o => o.EXP_MEST_ID == dic.Key.ID).ToList() : null;
                sdo.ExpMatyReqs = expMatyReqs != null ? expMatyReqs.Where(o => o.EXP_MEST_ID == dic.Key.ID).ToList() : null;
                sdo.ExpMetyReqs = expMetyReqs != null ? expMetyReqs.Where(o => o.EXP_MEST_ID == dic.Key.ID).ToList() : null;
                sdo.ExpMaterials = materials != null ? materials.Where(o => o.EXP_MEST_ID == dic.Key.ID).ToList() : null;
                sdo.ExpMedicines = medicines != null ? medicines.Where(o => o.EXP_MEST_ID == dic.Key.ID).ToList() : null;
                resultData.Add(sdo);
            }
        }

        private void ProcessAuto(List<HisExpMestResultSDO> resultData)
        {
            try
            {
                foreach (var resultSdo in resultData)
                {
                    if (IsProcessAuto(resultSdo))
                    {
                        HisExpMestResultSDO autoSDO = null;
                        if (new HisExpMestAutoProcess().Run(resultSdo.ExpMest, ref autoSDO))
                        {
                            resultSdo.ExpMest = autoSDO.ExpMest;
                            resultSdo.ExpBloods = autoSDO.ExpBloods;
                            resultSdo.ExpBltyReqs = autoSDO.ExpBltyReqs;
                            resultSdo.ExpMaterials = autoSDO.ExpMaterials;
                            resultSdo.ExpMatyReqs = autoSDO.ExpMatyReqs;
                            resultSdo.ExpMedicines = autoSDO.ExpMedicines;
                            resultSdo.ExpMetyReqs = autoSDO.ExpMetyReqs;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private bool IsProcessAuto(HisExpMestResultSDO data)
        {
            try
            {
                if (!IsNotNullOrEmpty(data.ExpMatyReqs)) return true;
                if (IsNotNullOrEmpty(data.ExpBltyReqs)) return false;
                if (HisMaterialTypeCFG.DATA.Any(o => o.IS_REUSABLE == Constant.IS_TRUE && data.ExpMatyReqs.Any(a => a.MATERIAL_TYPE_ID == o.ID)))
                {
                    LogSystem.Warn("Co Yeu cau vat tu tai su dung. Khong xu ly tu dong duyet. ExpMestCode: " + data.ExpMest.EXP_MEST_CODE);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }
    }
}
