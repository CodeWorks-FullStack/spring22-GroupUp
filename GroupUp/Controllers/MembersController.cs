using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using GroupUp.Models;
using GroupUp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupUp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MembersController : ControllerBase
  {
    private readonly MembersService _ms;

    public MembersController(MembersService ms)
    {
      _ms = ms;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Member>> Create([FromBody] Member member)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        member.ProfileId = userInfo.Id;
        Member newMember = _ms.Create(member);
        return Ok(newMember);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ms.Delete(id, userInfo.Id);
        return Ok("Member deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}