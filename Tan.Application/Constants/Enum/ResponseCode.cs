using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tan.Application.Constants.Enum;

public enum ResponseCode
{
    NONE,
    SUCCESS,
    NULL_OR_EMPTY,
    ERROR,
    Unauthorized = 401,
    //account 1000 ~
    TokenExpired = 1000,
    //Exception 9000 ~
    ERROR_EXCEPTION = 9000,
}
