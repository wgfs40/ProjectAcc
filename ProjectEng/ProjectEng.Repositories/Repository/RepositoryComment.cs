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
    public class RepositoryComment : RepositoryBase<ProjectEngContext>,IRepositoryComment
    {
        public IList<Comment> GetListComment()
        {
            return GetList<Comment>().ToList();
        }

        public Comment GetCommentByID(int CommentID)
        {
            return Get<Comment>(c => c.ID == CommentID);
        }

        public OperationStatus SaveComment(Comment comment)
        {
            using (DataContext)
            {
                DataContext.Entry(comment).State = comment.ID == 0 ? EntityState.Added : EntityState.Modified;
                return Save<Comment>(comment);
            }
        }
    }
}
