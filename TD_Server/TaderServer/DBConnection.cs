using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TaderServer
{
    public class DBConnection
    {
        private OracleConnection conn;
        private OracleCommand cmd;
        private OracleDataAdapter oda;
        private string strConn = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl))); User Id=ghc;Password=1234;";
        private string strSample = "";

        private DataSet ds;
        //DB연결
        private void ConnectDB()
        {
            try
            {
                conn = new OracleConnection(strConn);
                cmd = new OracleCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                ds = new DataSet();
                oda = new OracleDataAdapter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("연결 오류" + ex.Message);
            }
        }
        public DataSet Select_temp(string smsg)
        {
            try
            {
                ConnectDB();
                ds.Clear();
                oda.SelectCommand = new OracleCommand(smsg, conn);
                oda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public void DeleteALL_temp(string str)
        {
            try
            {
                ConnectDB();
                cmd = new OracleCommand(str, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("삭제 오류" + ex.Message);
            }
        }
        public string Insert_temp(string str)
        {
            try
            {
                ConnectDB();
                cmd = new OracleCommand(str, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("삽입 오류" + ex.Message);
                return "f";
            }
        }
        #region 종류 DB
        public DataSet SelectKindAll() // 종류 전체 불러오기
        {
            try
            {
                ConnectDB();
                strSample = "select KindName from FoodKindTB";
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
                conn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public DataSet SelectKind(string kindmsg) // 종류 검색
        {
            try
            {
                ConnectDB();
                strSample = string.Format("select KindName from FoodKindTB Where Kindname like '%{0}%'",kindmsg);
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
                conn.Close();
                 foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Console.WriteLine(r["Kindname"].ToString());
                }
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public string InsertKind(string kindstr)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("Insert into FoodKindTB (KindID, KindName) Select kid_seq.nextval,'{0}' From DUAL A Where Not EXISTS(Select 0 From foodkindtb WHERE KindName = '{0}')", kindstr);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch(Exception ex)
            {
                Console.WriteLine("삽입 오류" + ex.Message);
                return "f";
            }
        }
        public string UpdateKind(string newkind, string oldkind)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("update FoodKindTB set kindname = '{0}' where kindname='{1}'",newkind,oldkind);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("수정 오류" + ex.Message);
                return "f";
            }
        }
        public string DeleteKind(string kindstr)
        {
            try
            {
                //메뉴
                DeleteALL_temp(string.Format("delete from FoodmenuTB where storeid In(select storeid from FoodstoreTB where kindid IN(select kindid from FoodkindTb where kindname = '{0}'))", kindstr));
                //옵션
                DeleteALL_temp(string.Format("delete from FoodOptionTb where storeId In(Select StoreId from foodstoretb where kindid In(select kindid from foodkindtb where kindname = '{0}'))", kindstr));
                //가게
                DeleteALL_temp(string.Format("delete from foodstoretb where kindid in(select kindid from foodkindtb where kindname= '{0}')", kindstr));
                //종류
                DeleteALL_temp(string.Format("delete from FoodKindTb where KindName = '{0}'", kindstr));
                Console.WriteLine("삭제 성공");
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("삭제 오류" + ex.Message);
                return "f";
            }
        }
        #endregion

        #region 가게명 DB
        public DataSet SelectStore()
        {
            try
            {
                ConnectDB();
                strSample = "select StoreName from FoodStoreTB";
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
               
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public DataSet Kind_SelectStore(string kmsg)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("select StoreName from FoodStoreTB where KindId IN(select KindID from FoodKindTB where KindName like '%{0}%')", kmsg);
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public string InsertStore(string storestr, string kindstr)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("Insert into FoodStoreTB (StoreId, storename, kindid) Select sid_seq.nextval,'{0}',(Select KindID from foodkindtb where kindname='{1}')"
                                            +"From DUAL A Where Not EXISTS(Select 0 From foodstoretb Where storeName = '{0}')", storestr,kindstr);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("삽입 오류" + ex.Message);
                return "f";
            }
        }
        public string UpdateStore(string newstore, string oldstore, string kindstr)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("update FoodStoreTB set StoreName = '{0}' where StoreName='{1}' and KindID IN(select KindID from FoodKindTB where KindName = '{2}')", newstore, oldstore,kindstr);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("수정 오류" + ex.Message);
                return "f";
            }
        }
        public string DeleteStore(string storestr,string kindstr)
        {
            try
            {
                //메뉴
                DeleteALL_temp(string.Format("delete from foodmenutb where storeid in(select storeid from FoodStoreTB where StoreName = '{0}' and KindID IN(select KindID from FoodKindTB where KindName = '{1}'))", storestr, kindstr));
                //옵션
                DeleteALL_temp(string.Format("delete from foodoptiontb where storeid in(select storeid from FoodStoreTB where StoreName = '{0}' and KindID IN(select KindID from FoodKindTB where KindName = '{1}'))", storestr, kindstr));
                //가게
                DeleteALL_temp(string.Format("delete from FoodStoreTB where StoreName = '{0}' and KindID IN(select KindID from FoodKindTB where KindName = '{1}')", storestr, kindstr));
                Console.WriteLine("삭제 성공");
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("삭제 오류" + ex.Message);
                return "f";
            }
        }
        #endregion

        #region 메뉴 DB
        public DataSet SelectMenu()
        {
            try
            {
                ConnectDB();
                strSample = "select MenuName from FoodMenuTB";
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
               
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public DataSet Store_SelectOneMenu(string smsg)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("select MenuName from FoodMenuTB where StoreID IN(select storeID from foodstoretb where StoreName like '%{0}%')", smsg);
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public DataSet Store_SelectMenu(string smsg)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("select MenuName,MenuPrice from FoodMenuTB where StoreID IN(select storeID from foodstoretb where StoreName like '%{0}%')", smsg);
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public string InsertMenu(string menustr, string pricestr, string storestr)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("Insert Into FoodMenuTB values(mid_seq.nextval,'{0}',{1},(Select storeid from foodstoretb where storename='{2}'))", menustr, pricestr,storestr);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("삽입 오류" + ex.Message);
                return "f";
            }
        }
        public string UpdateMenu(string newmenu, string newprice, string oldmenu, string storestr)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("update FoodMenuTB set MenuName = '{0}', menuprice= {1} where MenuName='{2}' and StoreID IN(select StoreID from FoodStoreTB where StoreName = '{3}')", newmenu, newprice, oldmenu, storestr);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("수정 오류" + ex.Message);
                return "f";
            }
        }
        public string DeleteMenu(string menustr, string storestr)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("delete from FoodMenuTB where MenuName = '{0}' and StoreID IN(select StoreID from FoodStoreTB where StoreName = '{1}')", menustr, storestr);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("삭제 오류" + ex.Message);
                return "f";
            }
        }

        #endregion

        #region 옵션 DB
        public DataSet SelectOption()
        {
            try
            {
                ConnectDB();
                strSample = "select OptionDes from FoodOptionTB";
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
               /* foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Console.WriteLine(r["OptionDes"].ToString());
                }*/
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public DataSet Store_SelectOneOption(string smsg)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("select Optiondes from foodoptiontb where StoreID IN(select storeID from foodstoretb where StoreName like '%{0}%')", smsg);
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public DataSet Store_SelectOption(string smsg)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("select Optiondes,OptionPrice from foodoptiontb where StoreID IN(select storeID from foodstoretb where StoreName like '%{0}%')", smsg);
                ds.Clear();
                oda.SelectCommand = new OracleCommand(strSample, conn);
                oda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("셀렉트 오류" + ex.Message);
                return null;
            }
        }
        public string InsertOption(string optionstr, string pricestr, string storestr)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("Insert Into FoodOptionTB values(oid_seq.nextval,'{0}',{1},(Select storeid from foodstoretb where storename='{2}'))", optionstr, pricestr, storestr);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("삽입 오류" + ex.Message);
                return "f";
            }
        }
        public string UpdateOption(string newoption, string newprice, string oldoption, string storestr)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("update foodoptiontb set optiondes = '{0}', optionprice={1} where optiondes='{2}' and StoreID IN(select StoreID from FoodStoreTB where StoreName = '{3}')", newoption, newprice, oldoption, storestr);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("수정 오류" + ex.Message);
                return "f";
            }
        }
        public string DeleteOption(string optionstr, string storestr)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("delete from foodoptiontb where optiondes = '{0}' and StoreID IN(select StoreID from FoodStoreTB where StoreName = '{1}')", optionstr, storestr);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("삭제 오류" + ex.Message);
                return "f";
            }
        }
        #endregion
        #region 투표함
        //타이틀
        public string InsertTitleVoting(string title, string memo, string day, string yn)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("insert into VotingTitleTB (VotingNum,VotingTitle,VotingMemo,VotingDay,VotingYN) values (voting_seq.nextval,'{0}','{1}','{2}','{3}')",title,memo,day,yn);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch(Exception ex)
            {
                Console.WriteLine("투표함 타이틀 삽입 오류" + ex.Message);
                return "f";
            }
        }
        //항목
        public DataSet SelectTitleData(string title)
        {
            ConnectDB();
            strSample = string.Format("Select VotingNum from(Select * from VotingTitleTB ORDER BY ROWNUM ASC) where ROWNUM = 1 and VotingTitle = '{0}'", title);
            ds.Clear();
            oda.SelectCommand = new OracleCommand(strSample, conn);
            oda.Fill(ds);
            return ds;
        }
        public string InsertContentVoting(int title_num, string content, int count)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("Insert into VotingContentTB values(votingcon_seq.nextval,{0},'{1}',{2})",title_num,content,count);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("투표함 항목 삽입 오류" + ex.Message);
                return "f";
            }
        }
        //유저
        public DataSet SelectContentData(string content)
        {
            ConnectDB();
            strSample = string.Format("Select VotingConNum from(select * from VotingContentTB ORDER BY ROWNUM DESC) where ROWNUM = 1 and VotingCon = '{0}'", content);
            ds.Clear();
            oda.SelectCommand = new OracleCommand(strSample, conn);
            oda.Fill(ds);
            return ds;
        }
        public string InsertUserVoting(int content_num, string user)
        {
            try
            {
                ConnectDB();
                strSample = string.Format("insert into VotingCountTB values({0},'{1}')",content_num,user);
                cmd = new OracleCommand(strSample, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "t";
            }
            catch (Exception ex)
            {
                Console.WriteLine("투표함 유저 삽입 오류" + ex.Message);
                return "f";
            }
        }
        //전체 불러오기
        public DataSet SelectAllVoting(string user)
        {
            ConnectDB();
            strSample = string.Format("Select distinct * from votingtitletb t1, votingcontenttb t2, votingcounttb t3 where t1.votingnum" +
                " IN(Select distinct t1.votingnum from votingtitletb t1, votingcontenttb t2, votingcounttb t3 where t3.votinguser = '{0}')" +
                " and t1.votingnum = t2.votingnum2 and t2.votingconnum = t3.votingconnum2", user);
            ds.Clear();
            oda.SelectCommand = new OracleCommand(strSample, conn);
            oda.Fill(ds);
            return ds;
        }
        #endregion
    }
}
