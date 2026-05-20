using DonationProcessor.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace IdentityCampaign.Infrastructure;

public class AppDbContextFactory: IDesignTimeDbContextFactory<DonationProcessorDbContext>
{
    public DonationProcessorDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DonationProcessorDbContext>();
            //Essa conectionString só será necessária para criar o banco pela primeira vez, depois disso, o banco já estará criado e a aplicação irá usar a conectionString do appsettings.json
            optionsBuilder.UseMySql
            (
                "Server=localhost;Port=3306;Database=Bd_Donation;Uid=root;Pwd=1234;",
                new MySqlServerVersion(new Version(8, 0, 43))
            );
            return new DonationProcessorDbContext(optionsBuilder.Options);
        }
    
}
