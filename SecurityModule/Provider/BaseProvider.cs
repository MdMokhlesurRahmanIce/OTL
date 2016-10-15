using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityModule.Provider
{
    [Serializable]
    public class BaseProvider
    {
        public int EntryUserID
        {
            get;
            set;
        }
        public DateTime EntryDate
        {
            get;
            set;
        }
        public int UpdateUserID
        {
            get;
            set;
        }
        public DateTime UpdateDate
        {
            get;
            set;
        }
        public Int64 Ts
        {
            get;
            set;
        }
    }
}
