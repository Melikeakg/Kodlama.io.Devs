using Application.Features.UserSocialMedias.Command.CreateSocialMedia;
using Application.Features.UserSocialMedias.Command.DeleteSocialMedia;
using Application.Features.UserSocialMedias.Command.UpdateSocialMedia;
using Application.Features.UserSocialMedias.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSocialMediasController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateSocialMediaCommand createSocialMediaCommand)
        {
            CreatedSocialMediaDto result = await Mediator.Send(createSocialMediaCommand);
            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteSocialMediaCommand deleteSocialMediaCommand)
        {
            DeletedSocialMediaDto result = await Mediator.Send(deleteSocialMediaCommand);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSocialMediaCommand updateSocialMediaCommand)
        {
            UpdatedSocialMediaDto result = await Mediator.Send(updateSocialMediaCommand);
            return Ok(result);
        }
    }
}
