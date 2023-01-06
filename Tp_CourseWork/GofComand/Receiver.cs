using System.Windows.Input;
using Tp_CourseWork.DB;
using Tp_CourseWork.Models;

namespace Tp_CourseWork.GofComand
{
    public class ReceiverGetLocalities
    {
        public object GetLocalities(ApplicationContext ctx)
        {
            return ctx.Localities.ToList();
        }
    }

    public class ReceiverGetBudgets
    {
        public object GetBudgets(ApplicationContext ctx)
        {
            List<double> Temp = new List<double>();

            foreach (Locality item in ctx.Localities.ToList())
            {
                Temp.Add(item.Budget);
            }

            return Temp.ToArray();
        }
    }
}
