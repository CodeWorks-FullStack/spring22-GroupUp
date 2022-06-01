using System;
using System.Collections.Generic;
using GroupUp.Models;
using GroupUp.Repositories;

namespace GroupUp.Services
{
  public class GroupsService
  {
    private readonly GroupsRepository _repo;
    private readonly MembersRepository _membersRepo;

    public GroupsService(GroupsRepository repo, MembersRepository membersRepo)
    {
      _repo = repo;
      _membersRepo = membersRepo;
    }

    internal List<Group> Get()
    {
      List<Group> groups = _repo.Get();
      // NOTE Filter => FindAll
      groups = groups.FindAll(g => g.IsPrivate == false);
      return groups;
    }

    internal Group Get(int id)
    {
      Group found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Group not found");
      }
      return found;
    }

    internal Group Create(Group group)
    {
      Group newGroup = _repo.Create(group);
      // join the group
      // Create a new member and pass to membersRepo
      _membersRepo.Create(new Member()
      {
        GroupId = newGroup.Id,
        ProfileId = group.CreatorId
      });
      return newGroup;
    }

    internal Group Update(Group update)
    {
      Group original = Get(update.Id);
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.Image = update.Image ?? original.Image;

      _repo.Update(original);
      return original;
    }

    internal void Delete(int id, string userName)
    {
      Group group = Get(id);
      if (group.CreatorId != userName)
      {
        throw new Exception("You are not the creator of this group");
      }
      _repo.Delete(id);
    }
  }
}