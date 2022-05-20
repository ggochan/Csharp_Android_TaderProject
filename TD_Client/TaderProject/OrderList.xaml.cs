using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaderProject.CModels;

namespace TaderProject
{
    /// <summary>
    /// OrderList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderList : Window
    {
        HttpClient client = new HttpClient();

        string msguri = "";
        string msguserid = "";
        int msgposion = 0;

        string storename_str = "";
        string listusermenuop = "";

        OrderList_MenuAddDlg menudlg = null;
        OrderList_OptionAddDlg optiondlg = null;

        // 자식 -> 부모 데이터 보내기 이벤트
        public delegate void OnChildDataInputHandler(int Parameters);
        public event OnChildDataInputHandler OnChildDataInputEvent;

        public OrderList(string userid,string uristr,int positoncheck)
        {
            InitializeComponent();

            #region 클라이언트 세팅
            client.BaseAddress = new Uri(uristr);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            #endregion

            #region UI 세팅
            msguri = uristr;
            msguserid = userid;
            msgposion = positoncheck;

            if(positoncheck == 2)
            {
                MenuAddbtn.Content = "메뉴 수정";
                GetOrderUserMenuData(userid);
            }
            else if(positoncheck != 2)
            {
                MenuAddbtn.Content = "메뉴 추가";
            }
            #endregion

            GetOrderTitleData();
            GetOrderInfolistData();
        }
        #region 타이틀바 UI 이벤트
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ToMiniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 부모(Orderlist) <- 자식(MenuDlg, OptionDlg) 값 이벤트 
        private void recv_OnChildOrderUIDataInputEvent(int Parameters)
        {
            if(Parameters == 1)
            {
                if (OnChildDataInputEvent != null) OnChildDataInputEvent(2); // 현재 상태, User ID
                listusermenuop = "";
                this.Close();
            }
            if (menudlg != null)
            {
                menudlg.Close();
                menudlg = null;
            }
            if (optiondlg != null)
            {
                optiondlg.Close();
                optiondlg = null;
            }
        }
        #endregion

        #region Orderlist UI Data Get

        // 종류, 가게명 GET
        private async void GetOrderTitleData()
        {
            try
            {
                var response = await client.GetAsync("OrderList");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();
               
                foreach (var item in tests)
                {
                    if (item != null)
                    {
                        string[] splstring = item.ToString().Split('&');
                        li_FoodKindLA.Content = splstring[0];
                        li_StoreNameLA.Content = splstring[1];
                        storename_str = splstring[1];
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
            catch(Exception)
            {
                MessageBox.Show("주문이 없습니다.");
                this.Close();
            }
        }
        // 주문 내역 GET
        private async void GetOrderInfolistData()
        {
            try
            {
                var response = await client.GetAsync("OrderInfo");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<C_OrderCreatelist>>();

                C_OrderCreatelist.GetList().Clear();
                int i = 1;
                foreach (var item in tests)
                {
                    C_OrderCreatelist.GetList().Add(new C_OrderCreatelist {Num=i.ToString(),Menu=item.Menu,Menu_Count=item.Menu_Count});
                    i++;
                }
                li_OrderMenuList.ItemsSource = C_OrderCreatelist.GetList();

            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
        }

        //메뉴 수정 - 유저 주문 내역
        private async void GetOrderUserMenuData(string _userid)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderInfo/usermenu/{0}",_userid));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();

                foreach(var item in tests)
                {
                    if (item != null)
                    {
                        listusermenuop = item.ToString();
                    }
                    else
                    {
                        break;
                    }
                }

            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
        }
        #endregion

        #region 다이얼로그
        //사용자 메뉴, 옵션 불러오기
        private void MenuAddbtn_Click(object sender, RoutedEventArgs e)
        {
            if (menudlg == null && msgposion != 2) // 추가버튼
            {
                menudlg = new OrderList_MenuAddDlg(msguserid, msguri, storename_str, "");
                menudlg.OnChildDataInputEvent += new OrderList_MenuAddDlg.OnChildDataInputHandler(recv_OnChildOrderUIDataInputEvent);
                menudlg.ShowDialog();
            }
            else if(msgposion == 2) //수정할때 
            {
                menudlg = new OrderList_MenuAddDlg(msguserid, msguri, storename_str, listusermenuop);
                menudlg.OnChildDataInputEvent += new OrderList_MenuAddDlg.OnChildDataInputHandler(recv_OnChildOrderUIDataInputEvent);
                menudlg.ShowDialog();
            }
            
        }
        // 리스트 +버튼 클릭
        private void Listbtn_Click(object sender, RoutedEventArgs e)
        {
            C_OrderCreatelist listbtn = ListView_GetItem(e);
            if (optiondlg == null && msgposion != 2)
            {
                optiondlg = new OrderList_OptionAddDlg(msguserid, msguri, listbtn.Menu, storename_str);
                optiondlg.OnChildDataInputEvent += new OrderList_OptionAddDlg.OnChildDataInputHandler(recv_OnChildOrderUIDataInputEvent);
                optiondlg.ShowDialog();
            }
            else if (msgposion == 2)
            {
                MessageBox.Show("오른쪽 하단의 메뉴 수정을 눌러주세요");
            }

        }
        #endregion

        #region 리스트 Item 버튼 클릭 이벤트

        // 리스트 Item 버튼 클릭 이벤트
        private static C_OrderCreatelist ListView_GetItem(RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while(!(dep is System.Windows.Controls.ListViewItem))
            {
                try
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }
                catch
                {
                    return null;
                }
            }
            ListViewItem item = (ListViewItem)dep;
            C_OrderCreatelist content = (C_OrderCreatelist)item.Content;
            return content;
        }
        #endregion
    }
}
