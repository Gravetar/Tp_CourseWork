using Tp_CourseWork.DB;
using Tp_CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace Tp_CourseWork.Repositories
{    public class BaseRepository
    {
        public ApplicationContext _ctx;

        public BaseRepository(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        //Получить всю информаци по всем продуктам
        public List<Locality> GetLocalities()
        {
            return _ctx.Localities.ToList();
        }
    }
}
