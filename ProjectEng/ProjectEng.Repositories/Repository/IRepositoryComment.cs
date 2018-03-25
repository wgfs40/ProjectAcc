using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectEng.Models;

namespace ProjectEng.Repositories.Repository
{
    public interface IRepositoryComment
    {
        IList<Comment> GetListComment();
        Comment GetCommentByID(int CommentID);


        OperationStatus SaveComment(Comment comment);
    }
}
