using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityModule.DataAccess;


namespace SecurityModule.Provider
{
    [Serializable]
    public class UserProvider : BaseProvider
    {
        #region Proparties
        public int ID
        {
            get;
            set;
        }
        public string UserID
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }
        public string Designation
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string SecurityQuestion
        {
            get;
            set;
        }
        public string Answer
        {
            get;
            set;
        }
        public int UserGroupID
        {
            get;
            set;
        }
        public bool IsLocked
        {
            get;
            set;
        }
        public DateTime? LockedDate
        {
            get;
            set;
        }

        public int VatID
        {
            get;
            set;
        }
        public int WarehouseID
        {
            get;
            set;
        }
        public string WarehouseName
        {
            get;
            set;
        }
        public int StatusID
        {
            get;
            set;
        }
        public bool IsAdmin
        {
            get;
            set;
        }
        #endregion

        #region Constant
        //public const string IDField = "ID";
        //public const string UserIDField = "UserID";
        //public const string PasswordField = "Password";
        //public const string EmployeeIDField = "EmployeeID";
        //public const string EmailField = "Email";
        //public const string SecurityQuestionField = "SecurityQuestion";
        //public const string AnswerField = "Answer";
        //public const string UserGroupIDField = "UserGroupID";
        //public const string IsLockedField = "IsLocked";
        //public const string LockedDateField = "LockedDate";
        //public const string VatIDField = "VatID";
        //public const string WarehouseIDField = "WarehouseID";
        //public const string StatusIDField = "StatusID";
        #endregion

        #region Method
        private UserDataAccess dataAccess = new UserDataAccess();
        public bool Save()
        {
            if (this.UserID.Length == 0)
            {
                throw new Exception("Login Name never empty");
            }
            if (this.Password.Length == 0)
            {
                throw new Exception("Password never empty");
            }
            if (this.Email.Length == 0)
            {
                throw new Exception("Email never empty");
            }
            if (this.Answer.Length == 0)
            {
                throw new Exception("Answer never empty");
            }
            return dataAccess.Save(this);
        }
        public bool Update()
        {
            if (this.UserID.Length == 0)
            {
                throw new Exception("UserID never empty");
            }
            if (this.Password.Length == 0)
            {
                throw new Exception("Password never empty");
            }
            if (this.Email.Length == 0)
            {
                throw new Exception("Email never empty");
            }
            if (this.Answer.Length == 0)
            {
                throw new Exception("Answer never empty");
            }
            return dataAccess.Update(this);
        }
        public bool UpdatePassword()
        {
            return dataAccess.UpdatePassword(this);
        }

        public bool Delete()
        {
            if (UserID.Length == 0)
            {
                throw new Exception("UserID never empty");
            }
            if (Password.Length == 0)
            {
                throw new Exception("Password never empty");
            }
            if (this.Email.Length == 0)
            {
                throw new Exception("Email never empty");
            }
            if (this.Answer.Length == 0)
            {
                throw new Exception("Answer never empty");
            }
            else
            {
                return dataAccess.Delete(this);
            }
        }
        public DataSet GetAll()
        {
            return dataAccess.GetAll();
        }
        public UserProvider GetByUserNameAndPassword(string userID, string password)
            
        {
            return dataAccess.GetByUserNameAndPassword(userID, password);
        }
        public UserProvider GetByUserID(string userID)
        {
            return dataAccess.GetByUserID(userID);
        }
        public DataSet GetAllActive()
        {
            return dataAccess.GetAllActive();
        }
        public DataSet GetByID(int ID)
        {
            return dataAccess.GetByID(ID);
        }
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        #endregion
    }
}
