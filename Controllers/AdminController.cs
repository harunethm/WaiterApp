using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaiterApp.Models.BLL;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        bool IsAdmin()
        {
            return BllOrtak.Sessions.roleID == 1;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (IsAdmin())
                return View("Products");
            else
                return RedirectToAction("Index", "Login");
        }

        #region products
        [HttpGet]
        public ActionResult Products()
        {
            if (IsAdmin())
            {
                BllCategory bllCategory = new BllCategory();
                BllProduct bllProduct = new BllProduct();

                List<mMenu> mMenu = new List<mMenu>();

                List<mCategory> mCategories = bllCategory.GetAllmCategories();
                foreach (var item in mCategories)
                {
                    List<mProduct> mProducts = bllProduct.GetAllmProductsByCategory(item.ID);
                    if (mProducts.Count > 0)
                        mMenu.Add(new mMenu
                        {
                            category = item,
                            products = mProducts,
                        });
                }

                ViewBag.categories = mCategories;
                ViewBag.mMenu = mMenu;
                return View();
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public JsonResult AddUpdateProduct(int ID = 0, int categoryID = 0, string name = "", string description = "", string price = "", bool status = true)
        {
            BllProduct bllProduct = new BllProduct();
            if (ID > 0)
            {
                // update
                product product = bllProduct.GetProduct(ID);
                if (product != null)
                {
                    if (categoryID <= 0)
                        return Json(new { confirm = false, errorMessage = "Seçilen kategori bulunamadı." }, JsonRequestBehavior.AllowGet);
                    if (name.IsNullOrEmpty())
                        return Json(new { confirm = false, errorMessage = "Ad alanı boş bırakılmamalıdır." }, JsonRequestBehavior.AllowGet);
                    if (description.IsNullOrEmpty())
                        return Json(new { confirm = false, errorMessage = "Açıklama alanı boş bırakılmamalıdır." }, JsonRequestBehavior.AllowGet);
                    if (price.IsNullOrEmpty())
                        return Json(new { confirm = false, errorMessage = "Fiyat alanı boş bırakılmamalıdır." }, JsonRequestBehavior.AllowGet);

                    //product.name = !name.IsNullOrEmpty() ? name : product.name;
                    //product.categoryID = categoryID > 0 ? categoryID : product.categoryID;
                    //product.description = !description.IsNullOrEmpty() ? description : product.description;
                    //product.price = !price.IsNullOrEmpty() ? Convert.ToDecimal(price.Replace('.', ',')) : product.price;
                    //product.status = status;

                    product.categoryID = categoryID;
                    product.name = name;
                    product.description = description;
                    product.price = Convert.ToDecimal(price.Replace('.', ','));
                    product.status = status;

                    bool isSuccess = bllProduct.Update(product);

                    return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { confirm = false, errorMessage = "Ürün bulunamadı." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // add
                bool isSuccess = bllProduct.AddProduct(new mProduct()
                {
                    name = name,
                    categoryID = categoryID,
                    description = description,
                    price = Convert.ToDecimal(price.Replace('.', ',')),
                    status = status,
                });

                BllCategory bllCategory = new BllCategory();

                return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ChangeProductStatus(int ID = 0, bool status = false)
        {
            if (ID > 0)
            {
                BllProduct bllProduct = new BllProduct();
                product product = bllProduct.GetProduct(ID);
                if (product != null)
                {
                    product.status = status;
                    bool isSuccess = bllProduct.Update(product);
                    return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { confirm = false, errorMessage = "Bir şeyler yanlış." }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { confirm = false, errorMessage = "Ürün bulunamadı." }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region categories
        [HttpGet]
        public ActionResult Categories()
        {
            if (IsAdmin())
            {
                BllCategory bllCategory = new BllCategory();
                BllProduct bllProduct = new BllProduct();

                List<mMenu> mMenu = new List<mMenu>();

                List<mCategory> mCategories = bllCategory.GetAllmCategories();

                foreach (var item in mCategories)
                {
                    List<mProduct> mProducts = bllProduct.GetAllmProductsByCategory(item.ID);
                    mMenu.Add(new mMenu
                    {
                        category = item,
                        products = mProducts,
                    });
                }

                ViewBag.mMenu = mMenu;
                return View();
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public JsonResult AddUpdateCategory(int ID = 0, string name = "", bool status = true)
        {
            BllCategory bllCategory = new BllCategory();
            if (ID > 0)
            {
                // Update
                if (!name.IsNullOrEmpty())
                {
                    bool isSuccess = bllCategory.UpdateCategory(ID, name, status);
                    return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { confirm = false, errorMessage = "İsim boş bırakılamaz." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // Add
                bool isSuccess = bllCategory.AddCategory(name);
                return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ChangeCategoryStatus(int ID = 0, bool status = false)
        {
            if (ID > 0)
            {
                BllCategory bllCategory = new BllCategory();
                category category = bllCategory.GetCategory(ID);
                category.status = status;
                bool isSuccess = bllCategory.Update(category);
                return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { confirm = false, errorMessage = "Kategori bulunamadı." }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region tables
        [HttpGet]
        public ActionResult Tables()
        {
            if (IsAdmin())
            {
                BllTable bllTable = new BllTable();
                List<mTable> mTables = bllTable.ToModel(bllTable.GetAllTables());

                ViewBag.mTables = mTables;
                return View();
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public JsonResult AddUpdateTable(int ID = 0, int numberOfChairs = 0, string name = "", bool status = true)
        {
            BllTable bllTable = new BllTable();
            if (ID > 0)
            {
                // Update
                table table = bllTable.GetTableByID(ID);
                if (numberOfChairs == 0)
                    return Json(new { confirm = false, errorMessage = "Sandalye sayısı alanı boş bırakılmamalıdır." }, JsonRequestBehavior.AllowGet);
                if (name.IsNullOrEmpty())
                    return Json(new { confirm = false, errorMessage = "Masa adı alanı boş bırakılmamalıdır." }, JsonRequestBehavior.AllowGet);
                if (table.availability > 1)
                    return Json(new { confirm = false, errorMessage = "Lütfen değişiklik yapmadan önce masanın boşalmasını bekleyin." }, JsonRequestBehavior.AllowGet);
                else
                {
                    bool isSuccess = bllTable.UpdateTable(new mTable()
                    {
                        ID = ID,
                        availability = 1,
                        balance = 0,
                        numberOfChairs = numberOfChairs,
                        status = status,
                        tableName = name
                    }); if (isSuccess) bllTable.Save();
                    return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // Add
                bool isSuccess = bllTable.AddTable(name, numberOfChairs);
                return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ChangeTableStatus(int ID = 0, bool status = false)
        {
            BllTable bllTable = new BllTable();
            if (ID > 0)
            {
                // Update
                table table = bllTable.GetTableByID(ID);
                if (table != null)
                {
                    if (table.availability > 1)
                        return Json(new { confirm = false, errorMessage = "Lütfen değişiklik yapmadan önce masanın boşalmasını bekleyin." }, JsonRequestBehavior.AllowGet);
                    else
                    {
                        bool isSuccess = bllTable.UpdateTable(new mTable()
                        {
                            ID = ID,
                            availability = table.availability,
                            balance = table.balance,
                            numberOfChairs = table.numberOfChairs,
                            tableName = table.tableName,
                            status = status,
                        });
                        if (isSuccess) bllTable.Save();
                        return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { confirm = false, errorMessage = "Masa bulunamadı." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { confirm = false, errorMessage = "Masa bulunamadı." }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region users

        [HttpGet]
        public ActionResult Users()
        {
            if (IsAdmin())
            {
                BllUser bllUser = new BllUser();
                BllRole bllRole = new BllRole();
                List<mUser> mUsers = bllUser.GetmUsers();
                List<mRole> mRoles = bllRole.ToModel(bllRole.GetAllRoles());
                ViewBag.mUsers = mUsers;
                ViewBag.mRoles = mRoles;
                return View();
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public JsonResult AddUpdateUser(mUser mUser)
        {
            BllUser bllUser = new BllUser();

            if (mUser != null && mUser.ID > 0)
            {
                // Update
                user user = bllUser.GetUserByID(mUser.ID);
                user.name = !mUser.name.IsNullOrEmpty() ? mUser.name : user.name;
                user.surname = !mUser.surname.IsNullOrEmpty() ? mUser.surname : user.surname;
                user.eMail = !mUser.eMail.IsNullOrEmpty() ? mUser.eMail : user.eMail;
                user.phoneNumber = !mUser.phoneNumber.IsNullOrEmpty() ? mUser.phoneNumber : user.phoneNumber;
                user.password = !mUser.password.IsNullOrEmpty() ? mUser.password : user.password;
                user.status = mUser.status;
                user.roleID = mUser.roleID != 0 ? mUser.roleID : user.roleID;

                bool isSuccess = bllUser.Update(user);
                return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Add

                bool isSuccess = bllUser.AddUser(mUser);
                return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ChangeUserStatus(int ID = 0, bool status = false)
        {
            if (ID > 0)
            {
                BllUser bllUser = new BllUser();
                user user = bllUser.GetUserByID(ID);
                if (user != null)
                {
                    // kullanıcı var
                    user.status = status;
                    bool isSuccess = bllUser.Update(user);
                    return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // kullanıcı bulunamadı
                    return Json(new { confirm = false, errorMessage = "Kullanıcı bulunamadı." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // id bilgisi hatalı
                return Json(new { confirm = false, errorMessage = "Kullanıcı bulunamadı." }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region cashRegister

        [HttpGet]
        public ActionResult CashRegister()
        {
            if (IsAdmin())
            {
                BllPayment bllPayment = new BllPayment();
                List<payment> payments = bllPayment.GetAll();

                List<IGrouping<DateTime, payment>> list = payments.OrderByDescending(x => x.dateTime.Value.Date).GroupBy(x => x.dateTime.Value.Date).ToList();
                ViewBag.payments = list;

                return View();
            }
            else
                return RedirectToAction("Index", "Login");
        }

        #endregion

        #region orders

        [HttpGet]
        public ActionResult Orders()
        {
            if (IsAdmin())
            {
                BllOrder bllOrder = new BllOrder();
                List<order> orders = bllOrder.GetAll();

                ViewBag.orders = orders;
                return View();
            }
            else
                return RedirectToAction("Index", "Login");
        }

        #endregion

        #region reservations

        [HttpGet]
        public ActionResult Reservations()
        {
            if (IsAdmin())
            {
                BllReservation bllReservation = new BllReservation();
                List<reservation> reservations = bllReservation.GetAll();

                ViewBag.reservations = reservations;
                return View();
            }
            else
                return RedirectToAction("Index", "Login");
        }

        #endregion

        #region admin
        public JsonResult GetUserName()
        {
            int ID = BllOrtak.Sessions.ID;

            user user = new BllUser().GetUserByID(ID);
            string profilePic = user.profilePicture;
            string name = user.name + " " + user.surname;

            if (!name.IsNullOrEmpty())
                return Json(new { confirm = true, name, profilePic }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { confirm = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}