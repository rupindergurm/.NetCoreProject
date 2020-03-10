using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LM.Controllers
{
    [Route("api/Library")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
              
            private readonly UserContext _context;
            public LibraryController()
            {
                _context = new UserContext();
            }
           [HttpGet]
            [Route("get/id")]
            public ActionResult GetUser(Guid Id)
            {
                UserPoco poco = _context.Users.Where(u => u.id == Id).FirstOrDefault();
                return Ok(poco);
            }
            [HttpGet]
            [Route("get")]
            public ActionResult GetUser()
            {
                UserPoco poco = _context.Users.FirstOrDefault();
                return Ok(poco);
            }
            [HttpPost]
            [Route("Post")]
            public ActionResult PostUser([FromBody] UserPoco poco)
            {
                _context.Users.Add(poco);
                _context.SaveChanges();

                return Ok();
            }
            [HttpPut]
            [Route("put")]
            public ActionResult PutUser([FromBody] UserPoco poco)
            {
                _context.Users.Append(poco);
                _context.SaveChanges();
                return Ok();
            }
            [HttpDelete]
            [Route("delete")]
            public ActionResult DeleteUser(Guid id)
            {
                UserPoco poco = _context.Users.Where(u => u.id == id).FirstOrDefault();
                _context.Users.Remove(poco);
                _context.SaveChanges();
                return Ok();
            }
        }
        public class UserContext : DbContext

        {
            public DbSet<BookPoco> Books { get; set; }
            public DbSet<UserPoco> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LM.Controllers.UserContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(optionsBuilder);
        }
            public UserContext()
            {
                
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                modelBuilder.Entity<UserPoco>().HasMany(u => u.Books);
                base.OnModelCreating(modelBuilder);
            }
        }
        [Table("Books")]
        public class BookPoco
        {
            [Key]
            public Guid id { get; set; }
            public String BookName { get; set; }
            public int ISBN { get; set; }
            public DateTime Published_Date { get; set; }
        }
        [Table("Users")]
        public class UserPoco
        {
            [Key]
            public Guid id { get; set; }
            public String UserName { get; set; }
            public DateTime DoB { get; set; }
            public virtual ICollection<BookPoco> Books { get; set; }
        }
    }




