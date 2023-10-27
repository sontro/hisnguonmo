
using System.Collections.Generic;
namespace ACS.MANAGER.Core.TokenSys.Authorize
{
    interface IAcsTokenAuthorize
    {
        ACS.SDO.AcsAuthorizeSDO Run();
    }
}
