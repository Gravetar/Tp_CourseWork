using MathNet.Numerics.Statistics;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using Tp_CourseWork.DB;
using Tp_CourseWork.GoFIterator;
using Tp_CourseWork.Models;
using Tp_CourseWork.Models.ViewModels;

namespace Tp_CourseWork.GofComand
{
    public class ReceiverGetLocalities
    {
        public object GetLocalities(ApplicationContext ctx)
        {
            return ctx.Localities.ToList();
        }
    }

    public class ReceiverGetLStatisticBudgets
    {
        public double[]? Budgets;

        public ReceiverGetLStatisticBudgets(double[]? budgets = null)
        {
            Budgets = budgets;
        }

        public object GetLStatisticBudgets()
        {
            Statistic res = new Statistic();

            res.Median = Statistics.Median(Budgets);
            res.Mean = Statistics.Mean(Budgets);
            res.Max = Statistics.Maximum(Budgets);
            res.Min = Statistics.Minimum(Budgets);

            return res;
        }
    }

    public class ReceiverGetLocalitiesByMajor
    {
        public string Mayor;

        public ReceiverGetLocalitiesByMajor(string mayor)
        {
            Mayor = mayor;
        }

        public object GetLocalitiesByMayor(ApplicationContext ctx)
        {
            var localities = ctx.Localities.Where(l => l.Mayor == Mayor).ToList();
            return localities;
        }
    }

    public class ReceiverGetBudgets
    {
        public object GetBudgets(ApplicationContext ctx)
        {
            List<double> Temp = new List<double>();

            BudgetsAggregate la = new BudgetsAggregate();

            var selectedBudgets = (from b in ctx.Localities
                                   select Convert.ToDouble(b.Budget)).ToList();

            la.FillItems(selectedBudgets);

            Iterator i = la.CreateIterator();

            object item = i.First();

            while (item != null)
            {
                Temp.Add(Convert.ToDouble(item));
                item = i.Next();
            }

            return Temp.ToArray();
        }
    }
    public class ReceiverGetLocalityById
    {
        public int Id;

        public ReceiverGetLocalityById(int id)
        {
            Id = id;
        }

        public object GetLocalityById(ApplicationContext ctx)
        {
            var locality = ctx.Localities.Find(Id);
            return locality;
        }
    }

    public class ReceiverCreateLocality
    {
        public Locality Locality;

        public ReceiverCreateLocality(Locality loc = null)
        {
            Locality = loc;
        }

        public bool CreateLocality(ApplicationContext ctx)
        {
            if (Locality != null)
            {
                ctx.Localities.Add(Locality);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
    }

    public class ReceiverUpdateLocality
    {
        public Locality Loc;

        public ReceiverUpdateLocality(Locality loc = null)
        {
            Loc = loc;
        }

        public bool UpdateLocality(ApplicationContext ctx)
        {
            if (Loc != null)
            {
                ctx.Entry(Loc).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
    }

    public class ReceiverDeleteLocality
    {
        public int Id;

        public ReceiverDeleteLocality(int id = 0)
        {
            Id = id;
        }

        public bool DeleteLocality(ApplicationContext ctx)
        {
            Locality loc = ctx.Localities.Find(Id);

            if (Id != null)
            {
                ctx.Localities.Remove(loc);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
