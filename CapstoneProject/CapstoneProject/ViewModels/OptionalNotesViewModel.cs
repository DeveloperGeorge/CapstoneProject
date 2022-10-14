using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CapstoneProject.ViewModels
{
    public class OptionalNotesViewModel
    {
        public Note Note { get; set; }
        public Command UpdateNote { get; set; }
        public Command DeleteNote { get; set; }

        public OptionalNotesViewModel(Note note = null)
        {

            Note = note ?? new Note();

            UpdateNote = new Command(ExecuteUpdate);
            DeleteNote = new Command(ExecuteDelete);

        }

        public void ExecuteUpdate()
        {
            Note note = Note;
            App.AcademicRepo.UpdateNote(note);

        }

        public void ExecuteDelete()
        {
            Note note = this.Note;
            int noteId = note.NoteId;
            App.AcademicRepo.RemoveNote(noteId);
            //App.page.Navigation.PopAsync();
        }
    }
}
