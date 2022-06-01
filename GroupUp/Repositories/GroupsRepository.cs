using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GroupUp.Interfaces;
using GroupUp.Models;

namespace GroupUp.Repositories
{
  public class GroupsRepository : IRepository<Group, int>
  {
    private readonly IDbConnection _db;

    public GroupsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Group> Get()
    {
      string sql = @"
        SELECT 
            a.*,
            g.*
        FROM groups g
        JOIN accounts a ON g.creatorId = a.id";
      return _db.Query<Account, Group, Group>(sql, (a, g) =>
      {
        g.Creator = a;
        return g;
      }).ToList();
    }

    internal Group Get(int id)
    {
      string sql = @"
        SELECT 
            a.*,
            g.*
        FROM groups g
        JOIN accounts a ON g.creatorId = a.id
        WHERE g.id = @id";
      return _db.Query<Account, Group, Group>(sql, (a, g) =>
      {
        g.Creator = a;
        return g;
      }, new { id }).FirstOrDefault();
    }

    internal Group Create(Group group)
    {
      string sql = @"
            INSERT INTO groups 
            (name, description, image, isPrivate, creatorId)
            VALUES 
            (@Name, @Description, @Image, @IsPrivate, @CreatorId);
            SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(sql, group);
      group.Id = id;
      return group;
    }

    internal void Update(Group original)
    {
      string sql = @"
                UPDATE groups
                SET
                    name = @Name,
                    description = @Description,
                    image = @Image
                WHERE id = @Id;
            ";
      _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM groups WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    public List<Group> GetAll()
    {
      throw new System.NotImplementedException();
    }

    public Group GetById(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}