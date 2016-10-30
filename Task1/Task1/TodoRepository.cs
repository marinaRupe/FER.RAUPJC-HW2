using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Homework1;
using Task1;

namespace Repositories
{
    /// <summary >
    /// Class  that  encapsulates  all  the  logic  for  accessing  TodoTtems.
    ///  </summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository  does  not  fetch  todoItems  from  the  actual  database ,
        /// it uses in  memory  storage  for  this  excersise.
        /// </summary >
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            _inMemoryTodoDatabase = initialDbState  ?? new GenericList <TodoItem >();
        }
        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                TodoItem item = _inMemoryTodoDatabase.FirstOrDefault(t => t.Id.Equals(todoItem.Id));

                if (item != null)
                {
                    throw new DuplicateTodoItemException("duplicate id: " + todoItem.Id);
                }
                else
                {
                    _inMemoryTodoDatabase.Add(todoItem);
                }
            }
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.FirstOrDefault(t => t.Id.Equals(todoId));
        }

        public List<TodoItem> GetActive()
        {
            return _inMemoryTodoDatabase.Where(t => t.IsCompleted == false).ToList();
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _inMemoryTodoDatabase.Where(t => t.IsCompleted == true).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryTodoDatabase.Where(filterFunction).ToList();
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            TodoItem item = this.Get(todoId);

            if (item == null)
            {
                return false;
            }
            else
            {
                if (item.IsCompleted)
                {
                    return false;
                }
                else
                {
                    item.MarkAsCompleted();
                    return true;
                }
            }   
        }

        public bool Remove(Guid todoId)
        {
            TodoItem item = this.Get(todoId);

            if (item != null)
            {
                return _inMemoryTodoDatabase.Remove(item);
            }
            else
            {
                return false;
            }
            
        }

        public void Update(TodoItem todoItem)
        {
            TodoItem item = this.Get(todoItem.Id);

            if (item == null)
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
            else
            {
                item = todoItem;
            }
        }
    }
}
