using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModule
{
    public class DBConstants
    {
        public enum DataLoadingOption
        {
            LoadAll = 1,
            LoadWithTypeID = 3,
            LoadWithFilterExpression = 2,
            LoadWithSpecialFilter = 7,
            LoadWithFilterExpressionByBOE = 4,
            LoadForNewBOE = 3,
            LoadWithFilterExpressionForRetRej = 3,
            LoadByDivisionwise = 1,
            LoadWithSupplierProduct = 4
        }
        public enum DataModificationOption
        {
            Insert = 1,
            Update = 2,
            Delete = 3,

        }
    }
}
