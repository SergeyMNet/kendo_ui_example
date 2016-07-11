using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp2.Models
{
    public enum EnumErrorCode
    {
        Ok = 0,
        InvalidParameter = 101,
                
        ErrorOnInsert = 102,
        ErrorOnUpdate = 103,        
        ErrorOnDelete = 104,
        AlredyExist = 105
    }
}