using Microsoft.EntityFrameworkCore;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence
{
    public class AudioStoreLocalDbContext : DbContext
    {

        /*
         * MIGRATION COMMNAD
         Add-Migration InitialCreated -c AudioStoreDbContext -OutputDir "C:\Users\egomez\source\repos\WebApplicatioMusicStore\WebApplicatioMusicStore\DrivenAdapters\DatabaseAdapters\Migrations"
         */

        public static readonly string PATH_FOLDER_DATABASE = "Data Source=" + System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AudioStore" + "\\AudioStoreLocal.db";

        public AudioStoreLocalDbContext(DbContextOptions<AudioStoreLocalDbContext> options) : base(options)
        {
        }

        public AudioStoreLocalDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var a = PATH_FOLDER_DATABASE;
            optionsBuilder.UseSqlite(a);
        }

        public DbSet<AudioListEntity> AudioListEntity { get; set; }
        public DbSet<RegisterEntity> RegisterEntity { get; set; }
        public DbSet<UserEntity> UsersEntity { get; set; }
    }
}
