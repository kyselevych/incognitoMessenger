using AutoMapper;
using Business.Entities;
using IncognitoMessenger.ApplicationCore;
using IncognitoMessenger.ApplicationCore.Chats;
using IncognitoMessenger.ApplicationCore.Chats.Models;
using IncognitoMessenger.Hubs;
using IncognitoMessenger.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace IncognitoMessenger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> hubContext;
        private readonly ChatService chatService;
        private readonly InviteService inviteService;
        private readonly IMapper mapper;

        public ChatController(IHubContext<ChatHub> hubContext, ChatService chatService, InviteService inviteService, IMapper mapper)
        {
            this.hubContext = hubContext;
            this.chatService = chatService;
            this.inviteService = inviteService;
            this.mapper = mapper;
        }

        [HttpPost("create"), Authorize]
        public IActionResult CreateChat([FromBody] CreateChatModel createChatCredential)
        {
            try
            {
                var data = mapper.Map<Chat>(createChatCredential);
                var userId = User.Identity?.GetUserId();

                if (userId == null)
                    throw new ValidationException("Incorrect user id");

                data.UserId = userId.Value;

                var response = chatService.CreateChat(data);
                chatService.JoinUser(userId.Value, response.Id);

                return Ok(ApiResponse.Success(response));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse.Failure(ex.Message));
            }
        }

        [HttpGet("list"), Authorize]
        public IActionResult GetChatsMember()
        {
            try
            {
                var userId = User.Identity?.GetUserId();

                if (userId == null)
                    throw new ValidationException("Incorrect user id");

                var response = chatService.GetChats(userId.Value);

                return Ok(ApiResponse.Success(response));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse.Failure(ex.Message));
            }
        }

        [HttpPost("createinvite"), Authorize]
        public IActionResult CreateInvite([FromBody] CreateInviteModel createInviteModel)
        {
            try
            {
                var userId = User.Identity?.GetUserId();

                if (userId == null)
                    throw new ValidationException("Incorrect user id");

                var response = inviteService.CreateInvite(createInviteModel.ChatId, userId.Value);
                return Ok(ApiResponse.Success(response));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse.Failure(ex.Message));
            }
        }

        [HttpPost("acceptinvite"), Authorize]
        public IActionResult AcceptInvite([FromBody] AcceptInviteModel acceptInviteModel)
        {
            try
            {
                var userId = User.Identity?.GetUserId();

                if (userId == null) throw new ValidationException("Incorrect user id");

                inviteService.AcceptInvite(userId.Value, acceptInviteModel.Code);
                return Ok(ApiResponse.Success(new {}));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse.Failure(ex.Message));
            }
        }

        [HttpPost("kickuser"), Authorize]
        public IActionResult KickUser([FromBody] KickUserModel kickUserModel)
        {
            try
            {
                var userId = User.Identity?.GetUserId();

                if (userId == null) throw new ValidationException("Incorrect user id");

                chatService.KickUser(kickUserModel.ChatId, userId.Value, kickUserModel.UserId);
                return Ok(ApiResponse.Success(new { }));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse.Failure(ex.Message));
            }
        }

        [HttpPost("leavechat"), Authorize]
        public IActionResult LeaveChat([FromBody] LeaveChatModel leaveChatModel)
        {
            try
            {
                var userId = User.Identity?.GetUserId();

                if (userId == null) throw new ValidationException("Incorrect user id");

                chatService.LeaveChat(leaveChatModel.ChatId, userId.Value);
                return Ok(ApiResponse.Success(new { }));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse.Failure(ex.Message));
            }
        }
    }
}
