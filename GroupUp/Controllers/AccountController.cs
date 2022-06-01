using System;
using System.Threading.Tasks;
using GroupUp.Models;
using GroupUp.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GroupUp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly MembersService _membersService;

    public AccountController(AccountService accountService, MembersService membersService)
    {
      _accountService = accountService;
      _membersService = membersService;
    }

    [HttpGet]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("groups")]
    public async Task<ActionResult<List<GroupMemberViewModel>>> GetGroups()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<GroupMemberViewModel> groups = _membersService.GetByProfileId(userInfo.Id);
        return Ok(groups);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}