using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SipStation;

public partial class postgresContext : DbContext
{


    public postgresContext()
    {
    }

    public postgresContext(DbContextOptions<postgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<FlywaySchemaHistory> FlywaySchemaHistories { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<ShoppingSession> ShoppingSessions { get; set; }

    public virtual DbSet<Todo> Todos { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost;Username=postgres;Database=postgres;Password=password");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("clothing_category", new[] { "women", "men", "boy", "girl" })
            .HasPostgresEnum("clothing_subcategory", new[] { "shirt", "trouser", "beanie", "collage pants", "collage shirt", "sweater", "overall", "jeans overall", "hogwarts collage", "blazer", "coat", "dress shirt", "hoodie", "basic t-shirt", "t-shirt dress", "pants" })
            .HasPostgresEnum("user_role", new[] { "admin", "user" })
            .HasPostgresExtension("btree_gin")
            .HasPostgresExtension("btree_gist")
            .HasPostgresExtension("citext")
            .HasPostgresExtension("cube")
            .HasPostgresExtension("dblink")
            .HasPostgresExtension("dict_int")
            .HasPostgresExtension("dict_xsyn")
            .HasPostgresExtension("earthdistance")
            .HasPostgresExtension("fuzzystrmatch")
            .HasPostgresExtension("hstore")
            .HasPostgresExtension("intarray")
            .HasPostgresExtension("ltree")
            .HasPostgresExtension("pg_stat_statements")
            .HasPostgresExtension("pg_trgm")
            .HasPostgresExtension("pgcrypto")
            .HasPostgresExtension("pgrowlocks")
            .HasPostgresExtension("pgstattuple")
            .HasPostgresExtension("tablefunc")
            .HasPostgresExtension("unaccent")
            .HasPostgresExtension("uuid-ossp")
            .HasPostgresExtension("xml2");

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("answers_pkey");

            entity.ToTable("answers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_modified");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Upvotes).HasColumnName("upvotes");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("answers_question_id_fkey");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_item_pkey");

            entity.ToTable("cart_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SessionId).HasColumnName("session_id");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("cart_item_product_id_fkey");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("courses_pkey");

            entity.ToTable("courses");

            entity.HasIndex(e => e.CourseCode, "courses_course_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseCode)
                .HasMaxLength(50)
                .HasColumnName("course_code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_modified");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("drinks_pkey");

            entity.ToTable("drinks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(255)
                .HasColumnName("img_url");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.Sku)
                .HasMaxLength(10)
                .HasColumnName("sku");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Drinks)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("drinks_restaurantid_fkey");
        });

        modelBuilder.Entity<FlywaySchemaHistory>(entity =>
        {
            entity.HasKey(e => e.InstalledRank).HasName("flyway_schema_history_pk");

            entity.ToTable("flyway_schema_history");

            entity.HasIndex(e => e.Success, "flyway_schema_history_s_idx");

            entity.Property(e => e.InstalledRank)
                .ValueGeneratedNever()
                .HasColumnName("installed_rank");
            entity.Property(e => e.Checksum).HasColumnName("checksum");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.ExecutionTime).HasColumnName("execution_time");
            entity.Property(e => e.InstalledBy)
                .HasMaxLength(100)
                .HasColumnName("installed_by");
            entity.Property(e => e.InstalledOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("installed_on");
            entity.Property(e => e.Script)
                .HasMaxLength(1000)
                .HasColumnName("script");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("version");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_details_pkey");

            entity.ToTable("order_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UserId)
                .HasColumnType("character varying")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_details_order_id_fkey");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_items_pkey");

            entity.ToTable("order_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("brand");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Discount)
                .HasPrecision(3, 2)
                .HasColumnName("discount");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.Maincategory)
                .HasMaxLength(50)
                .HasColumnName("maincategory");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_at");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Sku)
                .HasColumnType("character varying")
                .HasColumnName("sku");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("questions_pkey");

            entity.ToTable("questions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_modified");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Upvotes).HasColumnName("upvotes");

            entity.HasOne(d => d.Course).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("questions_course_id_fkey");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("restaurants_pkey");

            entity.ToTable("restaurants");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ShoppingSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shopping_session_pkey");

            entity.ToTable("shopping_session");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_at");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UserId)
                .HasColumnType("character varying")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<Todo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("todos_pkey");

            entity.ToTable("todos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Item).HasColumnName("item");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
