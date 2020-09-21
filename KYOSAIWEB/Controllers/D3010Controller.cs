using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KYOSAI_WEB.Models;
using Oracle.ManagedDataAccess.Client;

namespace KYOSAI_WEB.Controllers
{
    public class D3010Controller : Controller
    {
        private Model1 db = new Model1();

        /// <summary>
        /// default mathod 
        /// initially all data load
        /// </summary>
        // GET: D3010
        public ActionResult Index()
        {
            if (Session["LoginUserid"] == null)
            {
                ViewData["ID"] = "Login";
                return RedirectToAction("Index", "Login");
            }
            ViewData["LoginUsernm"] = Session["LoginUsernm"];

            var s0010 = db.S0010.SingleOrDefault();
            ViewBag.S0030List = null;

            if (s0010 != null)
            {
                var s0030 = db.S0030.Where(m => m.APPID == s0010.APPID && m.CALTYP == s0010.CALTYP && m.WKKBN == "2").Select(m => m.YMD).ToArray();
                if (s0030 != null)
                {
                    String[] S0030HolidayArr = new String[s0030.Length];
                    for (int i = 0; i < s0030.Length; i++)
                    {
                        S0030HolidayArr[i] = String.Format("{0:yyyy-MM-dd}", s0030[i]);
                    }
                    ViewBag.S0030List = S0030HolidayArr;
                }
            }

            ViewData["ID"] = "納期回答画面";
            ViewData["SUPPCD"] = Session["UserSuppcd"];
            Session["SUPPCD"] = Session["UserSuppcd"];
            ViewData["STATE1"] = "checked";
            Session["STATE1"] = "1";
            ViewData["REPLYKBN1"] = "checked";
            Session["REPLYKBN1"] = "1"; 
            String SUPPCD = "";
            List<D3010> listd3010 = new List<D3010>();
            if (Session["UserSuppcd"] != null)
            {
                SUPPCD = Session["UserSuppcd"].ToString();

                var d3010 = db.D3010.Include(d => d.M0010).Include(d => d.M1050);
                d3010 = d3010.Where(m => m.SUPPCD == SUPPCD);
                d3010 = d3010.Where(m => m.STATE == "1");
                //Comment this two line for testing purpose
                //d3010 = d3010.Where(m => m.PRTKBN1 == "2");
                //d3010 = d3010.Where(m => m.DECIKBN == "2");

                d3010 = d3010.Where(m => !(from dc in db.DC0060 select dc.ORDERNO).Contains(m.ORDERNO));
                listd3010 = SetOrderData(d3010.ToList());
            }

            ViewBag.M1010List = db.M1010.ToList().OrderBy(x => x.BCCD);
            ViewBag.M0010List = db.M0010.ToList().OrderBy(x => x.HMNO);

            if (Session["scrollTop"] != null)
            {
                ViewData["scrollTop"] = Session["scrollTop"];
            }
            else
            {
                ViewData["scrollTop"] = 0;
            }

            if (Session["scrollGridVer"] != null)
            {
                ViewData["scrollGridVer"] = Session["scrollGridVer"];
            }
            else
            {
                ViewData["scrollGridVer"] = 0;
            }

            if (Session["scrollGridHor"] != null)
            {
                ViewData["scrollGridHor"] = Session["scrollGridHor"];
            }
            else
            {
                ViewData["scrollGridHor"] = 0;
            }

            Session["scrollGridVer"] = 0;
            Session["scrollGridHor"] = 0;
            ViewData["pageMethod"] = "index";
            return View(listd3010);
        }

        /// <summary>
        ///  Search data based on search criteria
        /// </summary>
        [ValidateInput(false)]
        public ActionResult getData(String SUPPCD, String ORDERNO, String ORDERNOTO, String SEHMNO, String SEHMNOTO, String HMNM, String ORDERDTFR, String ORDERDTTO, String MAKERIPNM, String AGREEDFR, String AGREEDTO, String DELINM, String REQDELIDTFR, String REQDELIDTTO, String CHARGENM,String STATE1,String STATE2, String REPLYKBN1, String REPLYKBN2,String REPLYKBN3,String OrderSeachFlg, String ShmnoSeachFlg)
        {
            if (Session["LoginUserid"] == null)
            {
                ViewData["ID"] = "Login";
                return RedirectToAction("Index", "Login");
            }
            ViewData["LoginUsernm"] = Session["LoginUsernm"];

            ViewData["ID"] = "納期回答画面";
            ViewData["pageMethod"] = "getData";

            var s0010 = db.S0010.SingleOrDefault();
            ViewBag.S0030List = null;

            if (s0010 != null)
            {
                var s0030 = db.S0030.Where(m => m.APPID == s0010.APPID && m.CALTYP == s0010.CALTYP && m.WKKBN == "2").Select(m => m.YMD).ToArray();
                if (s0030 != null)
                {
                    String[] S0030HolidayArr = new String[s0030.Length];
                    for (int i = 0; i < s0030.Length; i++)
                    {
                        S0030HolidayArr[i] = String.Format("{0:yyyy-MM-dd}", s0030[i]);
                    }
                    ViewBag.S0030List = S0030HolidayArr;
                }
            }

            var d3010 = db.D3010.Include(d => d.M0010).Include(d => d.M1050);

            //Comment this two line for testing purpose
            //d3010 = d3010.Where(m => m.PRTKBN1 == "2");
            //d3010 = d3010.Where(m => m.DECIKBN == "2");           

            if (SUPPCD != null && SUPPCD != "")
            {
                d3010 = d3010.Where(m => m.SUPPCD == SUPPCD);
            }

            if (OrderSeachFlg == "1")
            {
                if (ORDERNO != null && ORDERNO != "")
                {
                    d3010 = d3010.Where(m => m.ORDERNO.Contains(ORDERNO));
                }
            }
            else
            {
                if (ORDERNO != null && ORDERNO != "")
                {
                    d3010 = d3010.Where(m => String.Compare(m.ORDERNO, ORDERNO) >= 0);

                }

                if (ORDERNOTO != null && ORDERNOTO != "")
                {
                    d3010 = d3010.Where(m => String.Compare(m.ORDERNO, ORDERNOTO) <= 0);

                }
            }

            if (ShmnoSeachFlg == "1")
            {
                if (SEHMNO != null && SEHMNO != "")
                {
                    d3010 = d3010.Where(m => m.M0010.SEHMNO.StartsWith(SEHMNO));
                }
            }
            else
            {
                if (SEHMNO != null && SEHMNO != "")
                {
                    d3010 = d3010.Where(m => String.Compare(m.M0010.SEHMNO, SEHMNO) >= 0);

                }

                if (SEHMNOTO != null && SEHMNOTO != "")
                {
                    d3010 = d3010.Where(m => String.Compare(m.M0010.SEHMNO, SEHMNOTO) <= 0);

                }
            }

           
            if (HMNM != null && HMNM != "")
            {
                d3010 = d3010.Where(m => m.M0010.HMNM.Contains(HMNM));
            }
            if (MAKERIPNM != null && MAKERIPNM != "")
            {
                d3010 = d3010.Where(m => m.M0010.MC0030.MAKERIPNM.Contains(MAKERIPNM));
            }
            if (DELINM != null && DELINM != "")
            {
                d3010 = d3010.Where(m => m.M1050.DELINM.Contains(DELINM));
            }
            if (CHARGENM != null && CHARGENM != "")
            {
                d3010 = d3010.Where(m => db.MC0060.Where(d => db.M5010.Where(t => t.CHARGENM.Contains(CHARGENM)).Select(f=>f.CHARGECD).Contains(d.CHARGECD)).Select(f => f.JITANCD).Contains(m.JITANCD));
            }

            if (ORDERDTFR != null && ORDERDTFR != "")
            {
                var dt1 = DateTime.Parse(ORDERDTFR);
                d3010 = d3010.Where(m => dt1 <= m.ORDERDT);
            }
            if (ORDERDTTO != null && ORDERDTTO != "")
            {
                var dt2 = DateTime.Parse(ORDERDTTO);
                d3010 = d3010.Where(m => dt2 >= m.ORDERDT);
            }

            if (AGREEDFR != null && AGREEDFR != "")
            {
                var dt1 = DateTime.Parse(AGREEDFR);
                d3010 = d3010.Where(m => dt1 <= m.AGREED);
            }
            if (AGREEDTO != null && AGREEDTO != "")
            {
                var dt2 = DateTime.Parse(AGREEDTO);
                d3010 = d3010.Where(m => dt2 >= m.AGREED);
            }

            if (REQDELIDTFR != null && REQDELIDTFR != "")
            {
                var dt1 = DateTime.Parse(REQDELIDTFR);
                d3010 = d3010.Where(m => dt1 <= m.REQDELIDT);
            }
            if (REQDELIDTTO != null && REQDELIDTTO != "")
            {
                var dt2 = DateTime.Parse(REQDELIDTTO);
                d3010 = d3010.Where(m => dt2 >= m.REQDELIDT);
            }
            
            String STATE = "";
            if (STATE1 == "")
            {
                STATE1 = null;
            }
            if (STATE2 == "")
            {
                STATE2 = null;
            }

            if (STATE1 != null)
            {
                ViewData["STATE1"] = "checked";
                STATE = STATE1;

            }
            else {
                ViewData["STATE1"] = "";
            }
            if (STATE2 != null)
            {
                ViewData["STATE2"] = "checked";
                if (STATE != "")
                {
                    STATE = STATE + "," + STATE2;
                }
                else {
                    STATE = STATE2;
                }
             }
            else {
                ViewData["STATE2"] = "";
            }
            if (STATE != "")
            {
                String[] StateList = STATE.Split(',');
                d3010 = d3010.Where(m => StateList.Contains(m.STATE));
            }

            if (REPLYKBN1 == "")
            {
                REPLYKBN1 = null;
            }
            if (REPLYKBN2 == "")
            {
                REPLYKBN2 = null;
            }
            if (REPLYKBN3 == "")
            {
                REPLYKBN3 = null;
            }

            if (REPLYKBN1 != null && REPLYKBN2 == null && REPLYKBN3 == null)
            {
                d3010 = d3010.Where(m => !db.DC0060.Select(f => f.ORDERNO).Contains(m.ORDERNO));

            }else if (REPLYKBN2 != null && REPLYKBN1 == null && REPLYKBN3 == null)
            {
                d3010 = d3010.Where(m => db.DC0060.Select(f => f.ORDERNO).Contains(m.ORDERNO) && m.ORDERQTY > db.DC0060.Where(f => f.ORDERNO == m.ORDERNO).Select(f => f.DELISCHEQTY).Sum());

            }else if (REPLYKBN3 != null && REPLYKBN2 == null && REPLYKBN1 == null)
            {
                d3010 = d3010.Where(m => db.DC0060.Select(f => f.ORDERNO).Contains(m.ORDERNO) && m.ORDERQTY <= db.DC0060.Where(f => f.ORDERNO == m.ORDERNO).Select(f => f.DELISCHEQTY).Sum());

            }else if (REPLYKBN1 != null && REPLYKBN2 != null && REPLYKBN3 == null)
            {
                d3010 = d3010.Where(m => (!db.DC0060.Select(f => f.ORDERNO).Contains(m.ORDERNO)) || (db.DC0060.Select(f => f.ORDERNO).Contains(m.ORDERNO) && m.ORDERQTY > db.DC0060.Where(f => f.ORDERNO == m.ORDERNO).Select(f => f.DELISCHEQTY).Sum()));
            }
            else if (REPLYKBN2 != null && REPLYKBN1 == null && REPLYKBN3 != null)
            {
                d3010 = d3010.Where(m => db.DC0060.Select(f => f.ORDERNO).Contains(m.ORDERNO));
            }
            else if (REPLYKBN3 != null && REPLYKBN2 == null && REPLYKBN1 != null)
            {
                d3010 = d3010.Where(m => (db.DC0060.Select(f => f.ORDERNO).Contains(m.ORDERNO) && m.ORDERQTY <= db.DC0060.Where(f => f.ORDERNO == m.ORDERNO).Select(f => f.DELISCHEQTY).Sum()) || (!db.DC0060.Select(f => f.ORDERNO).Contains(m.ORDERNO)));
            }            

            if (REPLYKBN1 != null)
            {
                ViewData["REPLYKBN1"] = "checked";
            }
            else
            {
                ViewData["REPLYKBN1"] = "";
            }
            if (REPLYKBN2 != null)
            {
                ViewData["REPLYKBN2"] = "checked";
            }
            else
            {
                ViewData["REPLYKBN2"] = "";
            }
            if (REPLYKBN3 != null)
            {
                ViewData["REPLYKBN3"] = "checked";
            }
            else
            {
                ViewData["REPLYKBN3"] = "";
            }

            ViewBag.M1010List = db.M1010.ToList();
            ViewBag.M0010List = db.M0010.ToList();
            ViewData["SUPPCD"] = SUPPCD;
            ViewData["ORDERNO"] = ORDERNO;
            ViewData["ORDERNOTO"] = ORDERNOTO;
            ViewData["SEHMNO"] = SEHMNO;
            ViewData["SEHMNOTO"] = SEHMNOTO;
            ViewData["HMNM"] = HMNM;
            ViewData["ORDERDTFR"] = ORDERDTFR;
            ViewData["ORDERDTTO"] = ORDERDTTO;
            ViewData["MAKERIPNM"] = MAKERIPNM;
            ViewData["AGREEDFR"] = AGREEDFR;
            ViewData["AGREEDTO"] = AGREEDTO;
            ViewData["DELINM"] = DELINM;
            ViewData["REQDELIDTFR"] = REQDELIDTFR;
            ViewData["REQDELIDTTO"] = REQDELIDTTO;
            ViewData["CHARGENM"] = CHARGENM;
            ViewData["OrderSeachFlg"] = OrderSeachFlg;
            ViewData["ShmnoSeachFlg"] = ShmnoSeachFlg;
            
            
            ViewData["scrollTop"] = Session["scrollTop"];
            ViewData["scrollGridVer"] = Session["scrollGridVer"];
            ViewData["scrollGridHor"] = Session["scrollGridHor"];

            Session["SUPPCD"] = SUPPCD;
            Session["ORDERNO"] = ORDERNO;
            Session["ORDERNOTO"] = ORDERNOTO;
            Session["SEHMNO"] = SEHMNO;
            Session["SEHMNOTO"] = SEHMNOTO;
            Session["HMNM"] = HMNM;
            Session["ORDERDTFR"] = ORDERDTFR;
            Session["ORDERDTTO"] = ORDERDTTO;
            Session["MAKERIPNM"] = MAKERIPNM;
            Session["AGREEDFR"] = AGREEDFR;
            Session["AGREEDTO"] = AGREEDTO;
            Session["DELINM"] = DELINM;
            Session["REQDELIDTFR"] = REQDELIDTFR;
            Session["REQDELIDTTO"] = REQDELIDTTO;
            Session["CHARGENM"] = CHARGENM;
            Session["STATE1"] = STATE1;
            Session["STATE2"] = STATE2;
            Session["REPLYKBN1"] = REPLYKBN1;
            Session["REPLYKBN2"] = REPLYKBN2;
            Session["REPLYKBN3"] = REPLYKBN3;
            Session["OrderSeachFlg"] = OrderSeachFlg;
            Session["ShmnoSeachFlg"] = ShmnoSeachFlg;

            var listd3010 = SetOrderData(d3010.ToList());
            return View("Index", listd3010.OrderBy(m => m.ORDERNO));
        }

        /// <summary>
        /// Update grid data
        /// </summary>
        /// <param name="d3010">画面：明細部のリスト</param>
        /// <param name="scrollTop">画面の垂直スクロール位置</param>
        /// <param name="scrollGridVer">明細部の垂直スクロール位置</param>
        /// <param name="scrollGridHor">明細部の横スクロール位置</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateData([Bind(Include = "ORDERNO,ORDERKBN,ORDERDT,SUPPCD,DELICD,HMNO,ORDERQTY,ORDERUNIT,PRICEKBN,PRICE,PRICEUNIT,AMOUNT,TAXKBN,AGREED,REPLY,STATE,REMARK,PRTKBN1,PRTKBN2,DECIKBN,REPLANKBN,SORDERNO,EXCEKBN,RATE,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,KYOTENCD,JITANCD,JORDERNO,SZCMT,BCCMT,SHEETNO,ROWNO,PRTDT1,REPLYDLDT,REQDELIDT,ADJREQKBN,MORDERNO,GAITNKBN")] List<D3010> d3010, int scrollTop, int scrollGridVer, int scrollGridHor)
        {
            if (Session["LoginUserid"] == null)
            {
                ViewData["ID"] = "Login";
                return RedirectToAction("Index", "Login");
            }

            ViewData["ID"] = "納期回答画面";
            DbContextTransaction dbTran = db.Database.BeginTransaction();
            var av_userid = new OracleParameter("av_userid", OracleDbType.Varchar2, Session["LoginUserid"], ParameterDirection.Input);
            var spSql = "BEGIN DBMS_APPLICATION_INFO.SET_CLIENT_INFO(:av_userid); END; ";
            var spResult = db.Database.ExecuteSqlCommand(spSql, av_userid);
            try
            {
                List<D3010> d3010List = d3010;
                if (d3010List.Count > 0)
                {
                    Boolean isUpdate = false;
                    Boolean IsConCurErr = false;
                    for (int i = 0; i < d3010List.Count; i++)
                    {
                        D3010 d3010Db = db.D3010.Find(d3010List[i].ORDERNO);
                        if ((d3010Db.REPLY != d3010List[i].REPLY) || (d3010List[i].BCCMT != d3010Db.BCCMT))
                        {
                            if (CheckConcurrency(d3010List[i]))
                            {
                                d3010Db.REPLY = d3010List[i].REPLY;
                                d3010Db.BCCMT = d3010List[i].BCCMT;
                                db.Entry(d3010Db).State = EntityState.Modified;

                                List<DC0060> dC0060Lst = db.DC0060.Where(m => m.ORDERNO == d3010Db.ORDERNO).ToList();

                                //発注分納情報 の レコード件数 ＜＝ １件であった場合は、発注情報、発注分納情報への更新処理を実行する。
                                if (dC0060Lst != null && dC0060Lst.Count == 1)
                                {
                                    DC0060 dC0060 = dC0060Lst[0];
                                    dC0060.DELISCHEDT = d3010Db.REPLY;
                                    db.Entry(dC0060).State = EntityState.Modified;
                                }

                                db.Configuration.ValidateOnSaveEnabled = false;
                                db.SaveChanges();
                                db.Configuration.ValidateOnSaveEnabled = true;

                                isUpdate = true;
                            }
                            else
                            {
                                IsConCurErr = true;
                                break;
                            }
                        }   
                    }
                    if (isUpdate && !IsConCurErr)
                    {
                        dbTran.Commit();
                    }
                    if (IsConCurErr)
                    {
                        if(isUpdate)
                            dbTran.Rollback();
                        TempData["ErrMsg"] = "他の端末にて更新中です。しばらく経ってから再度お試しください。";
                        TempData.Keep("ErrMsg");
                    }
                }
            }
            catch (Exception e)
            {
                dbTran.Rollback();
                ViewData["errMsgForErrorPage"] = e;
                return View("~/Views/Shared/Error.cshtml");
            }
            Session["scrollTop"] = scrollTop;
            Session["scrollGridVer"] = scrollGridVer;
            Session["scrollGridHor"] = scrollGridHor;
            //ViewBag.HMNO = new SelectList(db.M0010, "HMNO", "HMNM", d3010.HMNO);
                       
            return RedirectToAction("getData", "D3010", new
            {
                @SUPPCD = (Session["SUPPCD"] == null ? "" : Session["SUPPCD"].ToString()),
                @ORDERNO = (Session["ORDERNO"] == null ? "" : Session["ORDERNO"].ToString()),
                @ORDERNOTO = (Session["ORDERNOTO"] == null ? "" : Session["ORDERNOTO"].ToString()),
                @SEHMNO = (Session["SEHMNO"] == null ? "" : Session["SEHMNO"].ToString()),
                @SEHMNOTO = (Session["SEHMNOTO"] == null ? "" : Session["SEHMNOTO"].ToString()),
                @HMNM = (Session["HMNM"] == null ? "" : Session["HMNM"].ToString()),
                @ORDERDTFR = (Session["ORDERDTFR"] == null ? "" : Session["ORDERDTFR"].ToString()),
                @ORDERDTTO = (Session["ORDERDTTO"] == null ? "" : Session["ORDERDTTO"].ToString()),
                @MAKERIPNM = (Session["MAKERIPNM"] == null ? "" : Session["MAKERIPNM"].ToString()),
                @AGREEDFR = (Session["AGREEDFR"] == null ? "" : Session["AGREEDFR"].ToString()),
                @AGREEDTO = (Session["AGREEDTO"] == null ? "" : Session["AGREEDTO"].ToString()),
                @DELINM = (Session["DELINM"] == null ? "" : Session["DELINM"].ToString()),
                @REQDELIDTFR = (Session["REQDELIDTFR"] == null ? "" : Session["REQDELIDTFR"].ToString()),
                @REQDELIDTTO = (Session["REQDELIDTTO"] == null ? "" : Session["REQDELIDTTO"].ToString()),
                @CHARGENM = (Session["CHARGENM"] == null ? "" : Session["CHARGENM"].ToString()),
                @STATE1 = (Session["STATE1"] == null ? "" : Session["STATE1"].ToString()),
                @STATE2 = (Session["STATE2"] == null ? "" : Session["STATE2"].ToString()),
                @REPLYKBN1 = (Session["REPLYKBN1"] == null ? "" : Session["REPLYKBN1"].ToString()),
                @REPLYKBN2 = (Session["REPLYKBN2"] == null ? "" : Session["REPLYKBN2"].ToString()),
                @REPLYKBN3 = (Session["REPLYKBN3"] == null ? "" : Session["REPLYKBN3"].ToString()),
                @OrderSeachFlg = (Session["OrderSeachFlg"] == null ? "0" : Session["OrderSeachFlg"].ToString()),
                @ShmnoSeachFlg = (Session["ShmnoSeachFlg"] == null ? "" : Session["ShmnoSeachFlg"].ToString())
            });

        }
        
        /// <summary>
        /// get Unmapped col for search data
        /// </summary>
        /// <param name="d3010">list of data</param>
        public List<D3010> SetOrderData(List<D3010> d3010) {
            foreach (var item in d3010) {
                //SUPPNM
                var suppName = "";
                var m1010 =  db.M1010.Find(item.SUPPCD);
                if (m1010 != null)
                {
                    suppName = m1010.BCNM;
                }
                item.SUPPNM = suppName;

                // 納入数
                var DELIQTY = (from d30 in db.D3010
                               join d40 in db.D4020 on d30.ORDERNO equals d40.ORDERNO
                               where d40.DATAKBN == "2"
                               group d40 by d40.ORDERNO into t
                               select t.Sum(a => a.ACTUALQTY)).FirstOrDefault();
                item.DELIQTY = DELIQTY;

                // 注残数
                item.BKLOGQTY = item.ORDERQTY - DELIQTY;

                // 回答納期
                var dc0060 = db.DC0060.Where(m => m.ORDERNO == item.ORDERNO).ToList();
                if (dc0060.Where(m => m.RECEIVEKBN == "1").Count() > 0) {
                    item.REPLY = dc0060.Where(m => m.RECEIVEKBN == "1").Min(m => m.DELISCHEDT);
                } else if (dc0060.Where(m => new String [] {"2","3" }.Contains(m.RECEIVEKBN)).Count() > 0) {
                    item.REPLY = dc0060.Where(m => new String[] { "2", "3" }.Contains(m.RECEIVEKBN)).Max(m => m.DELISCHEDT);
                }

                // 分納
                if (dc0060.Count() >= 2)
                {
                    //checkbox checked true and disapble
                    item.PAYMENTKBN = "1";
                }
                else
                {
                    item.PAYMENTKBN = "0";
                }

                // 取引先コメント	
                // if checkbox check true then this field is disable

                //回答日
                if (dc0060.Count()>0)
                    item.ANSDT = dc0060.Max(m=>m.REPLYDT);

                //回答回数
                if (dc0060.Count() > 0)
                    item.REPLYCNT = dc0060.Count();

                var unitnm = "";
                var chargenm = "";

                //単位
                var ms080 = db.MS080.Find(item.ORDERUNIT);
                if (ms080 != null)
                {
                    unitnm = ms080.UNITNM;
                }
                item.UNITNM = unitnm;

                //発注担当者
                var m5010tbl = (from m5010 in db.M5010
                                join mc60 in db.MC0060 on m5010.CHARGECD equals mc60.CHARGECD
                                where mc60.JITANCD == item.JITANCD
                                select m5010).FirstOrDefault();                

                if (m5010tbl != null)
                {
                    chargenm = m5010tbl.CHARGENM;
                }                
                item.CHARGENM = chargenm;

            }
            return d3010;
        }

        /// <summary>
        /// To get Supplier name
        /// </summary>
        /// <param name="suppcd">Suppliier name</param>
        /// <returns>Supplier name</returns>
        public String getSuppcdNm(String suppcd)
        {
            if (suppcd == "")
            {
                return "Invalid";
            }
            M1010 m1010 =  db.M1010.Find(suppcd);

            if (m1010 == null)
            {
                return "not exist";
            }
            else
            {
                return m1010.BCNM;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Update schedule register pop up data
        /// </summary>
        /// <param name="dc0060">DC0060</param>
        /// <param name="orderNo">from screen</param>
        /// <param name="remark">from screen</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult UpdateScheduleRegData([Bind(Include = "ORDERNO,DELISCHECNT,DELISCHEDT,DELISCHEQTY,REPLYDT,RECEIVEDT,RECEIVEKBN,UKECHARGECD,UPDTLNG,UPDTDT")] List<DC0060> dc0060, String orderNo,String remark, int scrollTop, int scrollGridVer, int scrollGridHor, DateTime D3010Updtdt)
        {
            if (Session["LoginUserid"] == null)
            {
                ViewData["ID"] = "Login";
                return RedirectToAction("Index", "Login");
            }
            ViewData["ID"] = "納期回答画面";
            DbContextTransaction dbTran = db.Database.BeginTransaction();
            var av_userid = new OracleParameter("av_userid", OracleDbType.Varchar2, Session["LoginUserid"], ParameterDirection.Input);
            var spSql = "BEGIN DBMS_APPLICATION_INFO.SET_CLIENT_INFO(:av_userid); END; ";
            var spResult = db.Database.ExecuteSqlCommand(spSql, av_userid);
            try
            {
                Boolean d310ConCurFlg = false;
                Boolean dc60ConFlg = false;
                Boolean isDataUpd = false;

                if(dc0060.Count() > 0)
                {
                    for (int i = 0; i < dc0060.Count; i++)
                    {
                        String orderno = dc0060[i].ORDERNO;
                        Byte delischecnt = dc0060[i].DELISCHECNT;

                        //Get DC0060 data from db
                        var dc0060db = ( from dc60 in db.DC0060
                                         where dc60.ORDERNO == orderno && dc60.DELISCHECNT == delischecnt
                                         select dc60).FirstOrDefault();

                        //Case of update
                        if(dc0060db != null)
                        {
                            if ((dc0060db.DELISCHEDT != dc0060[i].DELISCHEDT) || (dc0060db.DELISCHEQTY != dc0060[i].DELISCHEQTY))
                            {
                               
                                //DateTime updtLng = new DateTime (1970,1,1) + TimeSpan.FromMilliseconds(dc0060[i].UPDTLNG);
                                DateTime updtLng = new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(dc0060[i].UPDTLNG) + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                                var date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(dc0060[i].UPDTLNG.ToString()));
                                if (dc0060db.UPDTDT != updtLng)
                                {
                                    dc60ConFlg = true;
                                    break;
                                }
                                else
                                {
                                    dc0060db.DELISCHEDT = dc0060[i].DELISCHEDT;
                                    dc0060db.DELISCHEQTY = dc0060[i].DELISCHEQTY;
                                    db.Entry(dc0060db).State = EntityState.Modified;
                                    isDataUpd = true;
                                }                                
                            }
                        }
                        //Case of insert
                        else
                        {
                            db.DC0060.Add(dc0060[i]);
                            isDataUpd = true;
                        }
                    }

                    //Case of delete
                    String ordernum = dc0060[0].ORDERNO;
                    List<KYOSAI_WEB.Models.DC0060> dc60db = (from dc60 in db.DC0060
                                                             where dc60.ORDERNO == ordernum
                                                             select dc60).ToList();
                    if (dc60db.Count() > 0)
                    {
                        for (int c1 = 0; c1 < dc60db.Count; c1++)
                        {
                            bool datafound = false;
                            for (int c2 = 0; c2 < dc0060.Count; c2++)
                            {
                                if ((dc60db[c1].ORDERNO == dc0060[c2].ORDERNO) && (dc60db[c1].DELISCHECNT == dc0060[c2].DELISCHECNT))
                                {
                                    datafound = true;
                                }
                            }
                            if (!datafound)
                            {
                                db.Entry(dc60db[c1]).State = EntityState.Deleted;
                                isDataUpd = true;
                            }
                        }
                    }

                    //Get D3010 data for update
                    D3010 d3010Db = db.D3010.Find(dc0060[0].ORDERNO);

                    if (d3010Db != null && d3010Db.BCCMT != remark)
                    {
                        if (d3010Db.UPDTDT != D3010Updtdt)
                        {
                            d310ConCurFlg = true;
                        }
                        else
                        {
                            d3010Db.BCCMT = remark;
                            db.Entry(d3010Db).State = EntityState.Modified;
                            isDataUpd = true;
                        }
                    }
                }

                var bolDataCommit = false;
                if (isDataUpd && !d310ConCurFlg && !dc60ConFlg)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    db.Configuration.ValidateOnSaveEnabled = true;
                    dbTran.Commit();
                    bolDataCommit = true;
                }

                if ((d310ConCurFlg || dc60ConFlg))
                {
                    if (isDataUpd && !bolDataCommit)
                    {
                        dbTran.Rollback();                        
                    }
                    TempData["ErrMsg"] = "他の端末にて更新中です。しばらく経ってから再度お試しください。";
                    TempData.Keep("ErrMsg");
                }
            }
            catch (Exception e)
            {
                dbTran.Rollback();
                ViewData["errMsgForErrorPage"] = e;
                return View("~/Views/Shared/Error.cshtml");
            }

            Session["scrollTop"] = scrollTop;
            Session["scrollGridVer"] = scrollGridVer;
            Session["scrollGridHor"] = scrollGridHor;

            return RedirectToAction("getData", "D3010", new
            {
                @SUPPCD = (Session["SUPPCD"] == null ? "" : Session["SUPPCD"].ToString()),
                @ORDERNO = (Session["ORDERNO"] == null ? "" : Session["ORDERNO"].ToString()),
                @ORDERNOTO = (Session["ORDERNOTO"] == null ? "" : Session["ORDERNOTO"].ToString()),
                @SEHMNO = (Session["SEHMNO"] == null ? "" : Session["SEHMNO"].ToString()),
                @SEHMNOTO = (Session["SEHMNOTO"] == null ? "" : Session["SEHMNOTO"].ToString()),
                @HMNM = (Session["HMNM"] == null ? "" : Session["HMNM"].ToString()),
                @ORDERDTFR = (Session["ORDERDTFR"] == null ? "" : Session["ORDERDTFR"].ToString()),
                @ORDERDTTO = (Session["ORDERDTTO"] == null ? "" : Session["ORDERDTTO"].ToString()),
                @MAKERIPNM = (Session["MAKERIPNM"] == null ? "" : Session["MAKERIPNM"].ToString()),
                @AGREEDFR = (Session["AGREEDFR"] == null ? "" : Session["AGREEDFR"].ToString()),
                @AGREEDTO = (Session["AGREEDTO"] == null ? "" : Session["AGREEDTO"].ToString()),
                @DELINM = (Session["DELINM"] == null ? "" : Session["DELINM"].ToString()),
                @REQDELIDTFR = (Session["REQDELIDTFR"] == null ? "" : Session["REQDELIDTFR"].ToString()),
                @REQDELIDTTO = (Session["REQDELIDTTO"] == null ? "" : Session["REQDELIDTTO"].ToString()),
                @CHARGENM = (Session["CHARGENM"] == null ? "" : Session["CHARGENM"].ToString()),
                @STATE1 = (Session["STATE1"] == null ? "" : Session["STATE1"].ToString()),
                @STATE2 = (Session["STATE2"] == null ? "" : Session["STATE2"].ToString()),
                @REPLYKBN1 = (Session["REPLYKBN1"] == null ? "" : Session["REPLYKBN1"].ToString()),
                @REPLYKBN2 = (Session["REPLYKBN2"] == null ? "" : Session["REPLYKBN2"].ToString()),
                @REPLYKBN3 = (Session["REPLYKBN3"] == null ? "" : Session["REPLYKBN3"].ToString()),
                @OrderSeachFlg = (Session["OrderSeachFlg"] == null ? "0" : Session["OrderSeachFlg"].ToString()),
                @ShmnoSeachFlg = (Session["ShmnoSeachFlg"] == null ? "" : Session["ShmnoSeachFlg"].ToString())
            });
        }

        public ActionResult getScheduleRegData(String orderno)
        {
            List<DC0060> dc60List = db.DC0060.Where(m => m.ORDERNO == orderno).ToList();

            return Json(dc60List, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Check concurrecny in data
        /// </summary>
        /// <param name="d3010">Model object with data</param>
        /// <returns>
        /// if true then no concurrency
        /// if false then concurrency
        /// </returns>
        public Boolean CheckConcurrency(D3010 d3010)
        {           
            Boolean rtnFlag = true;            
            D3010 d3010db = db.D3010.Find(d3010.ORDERNO);
            if (d3010db != null)
            {
                if (d3010.UPDTDT != d3010db.UPDTDT)
                {
                    rtnFlag = false;
                }
            }
            return rtnFlag;
        }
    }
}
