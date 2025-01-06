using System.ComponentModel.DataAnnotations;
using FirstWebApi.Models;

namespace MyApi.Tests.NotesTests
{
    public class NoteModelTests
    {
        [Fact]
        public void NoteDefaultValuesAreCorrect()
        {
            Note note = new Note();

            Assert.Equal("", note.Title);
            Assert.Equal("", note.Description);
        }
        [Fact]
        public void NoteModelShouldBeValidWithValidData()
        {
            Note note = new Note
            {
                Title = "Test",
                Description = "This is for Test"
            };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(note, new ValidationContext(note), validationResults, true);

            Assert.True(isValid);
        }
    }
}
