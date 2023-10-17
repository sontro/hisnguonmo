using MOS.EFMODEL.DataModels;
using MOS.SDO;
namespace HIS.Desktop.Plugins.RegisterV2.Register
{
    interface IServiceRequestRegisterPatientProfile
    {
        HisPatientProfileSDO Run();
    }
}
