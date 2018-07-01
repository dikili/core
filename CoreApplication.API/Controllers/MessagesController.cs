using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoreApplication.API.DTOs;
using CoreApplication.API.Helpers;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApplication.API.Controllers
{
      // [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    public class MessagesController : Controller
    {
        private IMapper _mapper;
        private IDatingRepository _userRepo;

        public MessagesController(IDatingRepository userRepo, IMapper mapper)
        {
            _mapper=mapper;
            _userRepo=userRepo;
        }

       [HttpGet("{id}", Name="GetMessage")]
       public async Task<IActionResult> GetMessage(int userId, int id)
       {
             if(userId!= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            var messageFromRepo = await _userRepo.GetMessage(id);

            if (messageFromRepo == null)
             return NotFound();

             return Ok(messageFromRepo);
       }

       [HttpGet]

       public async Task<IActionResult> GetMessagesForUser(int userId,CoreApplication.Data.Helpers.MessageParams messageParams)
       {
               if(userId!= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

        //   var messeageParamsForDb=_mapper.Map<CoreApplication.Data.Helpers.MessageParams>(messageParams);

            var messagesFromRepo=await _userRepo.GetMessagesForUser(messageParams);

            var messages=_mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            Response.AddPagination(messagesFromRepo.CurrentPage,messagesFromRepo.PageSize,messagesFromRepo.TotalCount,messagesFromRepo.TotalPages);

            return Ok(messages);
       }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, [FromBody] MessageForCreationDto messageForCreationDto) 
        {
           if(userId!= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            messageForCreationDto.SenderId= userId;

            var recipient = await _userRepo.GetUser(messageForCreationDto.ReceiverId);
             var sender = await _userRepo.GetUser(messageForCreationDto.SenderId);
          
            if(recipient==null)
                return BadRequest("Could not find user");

            var message=_mapper.Map<Message>(messageForCreationDto);

            _userRepo.Add(message);

            var messageToReturn= _mapper.Map<MessageToReturnDto>(message);

            if(await _userRepo.SaveAll())
                return CreatedAtRoute("GetMessage",new {id=message.Id},messageToReturn);

                throw new Exception("Creating the message failed on save");
        }

        [HttpGet("thread/{id}")]
        public async Task<IActionResult> GetMessageThread(int userId,int id)
        {

            if(userId!= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            var messagesFromRepo = await _userRepo.GetMessageThread(userId,id);

            var messageThread= _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messageThread);


         
        }

        [HttpPost("{id}")]

        public async Task<IActionResult> DeleteMessage(int id, int userId)
        {

            if(userId!= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                 return Unauthorized();

            var messageFromRepo = _userRepo.GetMessage(id).Result;

            if(messageFromRepo.SenderId == userId)
              messageFromRepo.SenderDeleted = true;

               if(messageFromRepo.ReceiverId == userId)
              messageFromRepo.ReciepentDeleted = true; 

             if(messageFromRepo.SenderDeleted && messageFromRepo.ReciepentDeleted)
              _userRepo.Delete(messageFromRepo);

              if(await _userRepo.SaveAll())
                 return NoContent();

               throw new Exception("Error deleting the message");  


        }
    }
}