using Inventec.Core;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.MANAGER.Core.SarUserReportType.Copy
{
    class SarUserReportTypeCopyByReportTypeBehavior : BeanObjectBase, ISarUserReportTypeCopy
    {
        private SDO.SarUserReportTypeCopyByReportTypeSDO entity;

        internal SarUserReportTypeCopyByReportTypeBehavior(CommonParam param, SDO.SarUserReportTypeCopyByReportTypeSDO data)
            : base(param)
        {
            entity = data;
        }

        object ISarUserReportTypeCopy.Run()
        {
            List<SAR_USER_REPORT_TYPE> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(entity);
                valid = valid && IsGreaterThanZero(entity.CopyReportTypeId);
                valid = valid && IsGreaterThanZero(entity.PasteReportTypeId);
                if (valid)
                {
                    List<SAR_USER_REPORT_TYPE> newDatas = new List<SAR_USER_REPORT_TYPE>();

                    List<SAR_USER_REPORT_TYPE> copyDatas = Base.DAOWorker.SqlDAO.GetSql<SAR_USER_REPORT_TYPE>("SELECT * FROM SAR_USER_REPORT_TYPE WHERE REPORT_TYPE_ID = :param1", entity.CopyReportTypeId);
                    List<SAR_USER_REPORT_TYPE> pasteDatas = Base.DAOWorker.SqlDAO.GetSql<SAR_USER_REPORT_TYPE>("SELECT * FROM SAR_USER_REPORT_TYPE WHERE REPORT_TYPE_ID = :param1", entity.PasteReportTypeId);

                    if (!IsNotNullOrEmpty(copyDatas))
                    {
                        SAR_REPORT_TYPE report = new Manager.SarReportTypeManager(param).Get<SAR_REPORT_TYPE>(entity.CopyReportTypeId);
                        string name = report != null ? report.REPORT_TYPE_CODE : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.SarReportType_BaoCaoChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyServiceRooms");
                    }

                    foreach (SAR_USER_REPORT_TYPE copyData in copyDatas)
                    {
                        SAR_USER_REPORT_TYPE data = pasteDatas != null ? pasteDatas.FirstOrDefault(o => o.LOGINNAME == copyData.LOGINNAME) : null;
                        if (data != null)
                        {
                            continue;
                        }
                        else
                        {
                            data = new SAR_USER_REPORT_TYPE();
                            data.REPORT_TYPE_ID = entity.PasteReportTypeId;
                            data.LOGINNAME = copyData.LOGINNAME;
                            newDatas.Add(data);
                        }
                    }
                    if (IsNotNullOrEmpty(newDatas))
                    {
                        if (!Base.DAOWorker.SarUserReportTypeDAO.CreateList(newDatas))
                        {
                            throw new Exception("Khong tao duoc SAR_USER_REPORT_TYPE");
                        }
                    }

                    result = new List<SAR_USER_REPORT_TYPE>();
                    if (IsNotNullOrEmpty(newDatas))
                    {
                        result.AddRange(newDatas);
                    }
                    if (IsNotNullOrEmpty(pasteDatas))
                    {
                        result.AddRange(pasteDatas);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
