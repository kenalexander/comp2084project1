using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using COMP2084Project1.Controllers;
using COMP2084Project1.Models;
using System.Collections.Generic;
using System.Linq;

namespace COMP2084Project1.Tests.Controllers
{
    [TestClass]
    public class ArtTableControllerTest
    {
        Mock<IArtMock> mock;
        List<artTable> artTables;
        artTablesController controller;

        [TestMethod]
        public void TestInitialize()
        {
            mock = new Mock<IArtMock>();

            artTables = new List<artTable>
            {
                new artTable
                {
                    TitleID = 100, Title = "Mona Lisa", Artist = "Leonardo da Vinci", Year = "1503", museumTable = new museumTable
                    {
                        MuseumID = 4000, Name = "Louvre"
                    }
                },
                new artTable
                {
                    TitleID = 200, Title = "Girl with a Pearl Earring", Artist = "Johannes Vermeer", Year = "1665", museumTable = new museumTable
                    {
                        MuseumID = 4001, Name = "Mauritshuis"
                    }
                },
                new artTable
                {
                    TitleID = 300, Title = "The Starry Night", Artist = "Vincent van Gogh", Year = "1889", museumTable = new museumTable
                    {
                        MuseumID = 4002, Name = "Museum of Modern Art"
                    }
                }
            };

            mock.Setup(m => m.artTables).Returns(artTables.AsQueryable());
            controller = new artTablesController(mock.Object);
        }
        
        [TestMethod]
        public void IndexReturnsView()
        {

            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsArtTable()
        {
            var actual = (List<artTable>)((ViewResult)controller.Index()).Model;

            CollectionAssert.AreEqual(artTables.OrderBy(a => a.Title).ThenBy(a => a.Artist).ToList(), actual);
        }
        
        [TestMethod]
        public void DetailsNoId()
        {
            // act
            var result = (ViewResult)controller.Details(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            // act
            var result = (ViewResult)controller.Details(67830);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidId()
        {
            // act - cast the model as an Album object
            artTable actual = (artTable)((ViewResult)controller.Details(300)).Model;

            // assert - is this the first mock album in our array?
            Assert.AreEqual(artTables[2], actual);
        }

        [TestMethod]
        public void DetailsViewLoads()
        {
            // act
            ViewResult result = (ViewResult)controller.Details(300);

            // assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void EditNoId()
        {
            // arrange
            int? id = null;

            // act 
            var result = (ViewResult)controller.Edit(id);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidId()
        {
            // act
            var result = (ViewResult)controller.Edit(8983);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditViewLoads()
        {
            // act
            ViewResult actual = (ViewResult)controller.Edit(100);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        [TestMethod]
        public void EditLoadsArtTable ()
        {
            // act
            artTable actual = (artTable)((ViewResult)controller.Edit(100)).Model;

            // assert
            Assert.AreEqual(artTables[0], actual);
        }
        [TestMethod]
        public void CreateViewLoads()
        {
            // act
            var result = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void DeleteNoId()
        {
            // act
            var result = (ViewResult)controller.Delete(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidId()
        {
            // act
            var result = (ViewResult)controller.Delete(3739);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Delete(100);

            // assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsArtTable()
        {
            // act
            artTable result = (artTable)((ViewResult)controller.Delete(100)).Model;

            // assert
            Assert.AreEqual(artTables[0], result);
        }

        [TestMethod]
        public void EditPostLoadsIndex()
        {
            // act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(artTables[0]);

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EditPostInvalidLoadsView()
        {
            // arrange
            artTable invalid = new artTable { TitleID = 999999999 };
            controller.ModelState.AddModelError("Error", "Won't Save");

            // act
            ViewResult result = (ViewResult)controller.Edit(invalid);

            // assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditPostInvalidLoadsArtTable()
        {
            // arrange
            artTable invalid = new artTable { TitleID = 100 };
            controller.ModelState.AddModelError("Error", "Won't Save");

            // act
            artTable result = (artTable)((ViewResult)controller.Edit(invalid)).Model;

            // assert
            Assert.AreEqual(invalid, result);
        }

        [TestMethod]
        public void CreateValidAlbum()
        {
            // arrange
            artTable newAlbum = new artTable
            {
                TitleID = 400,
                Title = "The Last Supper",
                Year = "1495",
                museumTable = new museumTable
                {
                    MuseumID = 4004,
                    Name = "Santa Maria delle Grazie"
                }
            };

            // act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(newAlbum);

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateInvalidArtTable()
        {
            // arrange
            artTable invalid = new artTable();

            // act
            controller.ModelState.AddModelError("Cannot create", "create exception");
            ViewResult result = (ViewResult)controller.Create(invalid);

            // assert
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedNoId()
        {
            // act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {
            // act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(77777);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            // act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(100);

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
