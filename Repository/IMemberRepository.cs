using MemberObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member GetMemberByID(int memberId);
        void InsertMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int memberId);
        void Login (string email, string password);
    }
}
