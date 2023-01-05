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

        /// <summary>
        /// Получить всю информаци по всем локациям
        /// </summary>
        /// <returns>Лист локаций</returns>
        public List<Locality> GetLocalities()
        {
            return _ctx.Localities.ToList();
        }

        /// <summary>
        /// Получть бюджеты
        /// </summary>
        /// <returns>Массив бюджетов</returns>
        public double[] GetBudgets()
        {
            List<double> Temp = new List<double>();

            foreach (Locality item in _ctx.Localities.ToList())
            {
                Temp.Add(item.Budget);
            }

            return Temp.ToArray();
        }
    }
}
