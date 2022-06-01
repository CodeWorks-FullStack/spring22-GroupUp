using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GroupUp.Models;

namespace GroupUp.Repositories
{
  public class MembersRepository
  {
    private readonly IDbConnection _db;

    public MembersRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Member Create(Member member)
    {
      string sql = @"
      INSERT INTO members 
      (profileId, groupId) 
      VALUES 
      (@ProfileId, @GroupId); 
      SELECT LAST_INSERT_ID();";
      member.Id = _db.ExecuteScalar<int>(sql, member);
      return member;
    }

    internal Member GetById(int id)
    {
      string sql = "SELECT * FROM members WHERE id = @id";
      return _db.QueryFirstOrDefault<Member>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM members WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    internal List<GroupMemberViewModel> GetByProfileId(string id)
    {
      // Many to many get with populate
      string sql = @"
        SELECT
            a.*,
            g.*,
            m.id AS memberId
        FROM members m
        JOIN groups g ON m.groupId = g.id
        JOIN accounts a ON g.creatorId = a.id
        WHERE m.profileId = @id
      ";
      return _db.Query<Account, GroupMemberViewModel, GroupMemberViewModel>(sql, (a, g) =>
      {
        g.Creator = a;
        return g;
      }, new { id }).ToList();
    }

    internal Member getMembership(int id, string userId)
    {
      string sql = @"
      SELECT * FROM members
      WHERE groupId = @id AND profileId = @userId
      ";
      return _db.QueryFirstOrDefault<Member>(sql, new { id, userId });
    }

    internal List<MemberProfileViewModel> GetMembersByGroupId(int id)
    {
      // Many to many get without populate
      string sql = @"
            SELECT
                a.*,
                m.id AS memberId
            FROM members m
            JOIN accounts a ON m.profileId = a.id
            WHERE m.groupId = @id
        ";
      return _db.Query<MemberProfileViewModel>(sql, new { id }).ToList();
    }
  }
}
