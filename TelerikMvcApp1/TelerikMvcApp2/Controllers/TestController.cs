using DAL;
using DAL.Interfaces;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMvcApp2.Models;
using System.Globalization;
using System.Diagnostics;
using System.IO;

namespace TelerikMvcApp2.Controllers
{
    public class TestController : Controller
    {        
        [Inject]
        public IDAL Dal { get; set; }

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: Test_Html
        public ActionResult Test_Html()
        {               
            return View();
        }

        #region Grid

        public JsonResult GetGrid([DataSourceRequest] DataSourceRequest request, string targetCategory = "", string filterStatus = "", string filterDate = "", string filterName = "")
        {
            try
            {
                var all = Dal.GetAllDocuments().ToList();
                var res = new List<DocumentView>();

                if (!String.IsNullOrEmpty(targetCategory))
                {
                    try
                    {
                        int idCategory = Dal.GetCategoryByName(targetCategory).Id;
                        res = all.Where(d => d.CategoryId == idCategory).ToList().Select(d => new DocumentView(d)).ToList();

                        if (!String.IsNullOrEmpty(filterStatus))
                        {
                            Status statusFiltr = (Status)Enum.Parse(typeof(Status), filterStatus);
                            res = res.Where(d => d.Status == statusFiltr).ToList();
                        }
                        if (!String.IsNullOrEmpty(filterDate))
                        {
                            DateTime dateFiltr = DateTime.Parse(filterDate, CultureInfo.InvariantCulture);
                            res = res.Where(d => d.Upload_date > dateFiltr).ToList();
                        }
                        if (!String.IsNullOrEmpty(filterName))
                        {
                            res = res.Where(d => d.Name == filterName).ToList();
                        }

                    }
                    catch (Exception ex)
                    {
                        res = all.Select(d => new DocumentView(d)).ToList();
                        Console.WriteLine("---------------Error:------------");
                        Console.WriteLine("---------------" + ex.Message);
                    }
                }
                else
                {
                    res = all.Select(d => new DocumentView(d)).ToList();
                }

                return Json(res.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("---------------Error:------------");
                Console.WriteLine("---------------" + ex.Message);
                var res = new List<DocumentView>();
                return Json(res.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        #region Add-Update-Delete

        //---add new doc
        public JsonResult AddNewDocument(string newDocName, string parentName)
        {
            try
            {
                if (!String.IsNullOrEmpty(newDocName) && !String.IsNullOrEmpty(parentName))
                {
                    var parentFolder = Dal.GetCategoryByName(parentName);
                    if (parentFolder != null)
                    {
                        Document newDoc = new Document()
                        {
                            CategoryId = parentFolder.Id,
                            Extension = "",
                            Description = "",
                            Name = newDocName,
                            Upload_date = DateTime.Now,
                            Status = Status.Unapproved
                        };
                        if (!Dal.IsUniqueDocument(newDoc))
                        {
                            return Json(new ResultMessage(EnumErrorCode.AlredyExist, "Create doc alredy Exist!"), JsonRequestBehavior.AllowGet);
                        }

                        Dal.InsertDocument(newDoc);
                        return Json(new ResultMessage("Create_doc success!"), JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on Create_doc !"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage(EnumErrorCode.ErrorOnInsert, "Error on Add_doc!: " + ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //---create doc
        public JsonResult Create_Doc(DocumentView newDoc)
        {
            try
            {
                if (newDoc != null)
                {
                    var parentFolder = Dal.GetCategoriesById(newDoc.Category.CategoryID);
                    if (parentFolder != null)
                    {
                        Document newDocument = new Document
                        {
                            Id = newDoc.Id,
                            Name = newDoc.Name,
                            Description = newDoc.Description,
                            CategoryId = newDoc.Category.CategoryID,
                            Status = newDoc.Status,
                            Extension = newDoc.Extension,
                            Upload_date = DateTime.Now
                        };

                        if (!Dal.IsUniqueDocument(newDocument))
                        {
                            return Json(new ResultMessage(EnumErrorCode.AlredyExist, "Create doc alredy Exist!"), JsonRequestBehavior.AllowGet);
                        }
                        Dal.InsertDocument(newDocument);
                        return Json(new ResultMessage("Create_doc success!"), JsonRequestBehavior.AllowGet);
                    }
                    return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on Create_doc! Parent_folder Not Exist!"), JsonRequestBehavior.AllowGet);
                }
                return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on Create_doc!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("---------------Error:------------");
                Console.WriteLine("---------------" + ex.Message);
                return Json(new ResultMessage(EnumErrorCode.ErrorOnInsert, "Error on Create_doc!"), JsonRequestBehavior.AllowGet);                
            }
        }

        //---update doc
        public JsonResult Update_Doc(DocumentView newDoc)
        {
            try
            {
                if (newDoc != null)
                {
                    var parentFolder = Dal.GetCategoriesById(newDoc.Category.CategoryID);
                    if (parentFolder != null)
                    {
                        Document newDocument = Dal.GetDodumentById(newDoc.Id);

                        newDocument.Name = newDoc.Name;
                        newDocument.Description = newDoc.Description;                        
                        newDocument.Status = newDoc.Status;
                        newDocument.Extension = newDoc.Extension.HasValue() ? newDoc.Extension : "";
                        newDocument.Upload_date = DateTime.Now;
                        newDocument.CategoryId = newDoc.Category.CategoryID;
                        
                        if (!Dal.IsUniqueDocument(newDocument))
                        {
                            return Json(new ResultMessage(EnumErrorCode.AlredyExist, "Doc alredy Exist!"), JsonRequestBehavior.AllowGet);
                        }
                        Dal.UpdateDocument(newDocument);

                        return Json(new ResultMessage("Update_doc success!"), JsonRequestBehavior.AllowGet);                        
                    }
                    return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on Update_doc! Parent_folder Not Exist!"), JsonRequestBehavior.AllowGet);
                }
                return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on Update_doc!"), JsonRequestBehavior.AllowGet);                
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage(EnumErrorCode.ErrorOnUpdate, "Error on Update_doc! " + ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //---delete doc
        public JsonResult Delete_Doc(DocumentView newDoc)
        {
            try
            {
                if (newDoc != null)
                {
                    var document = Dal.GetDodumentById(newDoc.Id);
                    Dal.DeleteDocument(document);

                    return Json(new ResultMessage("Delete_doc success!"), JsonRequestBehavior.AllowGet);                    
                }
                return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on Delete_doc!"), JsonRequestBehavior.AllowGet);                
            }
            catch(Exception ex){
                return Json(new ResultMessage(EnumErrorCode.ErrorOnDelete, "Error on Delete_doc!: " + ex.Message), JsonRequestBehavior.AllowGet);                                
            }
        }
        
        #endregion

        public JsonResult GetAllCategoriesVM()
        {
            var categories = Dal.GetAllCategory()
                        .Select(c => new CategoryViewModel
                        {
                            CategoryID = c.Id,
                            CategoryName = c.Name
                        })
                        .OrderBy(e => e.CategoryName).ToList();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllStatusVM()
        {
            var status = new List<StatusVM> 
            {   new StatusVM { StatusID = 1, StatusName = "Unapproved" }, 
                new StatusVM { StatusID = 2, StatusName = "Approved" }, 
                new StatusVM { StatusID = 3, StatusName = "Archived" } };

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// загрузка и хранение файлов в базе данных
        /// </summary>
        /// <param name="content">файл</param>
        /// <param name="docName">имя документа</param>
        /// <returns> сохраняет файл в базе данных </returns>
        public ActionResult UploadFileToDb(HttpPostedFileBase content, string docName)
        {
            try
            {
                if (content != null || !String.IsNullOrEmpty(docName))
                {
                    var curDoc = Dal.GetDodumentByName(docName);

                    var extension = Path.GetExtension(content.FileName);
                    byte[] fileData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(content.InputStream))
                    {
                        fileData = binaryReader.ReadBytes(content.ContentLength);
                    }
                    
                    // установка массива байтов
                    curDoc.File = fileData;
                    curDoc.Extension = extension;

                    Dal.UpdateDocument(curDoc);
                    
                    return Content("");
                }
                else
                {
                    return Content("Error");
                }
            }
            catch (Exception ex)
            {
                return Content("Error" + ex.Message);
            }
        }

        /// <summary>
        /// Получить файл с базы данных
        /// </summary>
        /// <param name="docName">имя документа</param>
        /// <returns> Возвращает вайл </returns>
        public FileContentResult GetFile(string docName)
        {
            try
            {
                if (!String.IsNullOrEmpty(docName))
                {
                    var curDoc = Dal.GetDodumentByName(docName);
                    var file = new FileContentResult(curDoc.File, curDoc.Extension);
                    file.FileDownloadName = String.Format("{0}{1}", curDoc.Name, file.ContentType);
                    return file;                    
                }
                else
                {
                    Console.WriteLine("---------------Error:------------");
                    Console.WriteLine("---------------GetFile => docName == null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("---------------Error:------------");
                Console.WriteLine("---------------" + ex.Message);
                return null;
            }
        }

        #endregion
        
        #region Tree

        #region Add-Save-Delete
       
        //--Add Next
        [HttpPost]
        public JsonResult AddNewNextCategory(string newItemName, string parentName)
        {
            try
            {
                if (!String.IsNullOrEmpty(newItemName))
                {
                    if (!Dal.IsUniqueCategory(newItemName))
                    {
                        return Json(new ResultMessage(EnumErrorCode.AlredyExist, "Create category alredy Exist!"), JsonRequestBehavior.AllowGet);
                    }
                    var parent = Dal.GetCategoryByName(parentName);
                    if (parent != null && parent.Parent_id.HasValue)
                    {
                        var last = Dal.GetAllCategory().Where(c => c.Parent_id == parent.Parent_id && c.RightNode == 0).FirstOrDefault();
                        if (last != null)
                        {
                            Dal.InsertCategory(new Category { Name = newItemName, Parent_id = parent.Parent_id, LeftNode = last.Id, RightNode = 0 });
                            var curent = Dal.GetAllCategory().Where(c => c.LeftNode == last.Id).FirstOrDefault();
                            last.RightNode = curent.Id;
                            Dal.UpdateCategory(last);
                        }
                        else
                        {
                            Dal.InsertCategory(new Category { Name = newItemName, Parent_id = parent.Parent_id, LeftNode = 0, RightNode = 0 });
                        }

                        return Json(new ResultMessage("Category create success!"), JsonRequestBehavior.AllowGet);                        
                    }
                    else
                    {
                        var last = Dal.GetAllCategory().Where(c => c.Parent_id == null && c.RightNode == 0).FirstOrDefault();
                        if (last != null)
                        {
                            Dal.InsertCategory(new Category { Name = newItemName, Parent_id = null, LeftNode = last.Id, RightNode = 0 });
                            var curent = Dal.GetAllCategory().Where(c => c.LeftNode == last.Id).FirstOrDefault();
                            last.RightNode = curent.Id;
                            Dal.UpdateCategory(last);
                        }
                        else
                        {
                            Dal.InsertCategory(new Category { Name = newItemName, Parent_id = null, LeftNode = 0, RightNode = 0 });
                        }

                        return Json(new ResultMessage("Category create success!"), JsonRequestBehavior.AllowGet);                        
                    }
                }
                return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on create category!"), JsonRequestBehavior.AllowGet);                
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage(EnumErrorCode.ErrorOnInsert, "Error on create category!" + ex.Message), JsonRequestBehavior.AllowGet);
            }            
        }

        //---Add Child
        [HttpPost]
        public JsonResult AddNewChildCategory(string newItemName, string parentName)
        {
            try
            {
                if (!String.IsNullOrEmpty(newItemName) && !String.IsNullOrEmpty(parentName))
                {
                    if (!Dal.IsUniqueCategory(newItemName))
                    {
                        return Json(new ResultMessage(EnumErrorCode.AlredyExist, "Create category alredy Exist!"), JsonRequestBehavior.AllowGet);                        
                    }

                    var parent = Dal.GetCategoryByName(parentName);
                    if (parent != null)
                    {
                        var last = Dal.GetAllCategory().Where(c => c.Parent_id == parent.Id && c.RightNode == 0).FirstOrDefault();
                        if (last != null)
                        {
                            Dal.InsertCategory(new Category { Name = newItemName, Parent_id = parent.Id, LeftNode = last.Id, RightNode = 0 });
                            var curent = Dal.GetAllCategory().Where(c => c.LeftNode == last.Id).FirstOrDefault();
                            last.RightNode = curent.Id;
                            Dal.UpdateCategory(last);
                        }
                        else
                        {
                            Dal.InsertCategory(new Category { Name = newItemName, Parent_id = parent.Id, LeftNode = 0, RightNode = 0 });
                        }

                        return Json(new ResultMessage("Category create success!"), JsonRequestBehavior.AllowGet);
                    }                    
                }
                return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on create category!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage(EnumErrorCode.ErrorOnInsert, "Error on create category!" + ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //---Save DropTree
        public JsonResult SaveNodeTree(string childid, string parentid, string position)
        {
            try
            {
                if (ChangePositionFolder(childid, parentid, position))
                {                    
                    return Json(new ResultMessage("Change NodeTree success!"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new ResultMessage(EnumErrorCode.ErrorOnInsert, "Error on change NodeTree!"), JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex){
                return Json(new ResultMessage(EnumErrorCode.ErrorOnInsert, "Error on change NodeTree!" + ex.Message), JsonRequestBehavior.AllowGet);                
            }
        }

        //---Remove
        [HttpPost]
        public JsonResult RemoveCategory(string targetItemName)
        {
            try
            {
                if (!String.IsNullOrEmpty(targetItemName))
                {
                    //---убрать из дерева, обьеденить позиции
                    var source = Dal.GetCategoryByName(targetItemName);
                    RemoveFromTree(source);

                    string result = "";
                    RemoveAll(targetItemName, ref result);

                    return Json(new ResultMessage("Delete success!: " + result), JsonRequestBehavior.AllowGet);                                    
                }
                return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on delete category!"), JsonRequestBehavior.AllowGet);                
            }            
            catch(Exception ex){
                return Json(new ResultMessage(EnumErrorCode.ErrorOnDelete, "Error on delete category!" + ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        private bool RemoveAll(string targetItemName, ref string result)
        {
            var removeItem = Dal.GetCategoryByName(targetItemName);

            var childernItems = Dal.GetAllCategory().Where(c => c.Parent_id == removeItem.Id).ToList();
            if (childernItems.Any())
            {
                foreach (var item in childernItems)
                {
                    RemoveAll(item.Name, ref result);
                }
            }
            
            result += " " + removeItem.Name;
            
            var childernDocs = Dal.GetAllDocuments().Where(d => d.Category.Id == removeItem.Id).ToList();            
            foreach(var d in childernDocs)
            {
                //var map_path = Server.MapPath(PathFiles);
                //FileManager.TryDeleteFile(d.Name, map_path);

                Dal.DeleteDocument(d);
            }

            Dal.DeleteCategory(removeItem);
            
            return true;
        }

        //---Edit
        [HttpPost]
        public JsonResult EditNameFolder(string oldName, string newName)
        {
            try
            {
                if (!String.IsNullOrEmpty(oldName) && !String.IsNullOrEmpty(newName))
                {
                    if (!Dal.IsUniqueCategory(newName))
                    {
                        return Json(new ResultMessage(EnumErrorCode.AlredyExist, "Folder alredy Exist!"), JsonRequestBehavior.AllowGet);
                    }

                    Category editItem = Dal.GetCategoryByName(oldName);
                    editItem.Name = newName;
                    Dal.UpdateCategory(editItem);

                    return Json(new ResultMessage("Edit success!: " + editItem.Name), JsonRequestBehavior.AllowGet);                    
                }
                return Json(new ResultMessage(EnumErrorCode.InvalidParameter, "Error on edit category!"), JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex){
                return Json(new ResultMessage(EnumErrorCode.ErrorOnUpdate, "Error on edit category!" + ex.Message), JsonRequestBehavior.AllowGet);                
            }
        } 

        #endregion
        
        #region GetTree
        public JsonResult GetTreeView()
        {
            try
            {
                var res = GetTree(0, new List<CategoryView>(), 0);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                Console.WriteLine("---------------Error:------------");
                Console.WriteLine("---------------" + ex.Message);

                var res = new List<CategoryView>();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        int mainNode = 0;
        int myflag;        
        public List<CategoryView> GetTree(int itemID, List<CategoryView> mystr, int flag)
        {
            var dal = Dal;
            List<CategoryView> querylist = new List<CategoryView>();

            if (flag == 0)
            {
                var all = dal.GetAllCategory().Where(c => c.Parent_id == null).ToList();

                querylist.Add(all.Where(c => c.LeftNode == 0).Select(o => new CategoryView(o)).FirstOrDefault());

                for (int i = 1; i < all.Count(); i++)
                {
                    var it = querylist.LastOrDefault().rightNode;
                    querylist.Add(all.Where(c => c.Id == it).Select(o => new CategoryView(o)).FirstOrDefault());
                }

                mainNode = querylist.Count;
            }
            if (flag == 1)
            {
                var all = dal.GetAllCategory().Where(c => c.Parent_id == itemID).ToList();

                querylist.Add(all.Where(c => c.LeftNode == 0).Select(o => new CategoryView(o)).FirstOrDefault());

                for (int i = 1; i < all.Count(); i++)
                {
                    var it = querylist.LastOrDefault().rightNode;
                    querylist.Add(all.Where(c => c.Id == it).Select(o => new CategoryView(o)).FirstOrDefault());
                }

                mainNode = querylist.Count;
            }

            foreach (var item in querylist)
            {
                myflag = 0;
                mystr.Add(item);
                var querylistParent = dal.GetAllCategory().Where(c => c.Parent_id == item.id).ToList();

                if (querylistParent.Count() > 0)
                {
                    mystr.Last().items = new List<CategoryView>();
                    mystr.Last().items.AddRange(GetTree(item.id, new List<CategoryView>(), 1));
                }
            }

            return mystr;
        }

        #endregion

        public bool ChangePositionFolder(string child, string parent, string position)
        {
            var source = Dal.GetCategoryByName(child);
            var destination = Dal.GetCategoryByName(parent);

            //---убрать из дерева, обьеденить позиции
            RemoveFromTree(source);

            if (position == "over")
            {
                PosOver(source, destination);
            }
            else if (position == "after")
            {
                PosAfter(source, destination);
            }
            else if (position == "before")
            {
                PosBefore(source, destination);
            }
            else
            {
                return false;
            }

            SaveParentId(source, destination, position);

            return true;
        }

        #region Helpers_for_ChangePositionFolder

        public bool RemoveFromTree(Category source)
        {
            var left = Dal.GetCategoriesById((long)source.LeftNode);
            var right = Dal.GetCategoriesById((long)source.RightNode);

            if (left != null && right != null)
            {
                left.RightNode = right.Id;
                right.LeftNode = left.Id;
                Dal.UpdateCategory(left);
                Dal.UpdateCategory(right);
            }
            else if (left == null && right != null)
            {
                right.LeftNode = 0;
                Dal.UpdateCategory(right);
            }
            else if (left != null && right == null)
            {
                left.RightNode = 0;
                Dal.UpdateCategory(left);
            }

            return true;
        }

        public bool SaveParentId(Category source, Category destination, string position)
        {
            if (position == "over")
            {
                source.Parent_id = destination.Id;
            }
            else
            {
                source.Parent_id = destination.Parent_id;
            }

            Dal.UpdateCategory(source);

            return true;
        }

        public bool PosOver(Category source, Category destination)
        {
            //---Проверить детей в папке назначения
            var all = Dal.GetAllCategory().Where(c => c.Parent_id == destination.Id).ToList();
            if (all.Count() > 0)
            {
                //---последний сосед
                var last = all.OrderByDescending(o => o.LeftNode).FirstOrDefault();
                source.LeftNode = last.Id;
                source.RightNode = 0;

                last.RightNode = source.Id;

                Dal.UpdateCategory(last);
                Dal.UpdateCategory(source);
            }
            //---если нет, все соседи в ноль
            else
            {
                source.LeftNode = 0;
                source.RightNode = 0;
            }

            return true;
        }
        public bool PosAfter(Category source, Category destination)
        {
            var last = Dal.GetAllCategory().Where(c => c.Parent_id == destination.Parent_id && c.Id == destination.RightNode).FirstOrDefault();
            if (last != null)
            {
                last.LeftNode = source.Id;
                destination.RightNode = source.Id;
                source.LeftNode = destination.Id;
                source.RightNode = last.Id;

                Dal.UpdateCategory(last);
                Dal.UpdateCategory(destination);
                Dal.UpdateCategory(source);
            }
            else
            {
                destination.RightNode = source.Id;
                source.LeftNode = destination.Id;
                source.RightNode = 0;

                Dal.UpdateCategory(destination);
                Dal.UpdateCategory(source);
            }

            return true;
        }
        public bool PosBefore(Category source, Category destination)
        {
            var first = Dal.GetAllCategory().Where(c => c.Parent_id == destination.Parent_id && c.Id == destination.LeftNode).FirstOrDefault();

            if (first != null)
            {
                first.RightNode = source.Id;

                source.LeftNode = first.Id;
                source.RightNode = destination.Id;

                destination.LeftNode = source.Id;

                Dal.UpdateCategory(first);
                Dal.UpdateCategory(destination);
                Dal.UpdateCategory(source);
            }
            else
            {
                destination.LeftNode = source.Id;
                source.LeftNode = 0;
                source.RightNode = destination.Id;

                Dal.UpdateCategory(destination);
                Dal.UpdateCategory(source);
            }

            return true;
        }

        #endregion

        #endregion
        

        #region [Dispose]

        protected override void Dispose(bool disposing)
        {
            Dal.Dispose();
            base.Dispose(disposing);
        }

        #endregion
             
    }
}