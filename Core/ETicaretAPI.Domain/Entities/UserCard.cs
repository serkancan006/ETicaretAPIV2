using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;


namespace ETicaretAPI.Domain.Entities
{
    public class UserCard : BaseEntity
    {
        public string CardAlias { get; set; } // card takma adı (kredi kartım)
        public string CardToken { get; set; } // iyzico card token
        public string CardUserKey { get; set; } // kullanıcıya özel key
        public string LastFourDigits { get; set; }  // son 4 hane
        public string ExpireMonth { get; set; }  // son kullanma ayı
        public string ExpireYear { get; set; }   // son kullanma yılı


        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
