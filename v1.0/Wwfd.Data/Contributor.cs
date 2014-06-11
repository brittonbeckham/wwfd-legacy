using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;

namespace Wwfd.Data
{
    public enum ContributorRoles
    {
        Admin = 1,
        Approver,
        Modifier,
        Contributor,
        Researcher,
        None        
    }

    /// <summary>
    /// Summary description for Contributor
    /// </summary>
    public class Contributor
    {
        CoreDataObjects.ContributorsRow _dataRow;
        ContributorsTableAdapter _adpt;

        public Contributor(string username)
        {            
            _adpt = new ContributorsTableAdapter();
            _dataRow = _adpt.GetByEmail(username)[0];
        }

        public string FullName
        {
            get
            {
                return _dataRow.FullName;
            }
        }
        public Guid ContributorId
        {
            get
            {
                return _dataRow.ContributorID;
            }
        }

        public string FirstName
        {
            get
            {
                return _dataRow.FirstName;
            }
            set
            {
                _dataRow.FirstName = value;
            }
        }

        public string Password
        {
            get
            {
                return _dataRow.Password;
            }
            set
            {
                _dataRow.Password = value;
            }
        }

        public string LastName
        {
            get
            {
                return _dataRow.LastName;
            }
            set
            {
                _dataRow.LastName = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return _dataRow.Active;
            }
            set
            {
                _dataRow.Active = value;
            }
        }

        public ContributorRoles Role
        {
            get
            {
                return (ContributorRoles)_dataRow.RoleID;
            }
            set
            {
                _dataRow.RoleID = (byte)value;
            }
        }


        public string EmailAddress
        {
            get
            {
                return _dataRow.Email;
            }
            set
            {
                _dataRow.Email = value;
            }
        }

        public string Username
        {
            get
            {
                return _dataRow.Email;
            }
            set
            {
                _dataRow.Email = value;
            }
        }

        public void Save()
        {
            _adpt.Update(_dataRow);
        }
    }
}