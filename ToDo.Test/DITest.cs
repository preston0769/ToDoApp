using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using FluentAssertions;

namespace ToDo.Test
{
    [TestFixture]
    public class DITest
    {


        private ServiceProvider _serviceProvider;
        [SetUp]
        public void SetUp()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(typeof(ToDoManager));
            serviceCollection.AddLogging();

            _serviceProvider = serviceCollection.BuildServiceProvider();

        }


        [Test]
        public void TodoManagerShouldBeResolved()
        {


            var todoManager = _serviceProvider.GetService<ToDoManager>();

            todoManager.Should().NotBeNull();

        }
    }
}
