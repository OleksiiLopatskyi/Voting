using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Entities;

namespace Voting.BAL.Attributes
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private UserRoles _roleEnum;
        private string _authScheme = "Bearer";
        public MyAuthorizeAttribute()
        {
            AuthenticationSchemes = _authScheme;
        }
        public UserRoles Role 
        {
            get
            {
                return _roleEnum;
            }
            set
            {
                _roleEnum = value;
                Roles = value.ToString();
            }
        }

    }
}
