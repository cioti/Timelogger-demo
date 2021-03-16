using FluentAssertions;
using System;
using Timelogger.Domain.Entities;
using Timelogger.Shared.Exceptions;
using Xunit;

namespace Timelogger.Tests
{
    public class ProjectTests
    {
        [Fact]
        public void Constructor_EndDateLessThanStartDate_ThrowsException()
        {
            //Arrange
            Action act = () => new Project("test",
                "Test",
                Guid.NewGuid(),
                "test",
                "test",
                DateTime.Now,
                DateTime.Now.AddDays(-1));

            //Act + Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void AddWorkEntry_NullWorkEntry_ThrowsException()
        {
            //Arrange
            var project = GetValidProject();

            //Act
            Action act = () => project.AddWorkEntry(null);

            //Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void AddWorkEntry_WorkEntriesHoursInSameDayExceedsLimit_ThrowsException()
        {
            //Arrange
            var project = GetValidProject();
            project.AddWorkEntry(new WorkEntry("test", "test", 15, DateTime.Now));
            Action act =  () => project.AddWorkEntry(new WorkEntry("test", "test", 2, DateTime.Now));
            
            //Act + Assert
            act.Should().ThrowExactly<BadRequestException>();
        }

        private Project GetValidProject()
            => new Project("test", "test", Guid.NewGuid(), "test", "test", DateTime.Now);
    }
}
