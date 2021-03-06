using System;
using System.Collections.Generic;
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
  public class GroupsController : ControllerBase
  {
    private readonly GroupsService _gs;
    private readonly MembersService _ms;

    public GroupsController(GroupsService gs, MembersService ms)
    {
      _gs = gs;
      _ms = ms;
    }

    [HttpGet]
    public ActionResult<List<Group>> Get()
    {
      try
      {
        List<Group> groups = _gs.Get();
        return Ok(groups);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // REVIEW HINT HINT WINK WINK NUDGE NUDGE
    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> Get(int id)
    {
      try
      {
        // May not be logged in due to no [Authorize] will return null
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Group group = _gs.Get(id, userInfo?.Id);
        return Ok(group);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/members")]
    public ActionResult<List<MemberProfileViewModel>> GetMembers(int id)
    {
      try
      {
        List<MemberProfileViewModel> members = _ms.GetMembersByGroupId(id);
        return Ok(members);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Group>> Post([FromBody] Group group)
    {
      try
      {
        Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
        group.CreatorId = profile.Id;
        Group newGroup = _gs.Create(group);
        newGroup.Creator = profile;
        return Created($"/api/groups/{newGroup.Id}", newGroup);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Group>> Put(int id, [FromBody] Group group)
    {
      try
      {
        Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
        group.CreatorId = profile.Id;
        group.Id = id;
        Group updatedGroup = _gs.Update(group);
        return Ok(updatedGroup);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Group>> Delete(int id)
    {
      try
      {
        Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
        _gs.Delete(id, profile.Id);
        return Ok();
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}