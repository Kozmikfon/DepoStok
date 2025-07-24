using MediatR;

namespace DepoStok.Events.Domain
{
    public class StokHareketDurumu:INotification
    {
        public int HareketId { get; }
        public string UserId { get; }
       
        public StokHareketDurumu(int hareketId,string userId)
        {
            HareketId = hareketId;
            UserId = userId;
        } 
    }
}
