using nhl_stenden_cafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace nhl_stenden_cafe.Pages
{
    public static class StaticUserRepository
    {
        static List<CafeUser> _users = new List<CafeUser>();

        public enum AddUserResult
        {
            UserNameIsNotUnique,
            GuidIsNotUnique, //zou nooit moeten voorkomen
            Success
        }

        public static AddUserResult AddUser(CafeUser cafeUser)
        {
            if (cafeUser.UniqueGuid == default(Guid))
            {
                cafeUser.UniqueGuid = new Guid();
            }
            
            if (_users.Find(x => x.UniqueGuid == cafeUser.UniqueGuid) != null)
            {
                return AddUserResult.GuidIsNotUnique;
            }

            if (_users.Find(x =>
                    x.UserName.Equals(cafeUser.UserName, StringComparison.InvariantCultureIgnoreCase)) != null)
            {
                return AddUserResult.UserNameIsNotUnique;
            }

            
            
            _users.Add(cafeUser);
            return AddUserResult.Success;
        }

        public static CafeUser GetUserByID(Guid guid)
        {
            return _users.SingleOrDefault(x => x.UniqueGuid == guid);
        }

        public static CafeUser GetUserByUserName(string userName)
        {
            return _users.SingleOrDefault(x => x.UserName == userName);
        }
        public static bool GetUserExistanceUserName(string userName)
        {
            return _users.Any(x => x.UserName.ToLower() == userName.ToLower());
        }
    }
}
