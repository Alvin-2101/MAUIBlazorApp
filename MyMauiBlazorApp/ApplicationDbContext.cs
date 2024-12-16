using System;
using Microsoft.EntityFrameworkCore;

namespace MyMauiBlazorApp;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

    public DbSet<Resumes> ResumesSet { get; set; }
    public DbSet<JobDescription> JobDescriptions { get; set; }
    public DbSet<ResumeMatch> ResumeMatches { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Resume entity
        modelBuilder.Entity<Resumes>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Text).IsRequired();
            entity.Property(e => e.Embeddings).IsRequired();
        });

        // Configure the JobDescription entity
        modelBuilder.Entity<JobDescription>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Embeddings).IsRequired();
        });
    }
}

public class JobDescription
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public float[] Embeddings { get; set; }
}

public class Resumes
{
     public int Id { get; set; }
    public string Text { get; set; }
    public float[] Embeddings { get; set; }
}

public class ResumeMatch
{
    public int ResumeId { get; set; }
    public string ResumeText { get; set; }
    public int JobDescriptionId { get; set; }
    public double Similarity { get; set; }
}