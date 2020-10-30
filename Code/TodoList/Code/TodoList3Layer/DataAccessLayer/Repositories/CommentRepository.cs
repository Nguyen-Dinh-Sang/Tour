using DataAccessLayer.Context;
using DataAccessLayer.Entitys;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private TodoListDBContext todoListDBContext = TodoListDBContext.getInstance();

        public IEnumerable<Comment> getAllByIdWork(int id)
        {
            var result = from cm in todoListDBContext.Comment
                         where cm.IdWork == id
                         select cm;
            return result;
        }

        public Comment getById(int id)
        {
            return todoListDBContext.Comment.Find(id);
        }

        public bool remove(int id)
        {
            var results = todoListDBContext.Comment.Find(id);
            todoListDBContext.Comment.Remove(results);
            todoListDBContext.SaveChanges();
            if (todoListDBContext.Comment.Find(id) == null)
            {
                return true;
            }
            return false;
        }

        public Comment save(Comment comment)
        {
            if (comment.Id == 0)
            {
                var result = todoListDBContext.Comment.Add(comment);
                todoListDBContext.SaveChanges();
                return result.Entity;
            }
            else
            {
                var result = todoListDBContext.Comment.Find(comment.Id);
                result.CommentContent = comment.CommentContent;
                todoListDBContext.SaveChanges();
                return todoListDBContext.Comment.Find(comment.Id);
            }
        }
    }
}
