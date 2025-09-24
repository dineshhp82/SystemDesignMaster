using LibraryManagementSystem.Entities;

namespace LibraryManagementSystem.Repositories
{
    public class MemberRepository
    {
        private readonly List<Member> _members = new();
        public void Add(Member member) => _members.Add(member);
        public Member? GetById(Guid memberId) => _members.FirstOrDefault(m => m.MemberID == memberId);
    }
}
