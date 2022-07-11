using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberObject
{

    public class Member
    {
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Member()
        {
        }

        public Member(int memberID, string memberName, string email, string password, string city, string country)
        {
            MemberID = memberID;
            MemberName = memberName;
            Email = email;
            Password = password;
            City = city;
            Country = country;
        }

        public override string? ToString()
        {
            return $"[Member ID :{MemberID}]" + "[Member name :{MemberName}]" + "[Email:{Email}]" + "[City:{City}]" + "[Country:{Country]";
        }
    }

}