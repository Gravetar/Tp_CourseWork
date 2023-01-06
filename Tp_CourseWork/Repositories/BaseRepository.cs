using Tp_CourseWork.DB;
using Tp_CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using Tp_CourseWork.GofComand;

namespace Tp_CourseWork.Repositories
{    public class BaseRepository
    {
        public ApplicationContext _ctx;
        private Dictionary<string, ICommand> _commands;

        public BaseRepository(ApplicationContext ctx)
        {
            _ctx = ctx;
            FillCommands();
        }

        /// <summary>
        /// Получить всю информаци по всем локациям
        /// </summary>
        /// <returns>Лист локаций</returns>
        public List<Locality> GetLocalities()
        {
            return ExcuteCommand("GetLocalities") as List<Locality>;
        }

        /// <summary>
        /// Получть бюджеты
        /// </summary>
        /// <returns>Массив бюджетов</returns>
        public double[] GetBudgets()
        {
            return ExcuteCommand("GetBudgets") as double[];
        }

        private void FillCommands()
        {
            _commands = new Dictionary<string, ICommand>();

            _commands.Add("GetLocalities", new GetLocalitiesCommand(new ReceiverGetLocalities()));
            _commands.Add("GetBudgets", new GetBudgetsCommand(new ReceiverGetBudgets()));
        }

        private object ExcuteCommand(string command)
        {
            Invoker invoker = new Invoker();

            invoker.SetCtx(_ctx);
            invoker.SetCommand(_commands[command]);
            return invoker.Run();
        }
    }
}
