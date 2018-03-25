using ProjectEng.Repositories.UnitWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.Repository
{
    public class RepositoryTask : RepositoryBase<ProjectEngContext>,IRepositoryTask
    {
        public IList<Models.Task> GetListTask()
        {
            return GetList<Models.Task>().ToList();
        }

        public Models.Task GetTaskByID(int TaskID)
        {
            return Get<Models.Task>(t => t.ID == TaskID);
        }

        public IList<Models.Task> GetListTaskByProjectId(int projectId)
        {
            return GetList<Models.Task>(t => t.ProjectID == projectId).OrderByDescending(t => t.ID).Take(5).ToList();
        }

        public IList<Models.Task> GetListTaskByProjectId_All(int projectId)
        {
            return GetList<Models.Task>(t => t.ProjectID == projectId).OrderByDescending(t => t.ID).ToList();
        }

        public Models.OperationStatus SaveTask(Models.Task task)
        {
            using (DataContext)
            {
                DataContext.Entry(task).State = task.ID == 0 ? EntityState.Added : EntityState.Modified;
                return Save<Models.Task>(task);
            }
        }
    }
}
