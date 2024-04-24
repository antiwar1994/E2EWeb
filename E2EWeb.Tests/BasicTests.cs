using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace E2EWeb.Tests
{
    public class BasicTests
     : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public BasicTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, Priority(5)]
        public async Task GetTodoItems()
        {
            var response = await _client.GetAsync("/api/Todo");
            var result = await response.Content.ReadFromJsonAsync<List<TodoItem>>();

            response.EnsureSuccessStatusCode();
            Assert.True(result.Count() > 0);
        }

        [Fact, Priority(4)]
        public async Task GetTodoItem()
        {
            var response = await _client.GetAsync("/api/Todo/2");
            var result = await response.Content.ReadFromJsonAsync<TodoItem>();

            response.EnsureSuccessStatusCode();
            Assert.Equal(2, result.Id);
        }

        [Fact, Priority(1)]
        public async Task PostTodoItem()
        {
            var todo = new TodoItem { Id = 2, Name = "test", IsComplete = false };       
            var response = await _client.PostAsJsonAsync("/api/Todo", todo);
            var result = await response.Content.ReadFromJsonAsync<TodoItem>();

            response.EnsureSuccessStatusCode();
            Assert.Equal(2, result.Id);
        }

        [Fact, Priority(2)]
        public async Task PutTodoItem()
        {
            var todo = new TodoItem { Id = 1, Name = "test2313", IsComplete = false };

            var response = await _client.PostAsJsonAsync("/api/Todo/1", JsonConvert.SerializeObject(todo));
            var result = await response.Content.ReadFromJsonAsync<TodoItem>();

            response.EnsureSuccessStatusCode();
            Assert.Equal(todo.Name, result.Name);
        }

        [Fact, Priority(3)]
        public async Task DeleteTodoItem()
        {          
            var response = await _client.DeleteAsync("/api/Todo/1");
            response.EnsureSuccessStatusCode();
        }
    }
}
