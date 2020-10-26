using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLib.Model;
using RestItemService.Controllers;

namespace ItemTest
{
    [TestClass]
    public class UnitTest1
    {
        private ItemsController cntr = null;

        [TestInitialize]
        public void BeforeEachTest()
        {
            cntr = new ItemsController();
        }

        [TestMethod]
        public void TestGetMethod()
        {
            // Arrange
            // i BeforeEachTest

            // Act
            List<Item> Ilist = new List<Item>(cntr.Get());

            // Assert
            Assert.AreEqual(4, Ilist.Count);
        }

        //[TestMethod]
        //public void TestGetMethod2()
        //{
        //    List<Item> beforeIlist = new List<Item>(cntr.Get());
        //    cntr.Get(1);
        //    List<Item> afterIlist = new List<Item>(cntr.Get());

        //    Assert.AreEqual(beforeIlist.Count, afterIlist.Count);

        //}

        [TestMethod]
        public void TestPostMethod()
        {
            Item item6 = new Item(6, "Beer", "Middle", 2.5);

            List<Item> beforeIlist = new List<Item>(cntr.Get());
            cntr.Post(item6);
            List<Item> afterIlist = new List<Item>(cntr.Get());

            Assert.AreEqual(beforeIlist.Count+1, afterIlist.Count);
        }

        [TestMethod]
        public void TestPutMethod()
        {
            Item item6 = new Item(2, "Beer", "Middle", 2.5);

            List<Item> beforeIlist = new List<Item>(cntr.Get());
            cntr.Put(2, item6);
            List<Item> afterIlist = new List<Item>(cntr.Get());

            Assert.AreEqual(beforeIlist.Count, afterIlist.Count);
        }

        [TestMethod]
        public void TestDeleteMethod()
        {
            List<Item> beforeIlist = new List<Item>(cntr.Get());
            cntr.Delete(2);
            List<Item> afterIlist = new List<Item>(cntr.Get());

            Assert.AreEqual(beforeIlist.Count - 1, afterIlist.Count);
        }
    }
}
