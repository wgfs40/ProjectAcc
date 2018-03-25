using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectEng.Models;

namespace ProjectEng.Repositories.Repository
{
    public interface IRepositoryTask
    {
        IList<Models.Task> GetListTask();
        IList<Models.Task> GetListTaskByProjectId(int projectId);
        IList<Models.Task> GetListTaskByProjectId_All(int projectId);
        Models.Task GetTaskByID(int TaskID);


        OperationStatus SaveTask(ProjectEng.Models.Task task);
    }
}
