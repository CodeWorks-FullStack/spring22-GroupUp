using System;
using GroupUp.Interfaces;

namespace GroupUp.Models
{
  public class Group : IRepoItem<int>
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public bool IsPrivate { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }


}