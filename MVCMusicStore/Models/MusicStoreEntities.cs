using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
/*Essa classe representará o contexto de banco de dados Entity Framework e
* tratará nossas operações de criação, leitura, atualização e exclusão para nós*/

namespace MVCMusicStore.Models
{
    public class MusicStoreEntities : DbContext
    {
        public DbSet<Album> Tab_Album { get; set; }
        public DbSet<Artist> Tab_Artist { get; set; }
        public DbSet<Genre> Tab_Genre { get; set; }
        public DbSet<Usuario> Tab_Usuario { get; set; }
        public DbSet<Cart> Tab_Cart { get; set; }
        public DbSet<Order> Tab_Order { get; set; }
        public DbSet<OrderDetail> Tab_OrderDetail { get; set; }

        public MusicStoreEntities (DbContextOptions<MusicStoreEntities> options) :base (options)
        {
        }
    }
}
