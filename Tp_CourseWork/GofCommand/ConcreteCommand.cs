using Tp_CourseWork.DB;
using Tp_CourseWork.Models;

namespace Tp_CourseWork.GofComand
{
    public class GetLocalitiesCommand : ICommand
    {
        ReceiverGetLocalities receiver;
        public GetLocalitiesCommand(ReceiverGetLocalities r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.GetLocalities(ctx);
        }
    }

    public class GetBudgetsCommand : ICommand
    {
        ReceiverGetBudgets receiver;
        public GetBudgetsCommand(ReceiverGetBudgets r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.GetBudgets(ctx);
        }
    }
}
