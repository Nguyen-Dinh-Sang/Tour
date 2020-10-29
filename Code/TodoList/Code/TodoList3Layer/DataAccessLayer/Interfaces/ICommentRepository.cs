using DataAccessLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface ICommentRepository
    {
        bool remove(int id);

        Comment save(Comment comment);

        Comment getById(int id);

        IEnumerable<Comment> getAllByIdWork(int id);
    }
}
