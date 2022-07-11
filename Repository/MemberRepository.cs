using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemberObject;
using System.Threading.Tasks;
using DataAccess.DatabaseAccess;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(int memberId) => MemberDAO.Instance.Remove(memberId);


        public Member GetMemberByID(int memberId) => MemberDAO.Instance.GetMemberByID(memberId);


        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.GetMemberList();



        public void InsertMember(Member member) => MemberDAO.Instance.AddNew(member);


        public void UpdateMember(Member member) => MemberDAO.Instance.Update(member);
        public void Login (string username, string password) => MemberDAO.Instance.Login(username, password);   
    }
}
