using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SetupModule.DataAccess;


namespace SetupModule.Provider
{
    public class SupplierProvider:BaseProvider
    {
        #region Properties
        private SuppliersDataAccess dataAccess = new SuppliersDataAccess();
        public int ID
        {
            get;
            set;
        }
        public string TINNumber
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int StatusID
        {
            get;
            set;
        }
        public string ContactName
        {
            get;
            set;
        }
        public int TypeID
        {
            get;
            set;
        }
        public String VatRegNo
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string CountryName
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }
        public string Mobile
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Note
        {
            get;
            set;
        }
        public int TarriffID
        {
            get;
            set;
        }
        #endregion

        #region
        public const string IDField = "ID";
        public const string NameField = "Name";
        public const string StatusIDField = "StatusID";
        public const string ContactNameField = "ContactName";
        public const string TypeIDField = "TypeID";
        public const string VatRegNoField = "VatRegNo";
        public const string AddressField = "Address";
        public const string CountryIDField = "CountryID";
        public const string PhoneField = "Phone";
        public const string MobileField = "Mobile";
        public const string FaxField = "Fax";
        public const string NoteField = "Note";
        public const string TarriffIDField = "TarriffID";
        #endregion

        #region Methods
        public bool Save()
        {
            if (this.Name.Length == 0)
            {
                throw new Exception("Supplier  Name never empty");
            }
            if (this.ContactName.Length == 0)
            {
                throw new Exception("Contact Name never empty");
            }
            if (this.Address.Length == 0)
            {
                throw new Exception("Address never empty");
            }
            if (this.Mobile.Length == 0)
            {
                throw new Exception("Mobile never empty");
            }
            if (this.TypeID == 0)
            {
                throw new Exception("Supplier Type never empty");
            }
            if (this.StatusID == 0)
            {
                throw new Exception("Status never empty");
            }          
            return dataAccess.Save(this);
        }
        public bool Update()
        {
            //if (this.Code.Length == 0)
            //{
            //    throw new Exception("Code never empty");
            //}
            if (this.Name.Length == 0)
            {
                throw new Exception("Name never empty");
            }
            if (this.ContactName.Length == 0)
            {
                throw new Exception("ContactName never empty");
            }
            //if (this.VatRegNo.Length == 0)
            //{
            //    throw new Exception("VatRegNo never empty");
            //}
            if (this.Address.Length == 0)
            {
                throw new Exception("Address never empty");
            }
            if (this.StatusID == 0)
            {
                throw new Exception("Status never empty");
            }

            //if (this.Phone.Length == 0)
            //{
            //    throw new Exception("Phone never empty");
            //}
            if (this.Mobile.Length == 0)
            {
                throw new Exception("Mobile never empty");
            }
            //if (this.Fax.Length == 0)
            //{
            //    throw new Exception("Fax never empty");
            //}
            //if (this.Note.Length == 0)
            //{
            //    throw new Exception("Note never empty");
            //}
            return dataAccess.Update(this);

        }
        public bool Delete()
        {
            return dataAccess.Delete(this);
        }

        public DataSet GetAll()
        {
            return dataAccess.GetAll();
        }
        public DataSet GetAllActive()
        {
            return dataAccess.GetAllActive();
        }
        public DataTable GetByID(int ID)
        {
            return dataAccess.GetByID(ID);
        }
        //public DataTable GetSupplier(int ID)
        //{
        //    return dataAccess.GetSupplier(ID);
        //}
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }        
        public DataSet GetSupplierType()
        {
            return dataAccess.GetSupplierType();
        }
        public DataTable GetSupplierByTypeID(int supplierTypeID)
        {
            return dataAccess.GetSupplierByTypeID(supplierTypeID);
        }
        #endregion
    }
}
