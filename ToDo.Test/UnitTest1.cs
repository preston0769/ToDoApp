using NUnit.Framework;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using System.Collections.Generic;
using FluentAssertions;

namespace ToDo.Test 
{
    public class ToDoManagerTest 
    {
        private Fixture _autofixture;

        [SetUp]
        public void Setup()
        {

            _autofixture = new Fixture();
            _autofixture.Customize(new AutoNSubstituteCustomization());
        }


        [Test]
        public void ToDoManagerShouldHaveEmptyListWhenStart()
        {
            var sut = _autofixture.Build<ToDoManager>().With(x=>x.ToDoList, new List<ToDoItem>()).Create();
            sut.ToDoList.Should().BeEmpty();
        }


        [Test]
        public void ToDoManagerShouldBeAbleToAddItem()
        {
            var sut = _autofixture.Build<ToDoManager>().With(x=>x.ToDoList, new List<ToDoItem>()).Create();

            var item = _autofixture.Create<ToDoItem>();

            sut.AddToDoItem(item);

            sut.ToDoList.Should().Contain(item);
        }

        [Test]
        public void ToDoManagerShouldBeAbleToUpdateToDoItem()
        {
            var sut = _autofixture.Build<ToDoManager>().With(x=>x.ToDoList, new List<ToDoItem>()).Create();

            var item = _autofixture.Create<ToDoItem>();

            sut.AddToDoItem(item);

            var newitem = _autofixture.Create<ToDoItem>();
            newitem.Id = item.Id;
            sut.UpdateToDoItem(newitem);
            
            sut.ToDoList.Should().Contain(newitem);
            sut.ToDoList.Should().NotContain(item);

        }

    }
}