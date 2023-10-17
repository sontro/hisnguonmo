using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System.Collections.Generic;
namespace HIS.Desktop.Plugins.RegisterV3.Register
{
    interface IServiceRequestRegisterExam
    {
        HisServiceReqExamRegisterResultSDO Run();
    }
}
