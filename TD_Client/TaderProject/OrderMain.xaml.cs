using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaderProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderMain : Window
    {
        private static DispatcherTimer ordertimer;
        HttpClient client = new HttpClient();
        string uristr = ""; //"http://172.30.1.51:5656/";

        string strLocalPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf('\\')); //File Address
        private int positoncheck = 0; // 리스트 존재여부 체크 (1) 있다 t == check>0 , 리스트에서 ID결과 찾기 있다 = 2 없다 =1 (2) 존재여부 없다 check = 0
        private int itimer = 0;

        private string checkuserid = "";
        private string deleteid = "";
        private string dbcheckuserid = "";

        private bool timercall = false;
        private bool connectcheck = false;

        Order odpage = null;
        OrderList odlist = null;
        OrderReceipt odre = null;

        System.Windows.Forms.NotifyIcon ni;
        public OrderMain()
        {
            #region 공용 Window Setting
            InitializeComponent();

            #region 클라이언트 세팅
            if (System.IO.File.Exists(strLocalPath + "\\SERVERIP.txt")) // 폴더 확인
            {
                string strServerIP = System.IO.File.ReadAllText("SERVERIP.txt"); //폴더 불러오기

                if (strServerIP == "")
                {
                    return;
                }
                uristr = string.Format("http://{0}/", strServerIP);
                client.BaseAddress = new Uri(string.Format("http://{0}/", strServerIP));
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            }
            else
            {
                if (IPChecker(0) == false)
                    throw new Exception("IP 설정이 잘못되었습니다.");
                if (System.IO.File.Exists(strLocalPath + "\\SERVERIP.txt")) // 폴더 확인
                {
                    string strServerIP = System.IO.File.ReadAllText("SERVERIP.txt"); //폴더 불러오기

                    if (strServerIP == "")
                    {
                        return;
                    }
                    uristr = string.Format("http://{0}/", strServerIP);
                    client.BaseAddress = new Uri(string.Format("http://{0}/", strServerIP));
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                }
            }
            #endregion

            Loaded += Window_Loaded;

            #endregion

            #region 각각 속성 세팅
            IdTextBox.MaxLength = 20;

            #endregion

            #region ID File IO
            if (System.IO.File.Exists(strLocalPath + "\\ID.txt")) // 폴더 확인
            {
                IDupdatebtn.Content = "ID수정";
                string strReturnValue = System.IO.File.ReadAllText("ID.txt"); //폴더 불러오기

                if (strReturnValue == "")
                {
                    MessageBox.Show("불러오기 실패 ID가 없습니다.");
                    return;
                }
                IdTextBox.Text = strReturnValue;
                IdTextBox.IsEnabled = false;
            }
            #endregion

            GetOrderCheck();

            ordertimer = new DispatcherTimer();

            ordertimer.Interval = TimeSpan.FromSeconds(2);
            ordertimer.Tick += new EventHandler(OnTimedEvent);
            ordertimer.Start();
        }
        #region 트레이
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();    // Menu 객체

                System.Windows.Forms.MenuItem item1 = new System.Windows.Forms.MenuItem();    // Menu 객체에 들어갈 각각의 menu
                item1.Index = 1;
                item1.Text = "TD 종료";    // menu 이름

                item1.Click += delegate (object click, EventArgs eClick)    // menu 의 클릭 이벤트 등록
                {
                    this.Close();
                 };
                System.Windows.Forms.MenuItem item2 = new System.Windows.Forms.MenuItem();    // menu 객체에 들어갈 각 menu
                item2.Index = 0;
                item2.Text = "TD 열기";    // menu 이름
                item2.Click += delegate (object click, EventArgs eClick)    // menu의 클릭 이벤트 등록
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    InitializeComponent();
                };


                menu.MenuItems.Add(item1);    // Menu 객체에 각각의 menu 등록
                menu.MenuItems.Add(item2);    // Menu 객체에 각각의 menu 등록

                ni = new System.Windows.Forms.NotifyIcon();
                ni.Icon = TaderProject.Properties.Resources.tdicon_white1;    // 아이콘 등록 1번째 방법
                ni.Visible = true;
                ni.DoubleClick += delegate (object senders, EventArgs args)    // Tray icon의 더블 클릭 이벤트 등록
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    InitializeComponent();
                };
                ni.ContextMenu = menu;    // Menu 객체 등록
                ni.Text = "TD";    // Tray icon 이름
            }catch(Exception)
            {

            }
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if(WindowState.Minimized.Equals(WindowState))
            {
                this.Hide();
                ni.Visible = false;
                ni.Dispose();
            }
            base.OnStateChanged(e);
        }
        #endregion

        #region 주기적으로 확인
        private void OnTimedEvent(Object source, EventArgs e)
        {
            try
            {
                itimer++;
                if(itimer == 90)
                {
                    timercall = false;
                    itimer = 0;
                }
                GetOrderCheck();
                ordertimer.Stop();
                
            }catch(Exception)
            {

            }
        }
        #endregion

        #region IP Check && ConectCheck
        private bool IPChecker(int check)
        {
            IPSettingDlg iPSettingDlg = new IPSettingDlg(check);
            iPSettingDlg.ShowDialog();

            if (iPSettingDlg.DialogResult.HasValue && iPSettingDlg.DialogResult.Value)
                return true;
            else
                return false;
           
        }
       
        #endregion

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
        /* public void UpdateWindow() // 화면갱신
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(
                      System.Windows.Threading.DispatcherPriority.Background,
                      new System.Threading.ThreadStart(delegate { }));
        }*/

        //주문받기 활성화 확인 GET
        private async void GetOrderCheck()
        {
            try
            {
                var response = await client.GetAsync("OrderList/orderbool/t");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();
                foreach (var item in tests)
                {
                    if (item.ToString() == "t") // 주문 정보 있음
                    {
                        positoncheck = 3;
                        GetIdCheck();
                    }
                    else if (item.ToString() == "f")  // 주문 정보 없음
                    {
                        positoncheck = 0;
                    }
                }
                connectcheck = true;
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                connectcheck = false;
                MessageBox.Show("서버 연결이 되있지 않습니다");
            }
        }
        private void Tray_BalloonTipClicked(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }
                      // 주문 ID 체크
            private async void GetIdCheck()
        {
            try
            {
                string checkid = string.Format("OrderInfo/userid/{0}", IdTextBox.Text);
                var response = await client.GetAsync(checkid);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();
                foreach (var item in tests) // 주문 아이디 있음
                {
                    if (item.ToString() == "t")
                    {
                        positoncheck = 2;
                    }
                    else if (item.ToString() == "f") // 주문 아이디 없음
                    {
                        positoncheck = 1;
                    }
                    if(timercall == false && item.ToString() == "f")
                    {
                        ni.BalloonTipTitle = "주문 오더 내역이 있습니다";
                        ni.BalloonTipText = "주문을 해주세요";
                        ni.ShowBalloonTip(100);
                        ni.BalloonTipClicked += new EventHandler(Tray_BalloonTipClicked);
                        timercall = true;
                    }
                }
                GetUserCount();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
            }
        }
        // 인원 도달 여부 GET
        private async void GetUserCount()
        {
            try
            {
                var response = await client.GetAsync("Orderlist/Count");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();

                foreach (var item in tests)
                {
                    checkuserid = item.ToString();
                }
                if (IdTextBox.Text == checkuserid && checkuserid != "zopweiqushdzasdwqfngl")
                {
                    positoncheck = 0;
                    //OrderReceipt

                    receipt_btn.Visibility = Visibility.Visible;
                    odre = new OrderReceipt(uristr);
                    odre.ShowDialog();
                    receipt_btn.Visibility = Visibility.Hidden;
                }
                ordertimer.Start();
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
            }
        }
        // DB ID 체크
        private async void GetUserCheck()
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderInfo/userid/select/{0}",IdTextBox.Text));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();

                foreach (var item in tests)
                {
                    dbcheckuserid = item.ToString();
                }
                if (IdTextBox.Text == dbcheckuserid)
                {
                    if (System.IO.File.Exists(strLocalPath + "\\ID.txt"))
                    {
                        System.IO.File.Delete(strLocalPath + "\\ID.txt");
                    }
                    string strID = IdTextBox.Text;
                    System.IO.File.WriteAllText("ID.txt", strID);
                    IdTextBox.IsEnabled = false;
                    IDupdatebtn.Content = "ID수정";
                    dbcheckuserid = "";
                }
                else
                {
                    MessageBox.Show("이미 존재하는 아이디 입니다.");
                    dbcheckuserid = "";
                }
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
            }
        }
        //기존 유저 삭제
        private async void UserDeleteCall(string deletestr)
        {
            try
            {
                var response = await client.GetAsync(string.Format("OrderInfo/userid/delete/{0}", deletestr));
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var tests = await response.Content.ReadAsAsync<IEnumerable<string>>();

            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show("1오류" + jEx.Message);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("ID 수정 실패");
            }
        }

        //ID관련 예외처리
        private void IDupdatebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDupdatebtn.Content == "ID확인")
                {
                    string strID = IdTextBox.Text;
                    System.IO.File.WriteAllText("ID.txt", strID);
                }
                else
                {
                    if (IdTextBox.IsEnabled == false)
                    {
                        ordertimer.Stop();
                        IdTextBox.IsEnabled = true;
                        IDupdatebtn.Content = "수정완료";
                        deleteid = IdTextBox.Text;
                    }
                    else
                    {
                        GetUserCheck();
                        UserDeleteCall(deleteid);
                        ordertimer.Start();
                    }
                }
            }catch(Exception)
            {

            }
        }
        #region 자식 부모 이벤트 핸들러
        //주문받기버튼 - > 메인 자식->부모 데이터가져오는 이벤트 핸들러
        private void recv_OnChildOrderUIDataInputEvent(int Parameters)
        {
            try
            {
                positoncheck = Parameters;
                if (odpage != null)
                {
                    odpage.Close();
                    odpage.OnChildDataInputEvent -= new Order.OnChildDataInputHandler(recv_OnChildOrderUIDataInputEvent);
                    odpage = null;
                }
                ordertimer.Start();
            }
            catch(Exception)
            {

            }
        }
        //주문하기버튼 -> 메인
        private void recv_OnChildlistUIDataInputEvent(int Parameters)
        {
            try
            {
                positoncheck = Parameters;

                GetUserCount();

                if (odlist != null)
                {
                    odlist.Close();
                    odlist.OnChildDataInputEvent -= new OrderList.OnChildDataInputHandler(recv_OnChildlistUIDataInputEvent);
                    odlist = null;
                }
                ordertimer.Start();
            }
            catch(Exception)
            {

            }
        }
        private void recv_OnChildRecDataInputEvent(int Parameters)
        {
            try
            {
                positoncheck = Parameters;

                this.WindowState = System.Windows.WindowState.Minimized;
                if (odlist != null)
                {
                    odre.Close();
                    odre.OnChildDataInputEvent -= new OrderReceipt.recv_OnChildRecDataInputEvent(recv_OnChildRecDataInputEvent);
                    odre = null;
                }
                ordertimer.Start();
            }
            catch(Exception)
            { }
        }
        #endregion

        #region 버튼 이벤트
        //주문받기 버튼
        private void TakeOrderbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ordertimer.Stop();
                // od_name = new OrderName() { Name = this.IdTextBox.Text };
                if (IdTextBox.IsEnabled == false && IdTextBox.Text != "")
                {
                    if (odpage == null && positoncheck == 0)
                    {
                        odpage = new Order(IdTextBox.Text, uristr);
                        odpage.OnChildDataInputEvent += new Order.OnChildDataInputHandler(recv_OnChildOrderUIDataInputEvent);
                        odpage.ShowDialog();
                    }
                    else if (positoncheck != 0)
                    {
                        MessageBox.Show("오더가 있습니다. 주문을 해주세요");
                    }
                }
                else if (IdTextBox.Text == "")
                {
                    MessageBox.Show("id를 입력해주세요.");
                }
                else if (IdTextBox.IsEnabled == true)
                {
                    MessageBox.Show("id 옆 버튼을 클릭해 완료해주세요.");
                }
            }
            catch(Exception)
            {

            }
        }
        //주문하기 버튼
        private void Orderingbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ordertimer.Stop();
                if (IdTextBox.IsEnabled == false && IdTextBox.Text != "")
                {
                    if (odpage == null && positoncheck != 2) //&& positoncheck != 2
                    {
                        odlist = new OrderList(IdTextBox.Text, uristr, positoncheck);
                        odlist.OnChildDataInputEvent += new OrderList.OnChildDataInputHandler(recv_OnChildlistUIDataInputEvent);
                        odlist.ShowDialog();
                    }
                    else if (positoncheck == 2)
                    {
                        odlist = new OrderList(IdTextBox.Text, uristr, positoncheck);
                        odlist.OnChildDataInputEvent += new OrderList.OnChildDataInputHandler(recv_OnChildlistUIDataInputEvent);
                        odlist.ShowDialog();
                    }
                }
                else if (IdTextBox.Text == "")
                {
                    MessageBox.Show("ID를 입력해주세요.");
                }
                else if (IdTextBox.IsEnabled == true)
                {
                    MessageBox.Show("ID 옆 버튼을 클릭해 완료해주세요.");
                }
            }
            catch(Exception)
            {

            }
        }
        // 주문관리 버튼
        private void OrderManagement_Click(object sender, RoutedEventArgs e)
        {
            OrderManagement ma = new OrderManagement(uristr);
            ma.ShowDialog();
        }
        //영수증 버튼
        private void OrderReceipt_Click(object sender, RoutedEventArgs e)
        {
            OrderReceipt odre = new OrderReceipt(uristr);
            odre.ShowDialog();
        }
        private void IPbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(strLocalPath + "\\SERVERIP.txt") && connectcheck == true) // 폴더 확인
                {
                    if (IPChecker(1) == false)
                        throw new Exception("IP 설정이 잘못되었습니다.");

                    string strServerIP = System.IO.File.ReadAllText("SERVERIP.txt"); //폴더 불러오기

                    if (strServerIP == "")
                    {
                        return;
                    }
                    uristr = string.Format("http://{0}/", strServerIP);
                    client.BaseAddress = new Uri(string.Format("http://{0}/", strServerIP));
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    ordertimer.Start();
                }
                else if (connectcheck == false)
                {
                    if (IPChecker(1) == false)
                        throw new Exception("IP 설정이 잘못되었습니다.");
                    if (System.IO.File.Exists(strLocalPath + "\\SERVERIP.txt")) // 폴더 확인
                    {
                        string strServerIP = System.IO.File.ReadAllText("SERVERIP.txt"); //폴더 불러오기

                        if (strServerIP == "")
                        {
                            return;
                        }
                        uristr = string.Format("http://{0}/", strServerIP);
                        client.BaseAddress = new Uri(string.Format("http://{0}/", strServerIP));
                        client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                        ordertimer.Start();
                    }
                }
            }
            catch(Exception)
            {

            }
        }
        #endregion

     
    }
}
//주석값
//WindowStartupLocation="Manual" -xaml
//this.Left = SystemParameters.WorkArea.Width - this.Width;
//this.Top = SystemParameters.WorkArea.Height - this.Height; 우측하단