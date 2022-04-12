namespace BlogSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class BlogContext : DbContext
    {
     
        public BlogContext()
            : base("name=BlogContext")
        {
            Database.SetInitializer<BlogContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BlogCategory> BlogCategories  { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleToCategory> ArticleToCategories { get; set; }
        public DbSet<Comment> Comments  { get; set; }
        public DbSet<Fans> Fans { get; set; }
        public DbSet<Reply>  Replies { get; set; }
        public DbSet<CommentToReply>  CommentToReplies{ get; set; }

    }
     
   
}