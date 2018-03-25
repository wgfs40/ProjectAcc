using PagedList;
using ProjectEng.Models;
using ProjectEng.Repositories.UnitWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.Repository
{
    public class RepositoryProject : RepositoryBase<ProjectEngContext>,IRepositoryProject
    {

        public IPagedList<Project> GetListProjectByPM(string sort, string PM, string status, int index, int count, params System.Linq.Expressions.Expression<Func<Project, object>>[] incluedeProperties)
        {
            IQueryable<Models.Project> query = DataContext.Set<Models.Project>();

            foreach (var incluedePropertie in incluedeProperties)
            {
                query = query.Include(incluedePropertie);
            }
            IPagedList<Project> listpaged;

            switch (sort)
            {
                case "ProjectID":
                    listpaged = query.Where(p => p.ProjectStaffs.PMStaff == PM && p.Status == status).OrderBy(p => p.ProjectID).ToPagedList(index, count);
                    break;
                case "CustomerName":
                    listpaged = query.Where(p => p.ProjectStaffs.PMStaff == PM && p.Status == status).OrderBy(p => p.Customers.CustomerName).ToPagedList(index, count);
                    break;
                default:
                    listpaged = query.Where(p => p.ProjectStaffs.PMStaff == PM && p.Status == status).OrderBy(p => p.ProjectID).ToPagedList(index, count);
                    break;
            }

            return listpaged;
        }

        public IPagedList<Project> GetListProjectByCustomerId(string sort, int customerid, string PM, string status, int index, int count, params System.Linq.Expressions.Expression<Func<Project, object>>[] incluedeProperties)
        {
            IQueryable<Models.Project> query = DataContext.Set<Models.Project>();

            foreach (var incluedePropertie in incluedeProperties)
            {
                query = query.Include(incluedePropertie);
            }

            IPagedList<Project> listpaged;
            switch (sort)
            {
                case "ProjectID":
                    listpaged = query.Where(p => p.ProjectStaffs.PMStaff == PM && p.Status == status && p.CustomerId == customerid).OrderBy(p => p.ProjectID).ToPagedList(index, count);
                    break;
                case "CustomerName":
                    listpaged = query.Where(p => p.ProjectStaffs.PMStaff == PM && p.Status == status && p.CustomerId == customerid).OrderBy(p => p.Customers.CustomerName).ToPagedList(index, count);
                    break;
                default:
                    listpaged = query.Where(p => p.ProjectStaffs.PMStaff == PM && p.Status == status && p.CustomerId == customerid).OrderBy(p => p.ProjectID).ToPagedList(index, count);
                    break;
            }

            return listpaged;
        }

        public IPagedList<Project> GetListProjectByCustomerId(string sort, int customerid, string status, int index, int count, params System.Linq.Expressions.Expression<Func<Project, object>>[] incluedeProperties)
        {
            IQueryable<Models.Project> query = DataContext.Set<Models.Project>();

            foreach (var incluedePropertie in incluedeProperties)
            {
                query = query.Include(incluedePropertie);
            }

            IPagedList<Project> listpaged;
            switch (sort)
            {
                case "ProjectID":
                    listpaged = query.Where(p => p.Status == status && p.CustomerId == customerid).OrderBy(p => p.ProjectID).ToPagedList(index, count);
                    break;
                case "CustomerName":
                    listpaged = query.Where(p => p.Status == status && p.CustomerId == customerid).OrderBy(p => p.Customers.CustomerName).ToPagedList(index, count);
                    break;
                default:
                    listpaged = query.Where(p => p.Status == status && p.CustomerId == customerid).OrderBy(p => p.ProjectID).ToPagedList(index, count);
                    break;
            }

            return listpaged;
        }

        public IPagedList<Project> GetListProject(string sort, string status, int index, int count, params System.Linq.Expressions.Expression<Func<Project, object>>[] incluedeProperties)
        {
            IQueryable<Models.Project> query = DataContext.Set<Models.Project>();

            foreach (var incluedePropertie in incluedeProperties)
            {
                query = query.Include(incluedePropertie);
            }

            IPagedList<Project> listpaged;
            switch (sort)
            {
                case "ProjectID":
                    listpaged = query.Where(p => p.Status == status).OrderBy(p => p.ProjectID).ToPagedList(index, count);
                    break;
                case "CustomerName":
                    listpaged = query.Where(p => p.Status == status).OrderBy(p => p.Customers.CustomerName).ToPagedList(index, count);
                    break;
                default:
                    listpaged = query.Where(p => p.Status == status).OrderBy(p => p.ProjectID).ToPagedList(index, count);
                    break;
            }
           

            return listpaged;
        }

        public IList<Models.Project> GetListProject(string sort, params System.Linq.Expressions.Expression<Func<Models.Project, object>>[] incluedeProperties)
        {
            IQueryable<Models.Project> query = DataContext.Set<Models.Project>();

            foreach (var incluedePropertie in incluedeProperties)
            {
                query = query.Include(incluedePropertie);
            }

            return query.OrderByDescending(p => p.ProjectID).Take(10).ToList();
        }

        public IList<Models.DTO.Dashboard> GetListProjectDashboard()
        {
            var getlist = DataContext.Database.SqlQuery<Models.DTO.Dashboard>("proc_ProjectDashboard").ToList();

            return getlist;
        }

        public Models.Project GetProjectByProjectId(int projectid, params System.Linq.Expressions.Expression<Func<Models.Project, object>>[] incluedeProperties)
        {
            IQueryable<Models.Project> query = DataContext.Set<Models.Project>();

            foreach (var incluedePropertie in incluedeProperties)
            {
                query = query.Include(incluedePropertie);
            }

            return query.Where( p => p.ProjectID == projectid).SingleOrDefault();
        }
      
        public OperationStatus SaveProject(Models.Project project)
        {
            using (DataContext)
            {
                if (project.ProjectStatus == Status.Add)
                {
                    DataContext.Entry(project).State = EntityState.Added;
                }
                else
                {
                    if (project.ProjectStatus == Status.Modify)
                    {
                        DataContext.Entry(project).State = EntityState.Modified;
                    }
                }
                
                return Save<Models.Project>(project);
            }
        }

        public OperationStatus SaveProjectStaff(ProjectStaff projectstaff)
        {
            throw new NotImplementedException();
        }


        
    }
}
