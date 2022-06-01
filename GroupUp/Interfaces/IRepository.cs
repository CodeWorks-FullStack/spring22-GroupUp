using System.Collections.Generic;

namespace GroupUp.Interfaces
{
  public interface IRepository<TItem, TId>
  {
    List<TItem> GetAll();
    TItem GetById(TId id);
  }
}