using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CoreApplication.Data
{
    public class CoreContext : IdentityDbContext<LoginUser, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>//DbContext//IdentityDbContext<AdUser>
    {
         private static IConfigurationRoot _config { get; set; }

        //public CoreContext(DbContextOptions<CoreContext> options)
        //    :base(options)
        //{
        //   // _config = config;
        //}
        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Response> Responses { get; set; }
// below will come from aspnetcore identity now
    //     public DbSet<LoginUser> LoginUsers{get;set;}

        public DbSet<Photo> Photos {get;set;}

        public DbSet<Like> Likes { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();


            });
            builder.Entity<Like>()
            .HasKey(k=> new {k.LikeeId, k.LikerId});

       builder.Entity<Like>()
            .HasOne(u=>u.Likee)
            .WithMany(u=>u.Liker)
            .HasForeignKey(u=>u.LikerId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
            .HasOne(u=>u.Liker)
            .WithMany(u=>u.Likee)
            .HasForeignKey(u=>u.LikeeId)      
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
            .HasOne(x => x.Sender)
            .WithMany(p => p.MessagesSent)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
            .HasOne(p => p.Receiver)
            .WithMany(x => x.MessagesReceived)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Photo>().HasQueryFilter(p => p.isApproved);


        }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    var config = new Configuration(_config);

        //    optionsBuilder.UseSqlServer(config.GetConnectionString());

        //}


    }
}
