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
        public void MarkingExistingUncompleteItemAsCompletedReturnsTrue()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            repository.Add(todoItem);

            var markedAsCompleted = repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(markedAsCompleted, true);
        }


        [TestMethod]
        public void MarkingExistingCompleteItemAsCompletedReturnsFalse()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            repository.Add(todoItem);
            repository.MarkAsCompleted(todoItem.Id);

            var markedAsCompleted = repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(markedAsCompleted, false);
        }


        [TestMethod]
        public void MarkingNotExistingItemAsCompletedReturnsFalse()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            var markedAsCompleted = repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(markedAsCompleted, false);
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