using System;
using GroupUp.Interfaces;

namespace GroupUp.Models
{
  public class Member : IRepoItem<int>
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ProfileId { get; set; }
    public int GroupId { get; set; }
  }
}