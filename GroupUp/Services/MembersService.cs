using System;
using System.Collections.Generic;
using GroupUp.Models;
using GroupUp.Repositories;

namespace GroupUp.Services
{
  public class MembersService
  {
    private readonly MembersRepository _repo;

    public MembersService(MembersRepository repo)
    {
      _repo = repo;
    }

    internal Member Create(Member member)
    {
      return _repo.Create(member);
    }

    internal Member GetById(int id)
    {
      Member found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Member not found");
      }
      return found;
    }

    internal void Delete(int id, string userId)
    {
      Member memberToDelete = GetById(id);
      if (memberToDelete.ProfileId != userId)
      {
        throw new Exception("You are not authorized to delete this member");
      }
      _repo.Delete(id);
    }

    internal List<GroupMemberViewModel> GetByProfileId(string id)
    {
      List<GroupMemberViewModel> members = _repo.GetByProfileId(id);
      return members;
    }

    internal List<MemberProfileViewModel> GetMembersByGroupId(int id)
    {
      List<MemberProfileViewModel> members = _repo.GetMembersByGroupId(id);
      return members;
    }
  }
}