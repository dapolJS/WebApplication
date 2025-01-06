using System;
using System.Collections.Generic;
using Xunit;
using FirstWebApi.Models;
using System.Text.Json;

namespace FirstWebApi.Tests.Models
{
    public class NotebookTests
    {
        // Test the default values of the properties
        [Fact]
        public void Notebook_DefaultValues_AreCorrect()
        {
            // Arrange & Act
            Notebook notebook = new Notebook();

            // Assert
            Assert.Equal("", notebook.Title); // Default value of Title should be an empty string
        }

        // Test that setting Title works as expected
        [Fact]
        public void Notebook_SetTitle_SetsCorrectValue()
        {
            // Arrange
            Notebook notebook = new Notebook();
            var expectedTitle = "Test Notebook";

            // Act
            notebook.Title = expectedTitle;

            // Assert
            Assert.Equal(expectedTitle, notebook.Title);
        }

        // Test that setting UniqueKey works as expected
        [Fact]
        public void Notebook_SetUniqueKey_SetsCorrectValue()
        {
            // Arrange
            var notebook = new Notebook();
            var expectedUniqueKey = "123-ABC";

            // Act
            notebook.UniqueKey = expectedUniqueKey;

            // Assert
            Assert.Equal(expectedUniqueKey, notebook.UniqueKey);
        }

        // Test the serialization behavior for the Room property
        [Fact]
        public void Notebook_SerializesCorrectly_ExcludesRoomProperty()
        {
            // Arrange
            var notebook = new Notebook
            {
                Title = "Test Notebook",
                UniqueKey = "123-ABC",
                Room = new Room() // Assuming Room is another model
            };

            // Act
            var json = JsonSerializer.Serialize(notebook);

            // Assert
            Assert.DoesNotContain("Room", json); // Room property should not be part of the serialized JSON
        }

        // Test that Notes collection is initialized correctly
        [Fact]
        public void Notebook_NotesCollection_IsInitialized()
        {
            // Arrange & Act
            var notebook = new Notebook();

            // Assert
            Assert.Null(notebook.Notes); // The collection should be empty initially
        }
    }
}
