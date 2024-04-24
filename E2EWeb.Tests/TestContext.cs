using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace E2EWeb
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
