using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Homework1;

namespace Task1
{
    public class TodoRepository : ITodoRepository
    {
        private IEnumerable<TodoItem> _todoRepository = new GenericList<TodoItem>();
        public void Add(TodoItem todoItem)
        {
            ((IGenericList<TodoItem>)_todoRepository).Add(todoItem);
        }

        public TodoItem Get(Guid todoId)
        {
            return _todoRepository.Where(t => t.Id.Equals(todoId)).FirstOrDefault();
        }

        public List<TodoItem> GetActive()
        {
            return _todoRepository.Where(t => t.IsCompleted == false).ToList();
        }

        public List<TodoItem> GetAll()
        {
            return _todoRepository.ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _todoRepository.Where(t => t.IsCompleted == true).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _todoRepository.Where(filterFunction).ToList();
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            try
            {
                this.Get(todoId).MarkAsCompleted();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool Remove(Guid todoId)
        {
            try
            {
                _todoRepository = _todoRepository.Where(t => t.Id != todoId);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public void Update(TodoItem todoItem)
        {
            TodoItem item = this.Get(todoItem.Id);

            if (item == null)
            {
                this.Add(item);
            }
            else
            {
                item = todoItem;
            }
        }
    }
}
