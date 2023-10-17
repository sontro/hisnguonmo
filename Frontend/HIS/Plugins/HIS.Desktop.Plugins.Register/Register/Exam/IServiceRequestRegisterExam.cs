using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System.Collections.Generic;
namespace HIS.Desktop.Plugins.Register.Register
{
    interface IServiceRequestRegisterExam
    {
        HisServiceReqExamRegisterResultSDO Run();
    }
}
