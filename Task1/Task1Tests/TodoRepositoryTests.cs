using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace Repositories.Tests
{
    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }


        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        [TestMethod]
        public void GettingExistingItemWillReturnItemFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);

            var item = repository.Get(todoItem.Id);
            Assert.AreEqual(item, todoItem);
        }

        public void GettingNotExistingItemWillReturnsNull()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            Assert.IsTrue(repository.Get(todoItem.Id) == null);
        }


        [TestMethod]
        public void MarkingExistingUncompletedItemAsCompletedReturnsTrue()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            repository.Add(todoItem);

            var markedAsCompleted = repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(markedAsCompleted, true);
            Assert.AreEqual(repository.GetCompleted().Count, 1);
        }


        [TestMethod]
        public void MarkingExistingCompletedItemAsCompletedReturnsFalse()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            repository.Add(todoItem);
            repository.MarkAsCompleted(todoItem.Id);

            var markedAsCompleted = repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(markedAsCompleted, false);
            Assert.AreEqual(repository.GetCompleted().Count, 1);
        }


        [TestMethod]
        public void MarkingNotExistingItemAsCompletedReturnsFalse()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            var markedAsCompleted = repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(markedAsCompleted, false);
            Assert.AreEqual(repository.GetCompleted().Count, 0);
        }


        [TestMethod]
        public void GettingActiveTodoItems()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Apples");
            var todoItem2 = new TodoItem("Bananas");
            var todoItem3 = new TodoItem("Watermelons");
            var todoItem4 = new TodoItem("Hamburgers");

            repository.Add(todoItem);
            repository.Add(todoItem2);
            repository.Add(todoItem3);
            repository.Add(todoItem4);

            repository.MarkAsCompleted(todoItem.Id);
            repository.MarkAsCompleted(todoItem3.Id);

            var active = repository.GetActive();

            Assert.AreEqual(active.Count, 2);
            Assert.AreEqual(active[0].IsCompleted, false);
            Assert.AreEqual(active[1].IsCompleted, false);
        }


        [TestMethod]
        public void GettingCompletedTodoItems()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Apples");
            var todoItem2 = new TodoItem("Bananas");
            var todoItem3 = new TodoItem("Watermelons");
            var todoItem4 = new TodoItem("Hamburgers");

            repository.Add(todoItem);
            repository.Add(todoItem2);
            repository.Add(todoItem3);
            repository.Add(todoItem4);

            repository.MarkAsCompleted(todoItem.Id);
            repository.MarkAsCompleted(todoItem3.Id);

            var completed = repository.GetCompleted();
            
            Assert.AreEqual(completed.Count, 2);
            Assert.AreEqual(completed[0].IsCompleted, true);
            Assert.AreEqual(completed[1].IsCompleted, true);
        }


        [TestMethod]
        public void GettingCompletedAndActiveTodoItemsDiffers()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem("Apples");
            var todoItem2 = new TodoItem("Bananas");
            var todoItem3 = new TodoItem("Watermelons");
            var todoItem4 = new TodoItem("Hamburgers");
            var todoItem5 = new TodoItem("Eggs");

            repository.Add(todoItem1);
            repository.Add(todoItem2);
            repository.Add(todoItem3);
            repository.Add(todoItem4);

            repository.MarkAsCompleted(todoItem1.Id);
            repository.MarkAsCompleted(todoItem3.Id);
            repository.MarkAsCompleted(todoItem4.Id);

            var completed = repository.GetCompleted();
            var active = repository.GetActive();

            var completed2 = repository.GetAll().Except(active).ToList();
            var active2 = repository.GetAll().Except(completed).ToList();

            Assert.AreEqual(completed.Count, 3);
            Assert.AreEqual(completed2.Count, 3);
            Assert.AreEqual(active.Count, 1);
            Assert.AreEqual(active2.Count, 1);
        }


        [TestMethod]
        public void GettingAllTodoItems()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem("Apples");
            var todoItem2 = new TodoItem("Bananas");
            var todoItem3 = new TodoItem("Watermelons");
            var todoItem4 = new TodoItem("Hamburgers");
            var todoItem5 = new TodoItem("Eggs");

            repository.Add(todoItem1);
            repository.Add(todoItem2);
            repository.Add(todoItem3);
            repository.Add(todoItem4);

            Assert.AreEqual(repository.GetAll().Count, 4);
        }


        [TestMethod]
        public void RemovingNotExistingItemFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            Assert.IsTrue(repository.Get(todoItem.Id) == null);

            bool removed = repository.Remove(todoItem.Id);
            Assert.AreEqual(removed, false);
            Assert.IsTrue(repository.Get(todoItem.Id) == null);
        }


        [TestMethod]
        public void GettingFilteredTodoItems()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem("Apples");
            var todoItem2 = new TodoItem("Bananas");
            var todoItem3 = new TodoItem("Apricots");
            var todoItem4 = new TodoItem("Hamburgers");

            repository.Add(todoItem1);
            repository.Add(todoItem2);
            repository.Add(todoItem3);
            repository.Add(todoItem4);

            Func<TodoItem, bool> filterFunction = t => t.Text.StartsWith("A");
            var filtered = repository.GetFiltered(filterFunction).OrderBy(t => t.Text).ToList();

            Assert.AreEqual(filtered.Count, 2);
            Assert.AreEqual(filtered[0].Text, "Apples");
            Assert.AreEqual(filtered[1].Text, "Apricots");

        }

        [TestMethod]
        public void RemovingExistingItemFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);

            bool removed = repository.Remove(todoItem.Id);
            Assert.AreEqual(removed, true);
            Assert.IsTrue(repository.Get(todoItem.Id) == null);
        }

        [TestMethod]
        public void UpdatingNotExistingItemInDatabaseWillAddNewItemToDatabase()
        {
            ITodoRepository repository = new TodoRepository();

            var todoItem1 = new TodoItem("Groceries");
            repository.Add(todoItem1);

            var todoItem2 = new TodoItem("Bananas");
            Assert.IsTrue(repository.Get(todoItem2.Id) == null);

            repository.Update(todoItem2);

            var itemFromRepository1 = repository.Get(todoItem1.Id);
            Assert.AreEqual(itemFromRepository1, todoItem1);

            var itemFromRepository2 = repository.Get(todoItem2.Id);
            Assert.IsTrue(itemFromRepository2 != null);
            Assert.AreEqual(itemFromRepository2, todoItem2);
        }

        [TestMethod]
        public void UpdatingExistingItemInDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);

            todoItem.Text = "Bananas";
            repository.Update(todoItem);

            TodoItem itemFromRepository = repository.Get(todoItem.Id);
            Assert.AreEqual(itemFromRepository, todoItem);
        }


        

    }
}