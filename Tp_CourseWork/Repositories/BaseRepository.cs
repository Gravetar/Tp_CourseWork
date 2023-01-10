using Tp_CourseWork.DB;
using Tp_CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using Tp_CourseWork.GofComand;
using Tp_CourseWork.Models.ViewModels;

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
        /// Получить локацию по айди
        /// </summary>
        /// <returns>Локация</returns>
        public Locality GetLocalityById(int Id)
        {
            _commands["GetLocalityById"] = new GetLocalityByIdCommand(new ReceiverGetLocalityById(Id));
            return ExcuteCommand("GetLocalityById") as Locality;
        }

        /// <summary>
        /// Получить всю информаци по всем локациям
        /// </summary>
        /// <returns>Лист локаций</returns>
        public List<Locality> GetLocalitiesByMayor(string Mayor)
        {
            _commands["GetLocalitiesByMajor"] = new GetLocalitiesByMajorCommand(new ReceiverGetLocalitiesByMajor(Mayor));
            return ExcuteCommand("GetLocalitiesByMajor") as List<Locality>;
        }

        /// <summary>
        /// Создать локацию
        /// </summary>
        public bool CreateLocality(Locality loc)
        {
            _commands["CreateLocality"] = new CreateLocalityCommand(new ReceiverCreateLocality(loc));
            return (bool)ExcuteCommand("CreateLocality");
        }

        /// <summary>
        /// Редактировать локацию
        /// </summary>
        public bool UpdateLocality(Locality loc)
        {
            _commands["UpdateLocality"] = new UpdateLocalityCommand(new ReceiverUpdateLocality(loc));
            return (bool)ExcuteCommand("UpdateLocality");
        }

        /// <summary>
        /// Редактировать локацию
        /// </summary>
        public bool DeleteLocality(int id)
        {
            _commands["DeleteLocality"] = new DeleteLocalityCommand(new ReceiverDeleteLocality(id));
            return (bool)ExcuteCommand("DeleteLocality");
        }

        /// <summary>
        /// Получить статистические показатели бюджета
        /// </summary>
        public Statistic GetLStatisticBudgets(double[]? budgets)
        {
            _commands["GetLStatisticBudgets"] = new GetLStatisticBudgetsCommand(new ReceiverGetLStatisticBudgets(budgets));
            return ExcuteCommand("GetLStatisticBudgets") as Statistic;
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
            _commands.Add("GetLocalitiesByMajor", new GetLocalitiesByMajorCommand(new ReceiverGetLocalitiesByMajor("")));
            _commands.Add("GetLocalityById", new GetLocalityByIdCommand(new ReceiverGetLocalityById(0)));
            _commands.Add("GetBudgets", new GetBudgetsCommand(new ReceiverGetBudgets()));
            _commands.Add("CreateLocality", new CreateLocalityCommand(new ReceiverCreateLocality()));
            _commands.Add("UpdateLocality", new UpdateLocalityCommand(new ReceiverUpdateLocality()));
            _commands.Add("DeleteLocality", new DeleteLocalityCommand(new ReceiverDeleteLocality()));
            _commands.Add("GetLStatisticBudgets", new GetLStatisticBudgetsCommand(new ReceiverGetLStatisticBudgets()));
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
