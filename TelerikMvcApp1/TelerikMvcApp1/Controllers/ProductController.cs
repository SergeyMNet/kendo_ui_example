using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces;
using Ninject;
//using TelerikMvcApp1.Models;


namespace TelerikMvcApp1.Controllers
{
    public class ProductController : Controller
    {
        [Inject]
        public ICategoriesDAL CategoriesDal { get; set; }

        [Inject]
        public IDocumentsDAL DocumentsDal { get; set; }

        // GET: Product
        //public ActionResult Index()
        //{
        //    string r = Treeview(0, "", 0, 0);

        //    if (r.Last() == ',')
        //    {
        //        r = r.Substring(0, r.Count()-1);
        //        r += "]";
        //    }

        //    ViewData["treeviews"] = r;
        //    return View();
        //}

        //public ActionResult AngularView(string errorMessage, string okMessage)
        //{
        //    string r = Treeview(0, "", 0, 0);
        //    if (r.Last() == ',')
        //    {
        //        r = r.Substring(0, r.Count() - 1);
        //        r += "]";
        //    }
        //    ViewData["treeviews"] = r;

        //    ViewBag.ErrorMessage = errorMessage;
        //    ViewBag.OkMessage = okMessage;

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult SaveNode(string childid, string parentid)
        //{            
        //    childid = childid.Substring(0, childid.Length - 1);
        //    childid = childid.Trim();
        //    parentid = parentid.Substring(0, parentid.Length - 1);
        //    parentid = parentid.Trim();

        //    //*****REVISION******Do not allow reportsto = itself 
        //    if (childid != null && parentid != null && parentid != childid)
        //    {
        //        string source = childid;
        //        string destination = parentid;
        //        using (var ctx = new DocContext())
        //        {
        //            var category = ctx.Categories.Where(m => m.Name == source).First();

        //            var allParents = ctx.Categories.Where(m => m.Name == destination).ToList();
        //            if (allParents.Count() != 0)
        //            {
        //                var categoryParent = allParents.First();

        //                if (categoryParent.Parent_id != category.Id)
        //                {
        //                    category.Parent_id = Convert.ToInt16(categoryParent.Id);
        //                    ctx.SaveChanges();
        //                }
        //            }
        //            else
        //            {
        //                category.Parent_id = null;
        //                ctx.SaveChanges();
        //            }

                    
        //        }

        //    }
        //    return RedirectToAction("Index");
        //}

        public class AddNewChildCategoryModel
        {
            public string NewItem { get; set; }
            public int Parentid { get; set; }            
        }

        //[HttpPost]
        //public JsonResult AddNewChildCategory(AddNewChildCategoryModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        using (var ctx = new DocContext())
        //        {

        //            if (ctx.Categories.Any(c => c.Name == model.NewItem))
        //        {
        //            return Json("Item alredy Exist!");
        //        }

        //            if (ctx.Categories.Any(c => c.Id == model.Parentid))
        //        {
        //            ctx.Categories.Add(new Category { Name = model.NewItem, Parent_id = model.Parentid });
        //            ctx.SaveChanges();
        //            return Json("Save success!");
        //        }
        //        else
        //        {
        //            ctx.Categories.Add(new Category { Name = model.NewItem });
        //            ctx.SaveChanges();
        //            return Json("Save success!");
        //        }
        //    }
        //    }
            
        //    return Json("Error on create!");
        //}

        public class AddNewAfterCategoryModel
        {
            public string NewItem { get; set; }
            public int CategoryAfterId { get; set; }
        }

        //[HttpPost]
        //public JsonResult AddNewAfterCategory(AddNewAfterCategoryModel addNewAfterCategoryModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var ctx = new DocContext())
        //        {

        //            if (ctx.Categories.Any(c => c.Name == addNewAfterCategoryModel.NewItem))
        //            {
        //                return Json("Item alredy Exist!");
        //            }

        //            if (ctx.Categories.Any(c => c.Id == addNewAfterCategoryModel.CategoryAfterId) && ctx.Categories.Where(c => c.Id == addNewAfterCategoryModel.CategoryAfterId).FirstOrDefault().Parent_id.HasValue)
        //            {
        //                int dest = ctx.Categories.Where(c => c.Id == addNewAfterCategoryModel.CategoryAfterId).FirstOrDefault().Parent_id.Value;
        //                ctx.Categories.Add(new Category { Name = addNewAfterCategoryModel.NewItem, Parent_id = dest });
        //                ctx.SaveChanges();
        //                return Json("Save success!");
        //            }
        //            else
        //            {
        //                ctx.Categories.Add(new Category { Name = addNewAfterCategoryModel.NewItem });
        //                ctx.SaveChanges();

        //                return Json("Save success!");
        //            }
        //        }
        //    }

        //    return Json("Error on create!");
        //}

        //int mainNode = 0;
        //int childquantity = 0;
        //int myflag;

        //public string Treeview(int itemID, string mystr, int j, int flag)
        //{            
        //    List<Category> querylist = new List<Category>();
        //    var ctx = new DocContext();
            
        //    if (flag == 0)
        //    {

        //        querylist = (from m in ctx.Categories
        //                     where m.Parent_id == null
        //                     select m).ToList();
        //        mainNode = querylist.Count;

        //        mystr += "[";
        //    }
        //    if (flag == 1)
        //    {

        //        querylist = (from m in ctx.Categories
        //                     where m.Parent_id == itemID
        //                     select m).ToList();
        //        mainNode = querylist.Count;
        //        mystr += ",items:[";
        //    }

        //    //Below line shows an example of how to make parent node with his child
        //    //[{ id: "1", text: "P1", items: [{ id: "5", text: "P2" }] }]

        //    int i = 1;
        //    foreach (var item in querylist)
        //    {
        //        myflag = 0;
        //        mystr += "{id:\"" + item.Id +
        //        "\",text:\"" + item.Name +
        //        "\",imageUrl:\"" + item.Image +
        //        "\"";
        //        List<Category> querylistParent = new List<Category>();
        //        //Check this parent has child or not , if yes how many?
        //        querylistParent = (from m in ctx.Categories
        //                           where m.Parent_id == item.Id
        //                           select m).ToList();
        //        childquantity = querylistParent.Count;
        //        //If Parent Has Child again call Treeview with new parameter
        //        if (childquantity > 0)
        //        {
        //            mystr = Treeview(item.Id, mystr, i, 1);

        //        }
        //        //No Child and No Last Node
        //        else if (childquantity == 0 && i < querylist.Count)
        //        {
        //            mystr += "},";
        //        }
        //        //No Child and Last Node
        //        else if (childquantity == 0 && i == querylist.Count)
        //        {
        //            int fcheck2 = 0;
        //            int fcheck3 = 0;
        //            int counter = 0;
        //            int flagbreak = 0;

        //            int currentparent;
        //            List<Category> parentquery;
        //            List<Category> childlistquery;
        //            TempData["counter"] = 0;
        //            currentparent = Convert.ToInt16(item.Parent_id);
        //            int coun;
        //            while (currentparent != 0)
        //            {
        //                //count parent of parent

        //                fcheck2 = 0;
        //                fcheck3 = 0;
        //                parentquery = new List<Category>();
        //                parentquery = (from m in ctx.Categories
        //                               where m.Id == currentparent
        //                               select m).ToList();
        //                var rep2 = (from h in parentquery
        //                            select new { h.Parent_id }).First();

        //                //put {[ up to end

        //                //list of child
        //                childlistquery = new List<Category>();
        //                childlistquery = (from m in ctx.Categories
        //                                  where m.Parent_id == currentparent
        //                                  select m).ToList();

        //                foreach (var item22 in childlistquery)
        //                {
        //                    if (mystr.Contains(item22.Id.ToString()))
        //                    {

        //                        if (item22.Parent_id == currentparent)
        //                        {
        //                            fcheck3 += 1;
        //                            if (fcheck3 == 1)
        //                            {
        //                                counter += 1;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        myflag = 1;
        //                        if (item22.Parent_id == currentparent)
        //                        {
        //                            fcheck2 += 1;
        //                            if (fcheck2 == 1)
        //                            {
        //                                counter -= 1;
        //                                flagbreak = 1;
        //                            }
        //                        }
        //                    }
        //                }

        //                var result55 =
        //                (from h in parentquery select new { h.Id }).First();
        //                coun = Convert.ToInt16(result55.Id);
        //                TempData["coun"] = Convert.ToInt16(coun);
        //                currentparent = Convert.ToInt16(rep2.Parent_id);
        //                if (flagbreak == 1)
        //                {
        //                    break;
        //                }
        //            }

        //            for (int i2 = 0; i2 < counter; i2++)
        //            {
        //                mystr += "}]";
        //            }

        //            List<Category> lastchild = new List<Category>();
        //            lastchild = (from m in ctx.Categories
        //                         where m.Parent_id == item.Parent_id
        //                         select m).ToList();

        //            List<Category> lastparent = new List<Category>();
        //            lastparent = (from m in ctx.Categories
        //                          where m.Parent_id == null
        //                          select m).ToList();

        //            if (lastchild.Count > 0)
        //            {
        //                var result_lastchild =
        //                (from h in lastchild select new { h.Id }).Last();
        //                var result_lastparent =
        //                (from h in lastparent select new { h.Id }).Last();
        //                int mycount = Convert.ToInt16(TempData["coun"]);
        //                if (item.Id == result_lastchild.Id &&
        //                    //result_lastchild.Id == result_lastparent.Id && myflag == 0)
        //                mycount == result_lastparent.Id && myflag == 0)
        //                {
        //                    mystr += "}]";
        //                }
        //                else if (item.Id == result_lastchild.Id &&
        //                mycount == result_lastparent.Id && myflag == 1)
        //                {
        //                    mystr += "},";
        //                }
        //                else if (item.Id == result_lastchild.Id &&
        //                mycount != result_lastparent.Id)
        //                {
        //                    mystr += "},";
        //                }
        //            }
        //            //  finish }]
        //            else if (lastchild.Count == 0 && item.Parent_id == null)
        //            {
        //                mystr += "}]";
        //            }
        //        }
        //        i++;
        //    }

        //    //if (mystr.Last() == ',')
        //    //{
        //    //    mystr = mystr.Substring(0, mystr.Count()-1);
        //    //    mystr += "}]";
        //    //}

        //    return mystr;
        //}

        //public string Treeview2(int itemID, string mystr, int j, int flag)
        //{
        //    var dal = new DAL();
            

        //    List<Category> querylist = new List<Category>();
        //    //var ctx = new TreeviewEntities();

        //    if (flag == 0)
        //    {

        //        //querylist = (from m in ctx.Categorys
        //        //             where m.Parent_id == null
        //        //             select m).ToList();

        //        querylist = dal.GetAllCategory().Where(c => c.Parent_id == null).ToList();

        //        mainNode = querylist.Count;

        //        mystr += "[";
        //    }
        //    if (flag == 1)
        //    {

        //        querylist = dal.GetAllCategory().Where(c => c.Parent_id == itemID).ToList();
        //            //(from m in ctx.Categorys
        //            //         where m.Parent_id == itemID
        //            //         select m).ToList();

        //        mainNode = querylist.Count;
        //        mystr += ",items:[";
        //    }

        //    //Below line shows an example of how to make parent node with his child
        //    //[{ id: "1", text: "P1", items: [{ id: "5", text: "P2" }] }]

        //    int i = 1;
        //    foreach (var item in querylist)            
        //    {
        //        myflag = 0;
        //        mystr += "{id:\"" + item.Id +
        //        "\",text:\"" + item.Name + "\"";
        //        List<Category> querylistParent = new List<Category>();
        //        //Check this parent has child or not , if yes how many?
        //        querylistParent = dal.GetAllCategory().Where(c => c.Parent_id == item.Id).ToList();
        //            //(from m in ctx.Categorys
        //            //               where m.Parent_id == item.ID
        //            //               select m).ToList();
        //        childquantity = querylistParent.Count;
        //        //If Parent Has Child again call Treeview with new parameter
        //        if (childquantity > 0)
        //        {
        //            mystr = Treeview(item.Id, mystr, i, 1);

        //        }
        //        //No Child and No Last Node
        //        else  if (childquantity == 0 && i < querylist.Count)
        //        {
        //            mystr += "},";
        //        }
        //        //No Child and Last Node
        //        else if (childquantity == 0 && i == querylist.Count)
        //        {
        //            int fcheck2 = 0;
        //            int fcheck3 = 0;
        //            int counter = 0;
        //            int flagbreak = 0;

        //            int currentparent;
        //            List<Category> parentquery;
        //            List<Category> childlistquery;
        //            TempData["counter"] = 0;
        //            currentparent = Convert.ToInt16(item.Parent_id);
        //            int coun;
        //            while (currentparent != 0)
        //            {
        //                //count parent of parent

        //                fcheck2 = 0;
        //                fcheck3 = 0;
        //                parentquery = new List<Category>();
        //                parentquery = dal.GetAllCategory().Where(c => c.Id == currentparent).ToList();
        //                    //(from m in ctx.Categorys
        //                    //           where m.ID == currentparent
        //                    //           select m).ToList();

        //                var rep2 = (from h in parentquery
        //                            select new { h.Parent_id }).First();

        //                //put {[ up to end

        //                //list of child
        //                childlistquery = new List<Category>();
        //                childlistquery = dal.GetAllCategory().Where(c => c.Parent_id == currentparent).ToList();
        //                    //(from m in ctx.Categorys
        //                    //              where m.Parent_id == currentparent
        //                    //              select m).ToList();

        //                foreach (var item22 in childlistquery)
        //                {
        //                    if (mystr.Contains(item22.Id.ToString()))
        //                    {

        //                        if (item22.Parent_id == currentparent)
        //                        {
        //                            fcheck3 += 1;
        //                            if (fcheck3 == 1)
        //                            {
        //                                counter += 1;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        myflag = 1;
        //                        if (item22.Parent_id == currentparent)
        //                        {
        //                            fcheck2 += 1;
        //                            if (fcheck2 == 1)
        //                            {
        //                                counter -= 1;
        //                                flagbreak = 1;
        //                            }
        //                        }
        //                    }
        //                }

        //                var result55 =
        //                (from h in parentquery select new { h.Id }).First();
        //                coun = Convert.ToInt16(result55.Id);
        //                TempData["coun"] = Convert.ToInt16(coun);
        //                currentparent = Convert.ToInt16(rep2.Parent_id);
        //                if (flagbreak == 1)
        //                {
        //                    break;
        //                }
        //            }

        //            for (int i2 = 0; i2 < counter; i2++)
        //            {
        //                mystr += "}]";
        //            }

        //            List<Category> lastchild = new List<Category>();
        //            lastchild = dal.GetAllCategory().Where(c => c.Parent_id == item.Parent_id).ToList();
        //                //(from m in ctx.Categorys
        //                //         where m.Parent_id == item.Parent_id
        //                //         select m).ToList();

        //            List<Category> lastparent = new List<Category>();
        //            lastparent = dal.GetAllCategory().Where(c => c.Parent_id == null).ToList();
        //                //(from m in ctx.Categorys
        //                //          where m.Parent_id == null
        //                //          select m).ToList();

        //            if (lastchild.Count > 0)
        //            {
        //                var result_lastchild =
        //                (from h in lastchild select new { h.Id }).Last();
        //                var result_lastparent =
        //                (from h in lastparent select new { h.Id }).Last();
        //                int mycount = Convert.ToInt16(TempData["coun"]);
        //                if (item.Id == result_lastchild.Id &&
        //                mycount == result_lastparent.Id && myflag == 0)
        //                {
        //                    mystr += "}]";
        //                }
        //                else if (item.Id == result_lastchild.Id &&
        //                mycount == result_lastparent.Id && myflag == 1)
        //                {
        //                    mystr += "},";
        //                }
        //                else if (item.Id == result_lastchild.Id &&
        //                mycount != result_lastparent.Id)
        //                {
        //                    mystr += "},";
        //                }
        //            }
        //            //  finish }]
        //            else if (lastchild.Count == 0 && item.Parent_id == null)
        //            {
        //                mystr += "}]";
        //            }
        //        }
        //        i++;
        //    }
            
        //    return mystr;
        //}

        //public JsonResult GetAllDocumets([DataSourceRequest] DataSourceRequest request, int? CategoryId)
        //{
        //    var dal = new DAL();

        //    var result = dal.GetAllDocuments().ToList();

        //    if(CategoryId != null)
        //    {
        //        result = result.Where(c => c.CategoryId == CategoryId).ToList();
        //    }


        //    return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //}
    }
}