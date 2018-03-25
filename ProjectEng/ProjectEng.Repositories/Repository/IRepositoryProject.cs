using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectEng.Models;
using System.Linq.Expressions;
using PagedList;

namespace ProjectEng.Repositories.Repository
{
    public interface IRepositoryProject
    {
        IList<Models.Project> GetListProject(string sort, params Expression<Func<Models.Project, object>>[] incluedeProperties);
        IPagedList<Models.Project> GetListProject(string sort, string status, int index, int count, params Expression<Func<Models.Project, object>>[] incluedeProperties);
        IPagedList<Models.Project> GetListProjectByPM(string sort,string PM,string status,int index,int count,params Expression<Func<Models.Project, object>>[] incluedeProperties);
        IPagedList<Models.Project> GetListProjectByCustomerId(string sort, int customerid, string PM, string status, int index, int count, params Expression<Func<Models.Project, object>>[] incluedeProperties);
        IPagedList<Models.Project> GetListProjectByCustomerId(string sort, int customerid, string status, int index, int count, params Expression<Func<Models.Project, object>>[] incluedeProperties);

        IList<Models.DTO.Dashboard> GetListProjectDashboard();
        Models.Project GetProjectByProjectId(int projectid, params Expression<Func<Models.Project, object>>[] incluedeProperties);

        OperationStatus SaveProject(Models.Project project);
        OperationStatus SaveProjectStaff(ProjectStaff projectstaff);
    }
}
