using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;

namespace ZNews.Application.Services.Users.Queries.GetUserProperties
{
    public interface IGetUserPropertiesService
    {
        Guid UserGuid(long UserId);
        string ImageUrl(long UserId);
    }
    public class GetUserPropertiesService : IGetUserPropertiesService
    {
        private readonly IDataBaseContext _context;
        public GetUserPropertiesService(IDataBaseContext context)
        {
            _context = context;
        }

        public string ImageUrl(long UserId)
        {
            return _context.Users.Find(UserId).ImageUrl;
        }
        public Guid UserGuid(long UserId)
        {
            return _context.Users.Find(UserId).UserGuid;
        }
    }
}
