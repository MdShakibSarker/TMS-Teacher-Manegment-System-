using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Models;

namespace TMS.Model_Configuration;

public class TeacherConfiguration:IEntityTypeConfiguration<Teacher>
{

	public void Configure(EntityTypeBuilder<Teacher> builder)
	{
		 builder.ToTable(nameof(Teacher));
		builder.HasKey(x=>x.Id);
		builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
		builder.Property(x => x.Phone).HasMaxLength(250).IsRequired();
		builder.Property(x => x.Address).HasMaxLength(50).IsRequired();
		builder.Property(x => x.City).HasMaxLength(15).IsRequired();
	}
}
