using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToDoList.Models;

public class ToDoDb : DbContext
{
    public ToDoDb(DbContextOptions<ToDoDb> options) 
        : base(options)
    { 
    }
    public DbSet<ToDo> ToDos { get; set; } = null!;


    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=todolist;Username=postgres;Password=postgres");
    }*/
}