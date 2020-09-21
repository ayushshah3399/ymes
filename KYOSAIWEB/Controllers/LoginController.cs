using KYOSAI_WEB.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYOSAI_WEB.Controllers
{
    public class LoginController : Controller
    {
        Model1 db = new Model1();
        // GET: Login
        public ActionResult Index()
        {
            ViewData["ID"] = "Login";

            var dbNameSql = "SELECT GLOBAL_NAME FROM SYS.GLOBAL_NAME ";
            var dbName = db.Database.SqlQuery<String>(dbNameSql).FirstOrDefault();
            Session["DBName"] = dbName;
            return View();
        }

        [HttpPost]
        public ActionResult Index(S0050 s0050)
        {
            ViewData["ID"] = "Login";

            if (!ModelState.IsValid)
            {
                return View();
            }
            DbContextTransaction dbTran = db.Database.BeginTransaction();
            try
            {
                var av_appid = new OracleParameter("av_appid", OracleDbType.Varchar2, "TeLAS", ParameterDirection.Input);
                var av_userid = new OracleParameter("av_userid", OracleDbType.Varchar2, s0050.USERID, ParameterDirection.Input);
                var av_machine = new OracleParameter("av_machine", OracleDbType.Varchar2, Environment.MachineName, ParameterDirection.Input);
                var av_passkey = new OracleParameter("av_passkey", OracleDbType.Varchar2, "ACTY1234", ParameterDirection.Input);
                var av_pass = new OracleParameter("av_pass", OracleDbType.Varchar2, s0050.PASS, ParameterDirection.Input);
                var av_servicename = new OracleParameter("av_servicename", OracleDbType.Varchar2, "", ParameterDirection.Input);
                var av_errmsg = new OracleParameter("av_errmsg", OracleDbType.Varchar2, 32767);
                av_errmsg.Direction = ParameterDirection.InputOutput;
                var an_sessionid = new OracleParameter("an_sessionid", OracleDbType.Int32);
                an_sessionid.Direction = ParameterDirection.Output;
                var av_usernm = new OracleParameter("av_usernm", OracleDbType.Varchar2,32767);
                av_usernm.Direction = ParameterDirection.Output;
                var ac_loginhist = new OracleParameter("ac_loginhist", OracleDbType.Char,4);
                ac_loginhist.Direction = ParameterDirection.Output;

                var spSql = "begin TELAS.PC_SYS_LOG.PR_SYS_LOGIN(:av_appid,:av_userid,:av_machine,:av_passkey,:av_pass,:av_servicename,:av_errmsg,:an_sessionid,:av_usernm,:ac_loginhist); end;";
                var spResult = db.Database.ExecuteSqlCommand(spSql, av_appid, av_userid, av_machine, av_passkey, av_pass, av_servicename, av_errmsg, an_sessionid, av_usernm, ac_loginhist);
                String loginUserName = av_usernm.Value.ToString();
                String errorMsg = av_errmsg.Value.ToString();
                String loginhist = ac_loginhist.Value.ToString();
                String strErrorMessage = "";

                if (errorMsg != "null")
                {
                    switch (errorMsg)
                    {
                        case "S01":
                            strErrorMessage = "システム情報が未設定";
                            break;
                        case "S02":
                            strErrorMessage = "パスワード有効期間/警告期間が未設定";
                            break;
                        case "001":
                            strErrorMessage = "ユーザーIDかパスワードが正しくない";
                            break;
                        case "U01":
                            strErrorMessage = "このユーザーIDではアプリケーションを起動できません。";
                            break;
                        case "P01":
                            strErrorMessage = "パスワード有効期限切れ";
                            break;
                        case "P02":
                            strErrorMessage = "パスワード有効期限が近づいている";
                            break;
                        default:
                            break;
                    }
                    TempData["LoginErrMsg"] = strErrorMessage;
                }
                else
                {
                    //Redirect to next page
                    Session["LoginUserid"] = s0050.USERID;
                    Session["LoginUsernm"] = loginUserName;

                    S0050 s0050Db = db.S0050.Find(s0050.USERID);
                    String suppcd = "";
                    if (s0050Db == null)
                    {
                        suppcd = "";
                    }
                    else
                    {
                        suppcd = s0050Db.SUPPCD;
                    }
                    Session["UserSuppcd"] = suppcd;
                    TempData["LoginErrMsg"] = "";
                    return RedirectToAction("Index", "D3010");
                }

                dbTran.Commit();
            }
            catch (Exception e)
            {
                dbTran.Rollback();
                ViewData["errMsgForErrorPage"] = e;
                return View("~/Views/Shared/Error.cshtml");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}