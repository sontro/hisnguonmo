
using System.Collections.Generic;
namespace ACS.MANAGER.Core.TokenSys
{
    interface IAuthorizeDelegacy
    {
        ACS.SDO.AcsAuthorizeSDO Execute();
    }
}
