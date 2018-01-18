using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF
{
    /// <summary>
    /// The class represents context for talks.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class TalksContext : DbContext
    {
        /// <summary>
        /// Gets or sets the talks database set.
        /// </summary>
        /// <value>
        /// The talks.
        /// </value>
        public virtual DbSet<Talk> Talks { get; set; }

        /// <summary>
        /// Gets or sets the speakers database set.
        /// </summary>
        /// <value>
        /// The speakers.
        /// </value>
        public virtual DbSet<Speaker> Speakers { get; set; }

        /// <summary>
        /// Gets or sets the disciplines database set.
        /// </summary>
        /// <value>
        /// The disciplines.
        /// </value>
        public virtual DbSet<Discipline> Disciplines { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TalksContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public TalksContext (DbContextOptions<TalksContext> options) :base (options)
		{

		}

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.HasKey(e => e.DisciplineId);

                entity.Property(e => e.DisciplineName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Speaker>(entity =>
            {
                entity.HasKey(e => e.SpeakerId);

                entity.Property(e => e.Department).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Talk>(entity =>
            {
                entity.HasKey(e => e.TalkId);

                entity.Property(e => e.AdditionalDetail).HasMaxLength(280);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TalkDate).HasColumnType("date");

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasMaxLength(140);

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.Talks)
                    .HasForeignKey(d => d.DisciplineId)
                    .HasConstraintName("FK_Disciplines_Discipline");

                entity.HasOne(d => d.Speaker)
                    .WithMany(p => p.Talks)
                    .HasForeignKey(d => d.SpeakerId)
                    .HasConstraintName("FK_Speakers_Speaker");
            });
        }
    }
}
