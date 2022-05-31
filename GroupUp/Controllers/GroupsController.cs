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

    public GroupsController(GroupsService gs)
    {
      _gs = gs;
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

    [HttpGet("{id}")]
    public ActionResult<Group> Get(int id)
    {
      try
      {
        // TODO check for auth
        Group group = _gs.Get(id);
        return Ok(group);
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