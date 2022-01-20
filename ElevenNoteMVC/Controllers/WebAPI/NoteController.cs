using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNoteMVC.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Note")]
    public class NoteController : ApiController
    {
        private bool SetStarState(int noteId, bool newState)
        {
            //Create the Service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            //Get the note that is going to be starred/unstarred
            var note = service.GetNoteById(noteId);
            //Create an istance of the NoteEdit model to change the IsStarred bool
            var updatedNote =
                new NoteEdit
                {
                    NoteId = note.NoteId,
                    Title = note.Title,
                    Content = note.Content,
                    IsStarred = newState
                };
            //Return whether or not the update went through
            return service.UpdateNote(updatedNote);
        }
        [Route("{id}/Star")]
        [HttpPut]
        public bool ToggleStarOn(int id) => SetStarState(id, true);
        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);
    }
}
